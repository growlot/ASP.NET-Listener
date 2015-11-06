﻿namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Extensions;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using ApplicationService;
    using MetadataService;
    using MetadataService.Attributes;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    using Newtonsoft.Json;
    using Repository.WNP;
    using Serilog;
    using Services.FilterTransformer;
    using Utilities;

    /// <summary>
    /// Base implementation of OData controller for WNP.
    /// </summary>
    [EnableQuery]
    public abstract class WNPController : ODataController
    {
        /// <summary>
        /// The metadata service
        /// </summary>
        protected readonly IMetadataProvider metadataService;

        protected readonly IFilterTransformer filterTransformer;
        protected readonly IActionConfigurator actionConfigurator;

        protected IWNPUnitOfWork unitOfWork;

        protected readonly ICommandBus commandBus;

        protected int Owner { get; set; }

        protected Type EdmEntityClrType { get; set; }

        protected WNPController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus, CurrentUnitOfWork test = null)
        {
            this.metadataService = metadataService;
            this.unitOfWork = unitOfWork;
            this.filterTransformer = filterTransformer;
            this.actionConfigurator = actionConfigurator;
            this.commandBus = commandBus;
        }

        public async Task<IHttpActionResult> UnboundActionHandler()
        {
            var oDataProperties = this.Request.ODataProperties();
            var oDataPath = oDataProperties.Path;

            var actionSegment = oDataPath.Segments[0] as UnboundActionPathSegment;
            var fqnActionName = actionSegment?.ActionName;

            Debug.Assert(fqnActionName != null, "fqnActionName != null");

            var underscorePosition = fqnActionName.IndexOf("_", StringComparison.Ordinal);
            var containerTypeName = fqnActionName.Substring(0, underscorePosition);
            var actionName = fqnActionName.Substring(underscorePosition + 1);

            return await this.InvokeAction(this.actionConfigurator.GetUnboundActionContainer(containerTypeName), actionName);
        }

        protected async Task<IHttpActionResult> InvokeAction(Type actionsContainerType, string actionName, KeyValuePathSegment keySegment = null)
        {
            // each entity has its controller with actions defined there
            var actionsContainer = this;

            // get the action parameters
            var jsonParameters =
                JsonConvert.DeserializeObject<Dictionary<string, object>>(await this.Request.Content.ReadAsStringAsync());

            var methodInfo = actionsContainerType.GetMethod(actionName);
            if (methodInfo == null)
            {
                return this.NotFound();
            }

            // check the number of parameters
            var parametersInfo = methodInfo.GetParameters();
            if (this.GetRequiredParametersCount(parametersInfo) > jsonParameters.Count)
            {
                return
                    this.BadRequest($"Invalid number of non-optional parameters. Expected: {parametersInfo.Length}. Got: {jsonParameters.Count}.");
            }

            var missingParameter =
                parametersInfo.FirstOrDefault(
                    parameterInfo =>
                        !jsonParameters.ContainsKey(parameterInfo.Name) && !parameterInfo.IsOptional &&
                        parameterInfo.CustomAttributes.All(
                            data => data.AttributeType != typeof(BoundEntityKeyAttribute)));

            if (missingParameter != null)
            {
                return this.BadRequest($"Non-optional parameter {missingParameter.Name} not found in request body.");
            }

            // if we have a key, we can optionally bind it to the appropriate action parameter
            if (keySegment != null)
            {
                var keyValue = JsonConvert.DeserializeObject(keySegment.Value);
                var entityKeyParameter =
                    parametersInfo.FirstOrDefault(
                        info => info.CustomAttributes.Any(data => data.AttributeType == typeof(BoundEntityKeyAttribute)));

                if (entityKeyParameter != null)
                {
                    if (jsonParameters.ContainsKey(entityKeyParameter.Name))
                    {
                        return this.BadRequest($"Parameter {entityKeyParameter.Name} is entity key.");
                    }

                    jsonParameters.Add(entityKeyParameter.Name, keyValue);
                }
            }

            try
            {
                // adjust parameters types
                jsonParameters = jsonParameters.ToDictionary(kvp => kvp.Key,
                    kvp =>
                        Converters.Convert(kvp.Value, parametersInfo.First(info => info.Name == kvp.Key).ParameterType));

                var result = methodInfo.InvokeWithNamedParameters(actionsContainer, jsonParameters);

                if (methodInfo.ReturnType != typeof(void))
                {
                    return this.CreateSimpleOkResponse(methodInfo.ReturnType, result);
                }

                return this.Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Action {ActionName} in container {ContainerType} failed to execute.", actionName,
                    actionsContainerType.FullName);

                return this.InternalServerError(ex);
            }
        }

        private int GetRequiredParametersCount(ParameterInfo[] parameters) =>
            parameters.Count(
                info =>
                    !info.IsOptional &&
                    info.CustomAttributes.All(data => data.AttributeType != typeof(BoundEntityKeyAttribute)));

        protected IHttpActionResult CreateSimpleOkResponse(Type dataType, object result)
            => (IHttpActionResult)this.GetSimpleOkMethod(dataType).Invoke(this, new[] { result });

        private MethodInfo GetSimpleOkMethod(Type dataType)
            => MemoryCache.Default.GetOrAddExisting($"WNPController.SimpleOkMethod<{dataType.FullName}>",
            () =>
                this.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                    .MakeGenericMethod(dataType));

        protected ODataQueryOptions ConstructQueryOptions()
        {
            var oDataProperties = this.Request.ODataProperties();
            var oDataPath = oDataProperties.Path;
            var model = oDataProperties.Model;
            Type edmEntityClrType;

            if (oDataProperties.Path.PathTemplate == "~/entityset/key/action" || oDataProperties.Path.PathTemplate == "~/entityset/action")
            {
                var entitySetName = oDataProperties.Path.Segments[0] as EntitySetPathSegment;
                var edmType = entitySetName.GetEdmType(null);
                edmEntityClrType = this.metadataService.GetEntityType(((IEdmCollectionType)edmType).ElementType.ShortQualifiedName());
            }
            else
            {
                string modelTypeFullName;
                switch (oDataProperties.Path.EdmType.TypeKind)
                {
                    case EdmTypeKind.None:
                        throw new NotSupportedException("EdmTypeKind.None not yet supported");
                    case EdmTypeKind.Primitive:
                        throw new NotSupportedException("EdmTypeKind.Primitive not yet supported");
                    case EdmTypeKind.Entity:
                        modelTypeFullName = ((EdmEntityType)oDataProperties.Path.EdmType).FullName();
                        break;
                    case EdmTypeKind.Complex:
                        throw new NotSupportedException("EdmTypeKind.Complex not yet supported");
                    case EdmTypeKind.Collection:
                        modelTypeFullName = ((EdmCollectionType)oDataProperties.Path.EdmType).ElementType.FullName();
                        break;
                    case EdmTypeKind.EntityReference:
                        throw new NotSupportedException("EdmTypeKind.EntityReference not yet supported");
                    case EdmTypeKind.Enum:
                        throw new NotSupportedException("EdmTypeKind.Enum not yet supported");
                    case EdmTypeKind.TypeDefinition:
                        throw new NotSupportedException("EdmTypeKind.TypeDefinition not yet supported");
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                edmEntityClrType = this.metadataService.ODataModelAssembly.GetType(modelTypeFullName);
            }

            this.EdmEntityClrType = edmEntityClrType;
            return new ODataQueryOptions(new ODataQueryContext(model, edmEntityClrType, oDataPath), this.Request);
        }
    }
}