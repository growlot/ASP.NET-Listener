// <copyright file="WNPController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers.Base
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
        /// Initializes a new instance of the <see cref="WNPController"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        protected WNPController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus)
        {
            this.ConstructQueryOptions();
            this.MetadataService = metadataService;
            this.UnitOfWork = unitOfWork;
            this.FilterTransformer = filterTransformer;
            this.ActionConfigurator = actionConfigurator;
            this.CommandBus = commandBus;
        }

        /// <summary>
        /// Gets the metadata service.
        /// </summary>
        /// <value>
        /// The metadata service.
        /// </value>
        protected IMetadataProvider MetadataService { get; private set; }

        /// <summary>
        /// Gets the filter transformer.
        /// </summary>
        /// <value>
        /// The filter transformer.
        /// </value>
        protected IFilterTransformer FilterTransformer { get; private set; }

        /// <summary>
        /// Gets the action configurator.
        /// </summary>
        /// <value>
        /// The action configurator.
        /// </value>
        protected IActionConfigurator ActionConfigurator { get; private set; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        protected IWNPUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        /// Gets the command bus.
        /// </summary>
        /// <value>
        /// The command bus.
        /// </value>
        protected ICommandBus CommandBus { get; private set; }

        /// <summary>
        /// Gets or sets the owner for this request.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        protected int Owner { get; set; }

        /// <summary>
        /// Gets or sets the CLR type of the EDM entity.
        /// </summary>
        /// <value>
        /// The CLR type of the EDM entity.
        /// </value>
        protected Type EdmEntityClrType { get; set; }

        /// <summary>
        /// Gets or sets the query options.
        /// </summary>
        /// <value>
        /// The query options.
        /// </value>
        protected ODataQueryOptions QueryOptions { get; set; }

        /// <summary>
        /// Handles the call to unbound OData action.
        /// </summary>
        /// <returns>The result of the action</returns>
        public async Task<IHttpActionResult> UnboundActionHandler()
        {
            var oDataProperties = this.Request.ODataProperties();
            var oDataPath = oDataProperties.Path;

            var actionSegment = oDataPath.Segments[0] as UnboundActionPathSegment;
            var fqnActionName = actionSegment?.ActionName;

            Debug.Assert(fqnActionName != null, "fqnActionName != null");

            var underscorePosition = fqnActionName.LastIndexOf(".", StringComparison.Ordinal);
            var containerTypeName = "Unbound"; // fqnActionName.Substring(0, underscorePosition);
            var actionName = fqnActionName.Substring(underscorePosition + 1);

            return await this.InvokeAction(this.ActionConfigurator.GetUnboundActionContainer(containerTypeName), actionName);
        }

        /// <summary>
        /// Invokes the OData action.
        /// </summary>
        /// <param name="actionsContainerType">Type of the actions container.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="keySegment">The key segment.</param>
        /// <returns>The result of the action</returns>
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
                var entityKeyParameter =
                    parametersInfo.FirstOrDefault(
                        info => info.CustomAttributes.Any(data => data.AttributeType == typeof(BoundEntityKeyAttribute)));

                if (entityKeyParameter != null)
                {
                    if (jsonParameters.ContainsKey(entityKeyParameter.Name))
                    {
                        return this.BadRequest($"Parameter {entityKeyParameter.Name} is entity key.");
                    }

                    jsonParameters.Add(entityKeyParameter.Name, JsonConvert.DeserializeObject(keySegment.Value));
                }
            }

            try
            {
                // adjust parameters types
                jsonParameters = jsonParameters.ToDictionary(
                    kvp => kvp.Key,
                    kvp => Converters.Convert(kvp.Value, parametersInfo.First(info => info.Name == kvp.Key).ParameterType));

                return await (Task<IHttpActionResult>)methodInfo.InvokeWithNamedParameters(actionsContainer, jsonParameters);
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "Action {ActionName} in container {ContainerType} failed to execute.",
                    actionName,
                    actionsContainerType.FullName);

                return this.InternalServerError(ex);
            }
        }

        /// <summary>
        /// Creates the HTTP OK response.
        /// </summary>
        /// <param name="dataType">Type of the data.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        protected IHttpActionResult CreateSimpleOkResponse(Type dataType, object result)
                    => (IHttpActionResult)this.GetSimpleOkMethod(dataType).Invoke(this, new[] { result });

        /// <summary>
        /// Constructs the query options.
        /// </summary>
        protected void ConstructQueryOptions()
        {
            var oDataProperties = this.Request.ODataProperties();
            var oDataPath = oDataProperties.Path;
            var model = oDataProperties.Model;
            Type edmEntityClrType;

            switch (oDataProperties.Path.PathTemplate)
            {
                case "~/entityset/action":
                case "~/entityset/key/action":
                    var entitySetName = oDataProperties.Path.Segments[0] as EntitySetPathSegment;
                    var entitySetEdmType = entitySetName.GetEdmType(null);
                    edmEntityClrType = this.MetadataService.GetEntityType(((IEdmCollectionType)entitySetEdmType).ElementType.ShortQualifiedName());
                    break;

                case "~/entityset/key/navigation/action":
                case "~/entityset/key/navigation/key/action":
                    var navigationPropertyName = oDataProperties.Path.Segments[2] as NavigationPathSegment;
                    var navigationEdmType = navigationPropertyName.GetEdmType(null);
                    edmEntityClrType = this.MetadataService.GetEntityType(((IEdmCollectionType)navigationEdmType).ElementType.ShortQualifiedName());
                    break;

                default:
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

                    edmEntityClrType = this.MetadataService.ODataModelAssembly.GetType(modelTypeFullName);
                    break;
            }

            this.EdmEntityClrType = edmEntityClrType;
            this.QueryOptions = new ODataQueryOptions(new ODataQueryContext(model, edmEntityClrType, oDataPath), this.Request);
        }

        private MethodInfo GetSimpleOkMethod(Type dataType)
            => MemoryCache.Default.GetOrAddExisting(
                $"WNPController.SimpleOkMethod<{dataType.FullName}>",
                () => this.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                    .MakeGenericMethod(dataType));

        private int GetRequiredParametersCount(ParameterInfo[] parameters) =>
            parameters.Count(
                info =>
                    !info.IsOptional &&
                    info.CustomAttributes.All(data => data.AttributeType != typeof(BoundEntityKeyAttribute)));
    }
}