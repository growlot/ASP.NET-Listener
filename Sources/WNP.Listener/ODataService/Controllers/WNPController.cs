// <copyright file="WNPController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Collections;
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
    using Core;
    using MetadataService;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    using Newtonsoft.Json;
    using Persistence;
    using Serilog;
    using Services;
    using Services.FilterTransformer;

    [EnableQuery]
    public class WNPController : ODataController
    {
        protected readonly IMetadataProvider metadataService;
        protected readonly IFilterTransformer filterTransformer;
        protected readonly IAutoConvertor convertor;
        protected readonly IActionConfigurator actionConfigurator;

        private readonly WNPDBContext dbContext;

        private readonly ODataValidationSettings defaultODataValidationSettings;

        public WNPController(IMetadataProvider metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
        {
            this.metadataService = metadataService;
            this.dbContext = dbContext;
            this.filterTransformer = filterTransformer;
            this.convertor = convertor;
            this.actionConfigurator = actionConfigurator;

            this.defaultODataValidationSettings = new ODataValidationSettings()
            {
                AllowedQueryOptions =
                    AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Skip,
            };
        }

        public IHttpActionResult Get()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            var queryOptions = this.ConstructQueryOptions();
            queryOptions.Validate(this.defaultODataValidationSettings);

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;

            var modelMapping = this.metadataService.GetModelMapping(oDataModelType.Name);

            var skip = queryOptions.Skip?.Value ?? 0;
            var top = queryOptions.Top?.Value ?? 10;

            // create actual result object we will be sending over the wire
            var result = this.CreateResultList(oDataModelType);

            var sql =
                Sql.Builder.Select(this.GetDBColumnsList(queryOptions.SelectExpand, modelMapping.ModelToColumnMappings))
                           .From($"{modelMapping.TableName}");

            var sqlWhere = this.filterTransformer.TransformFilterQueryOption(queryOptions.Filter);

            // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
            for (int i = 0; i < sqlWhere.PositionalParameters.Length; i++)
            {
                DateTimeOffset? parameter = sqlWhere.PositionalParameters[i] as DateTimeOffset?;

                if (parameter != null)
                {
                    DateTime localTime = new DateTime(parameter.Value.ToLocalTime().Ticks);
                    DateTime localTimeAsUtc = DateTime.SpecifyKind(localTime, DateTimeKind.Utc);
                    sqlWhere.PositionalParameters[i] = (DateTimeOffset)localTimeAsUtc;
                }
            }

            if (!string.IsNullOrWhiteSpace(sqlWhere.Clause))
            {
                sql = sql.Where(sqlWhere.Clause, sqlWhere.PositionalParameters);
            }

            var dbResults = this.dbContext.SkipTake<dynamic>(skip, top, sql);

            foreach (var record in dbResults)
            {
                var entityInstance = this.CreateResult(oDataModelType);

                var rawData = (IDictionary<string, object>)record;
                foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                {
                    var property = oDataModelType.GetProperty(modelMapping.ColumnToModelMappings[key.ToLowerInvariant()]);
                    property.SetValue(entityInstance, this.convertor.Convert(rawData[key], property.PropertyType));
                }

                result.Add(entityInstance);
            }

            return this.CreateOkResponse(oDataModelType, result);
        }

        public async Task<IHttpActionResult> EntityActionHandler()
        {
            var queryOptions = this.ConstructQueryOptions();
            var oDataPath = queryOptions.Context.Path;

            var isCollectionWide = oDataPath.PathTemplate == "~/entityset/action";

            var entitySetSegment = oDataPath.Segments[0] as EntitySetPathSegment;

            Debug.Assert(entitySetSegment != null, "entitySetSegment != null");
            var entityName = ((IEdmCollectionType)entitySetSegment.GetEdmType(null)).ElementType.ShortQualifiedName();

            var actionSegment = oDataPath.Segments[isCollectionWide ? 1 : 2] as BoundActionPathSegment;
            var fqnActionName = actionSegment?.ActionName;

            Debug.Assert(fqnActionName != null, "fqnActionName != null");
            var actionName = fqnActionName.Substring(fqnActionName.IndexOf("_", StringComparison.Ordinal) + 1);

            var actionsContainerType = this.metadataService.GetModelMapping(this.metadataService.GetEntityType(entityName)).ActionsContainer;
            if (actionsContainerType == null)
            {
                return this.NotFound();
            }

            return await this.InvokeAction(actionsContainerType, actionName);
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

        protected object CreateResult(Type oDataModelType)
            => Activator.CreateInstance(oDataModelType);

        protected ODataQueryOptions ConstructQueryOptions()
        {
            var oDataProperties = this.Request.ODataProperties();
            var oDataPath = oDataProperties.Path;
            var model = oDataProperties.Model;

            if (oDataProperties.Path.PathTemplate == "~/entityset/key/action" || oDataProperties.Path.PathTemplate == "~/entityset/action")
            {
                var entitySetName = oDataProperties.Path.Segments[0] as EntitySetPathSegment;
                var edmType = entitySetName.GetEdmType(null);
                var type = this.metadataService.GetEntityType(((IEdmCollectionType)edmType).ElementType.ShortQualifiedName());

                return new ODataQueryOptions(new ODataQueryContext(model, type, oDataPath), this.Request);
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

                var type = this.metadataService.ODataModelAssembly.GetType(modelTypeFullName);

                return new ODataQueryOptions(new ODataQueryContext(model, type, oDataPath), this.Request);
            }
        }

        private async Task<IHttpActionResult> InvokeAction(Type actionsContainerType, string actionName, KeyValuePathSegment keySegment = null)
        {
            // create action container instance with all dependencies resolved
            var compositionRoot = ApplicationIntegration.DependencyResolver;
            var actionsContainer = compositionRoot.ResolveType(actionsContainerType);

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
            if (parametersInfo.Count(info => !info.IsOptional) > jsonParameters.Count)
            {
                return
                    this.BadRequest(
                        $"Invalid number of non-optional parameters. Expected: {parametersInfo.Length}. Got: {jsonParameters.Count}.");
            }

            var missingParameter =
                parametersInfo.FirstOrDefault(
                    parameterInfo => !jsonParameters.ContainsKey(parameterInfo.Name) && !parameterInfo.IsOptional);

            if (missingParameter != null)
            {
                return this.BadRequest($"Non-optional parameter {missingParameter.Name} not found in request body.");
            }

            // if we have a key, we can optionally bind it to the appropriate action parameter
            if (keySegment != null)
            {
                var keyValue = JsonConvert.DeserializeObject(keySegment.Value);

                // TODO: implement this
            }

            try
            {
                // adjust parameters types
                jsonParameters = jsonParameters.ToDictionary(
                    kvp => kvp.Key,
                    kvp =>
                        this.convertor.Convert(kvp.Value, parametersInfo.First(info => info.Name == kvp.Key).ParameterType));

                var result = methodInfo.InvokeWithNamedParameters(actionsContainer, jsonParameters);

                if (methodInfo.ReturnType != typeof(void))
                {
                    return this.CreateSimpleOkResponse(methodInfo.ReturnType, result);
                }

                return this.Ok();
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

        private string[] GetDBColumnsList(
            SelectExpandQueryOption queryOptions,
            Dictionary<string, string> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item]).ToArray() ?? mapping.Values.ToArray();

        private IHttpActionResult CreateSimpleOkResponse(Type dataType, object result)
            => (IHttpActionResult)this.GetSimpleOkMethod(dataType).Invoke(this, new[] { result });

        private MethodInfo GetSimpleOkMethod(Type dataType)
            => MemoryCache.Default.GetOrAddExisting(
                $"WNPController.SimpleOkMethod<{dataType.FullName}>",
            () =>
                this.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                    .MakeGenericMethod(dataType));

        private IHttpActionResult CreateOkResponse(Type oDataModelType, object result)
            => (IHttpActionResult)this.GetOkMethod(oDataModelType).Invoke(this, new[] { result });

        private IList CreateResultList(Type oDataModelType)
            => (IList)Activator.CreateInstance(this.GetGenericListType(oDataModelType));

        /// <summary>
        /// Gets the OK method for specified OData model type and adds ti to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The OK method for specified OData model type.</returns>
        private MethodInfo GetOkMethod(Type oDataModelType)
                    => MemoryCache.Default.GetOrAddExisting(
                        $"WNPController.OkMethod<{oDataModelType.FullName}>",
                            () =>
                                this.GetType()
                                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                                    .MakeGenericMethod(this.GetGenericListType(oDataModelType)));

        /// <summary>
        /// Gets the list type for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The list type for specified OData model type.</returns>
        private Type GetGenericListType(Type oDataModelType)
                    => MemoryCache.Default.GetOrAddExisting(
                        $"WNPController.List<{oDataModelType.FullName}>",
                            () => typeof(List<>).MakeGenericType(oDataModelType));
    }
}