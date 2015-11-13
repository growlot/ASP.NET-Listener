// <copyright file="WNPEntityController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using Serilog;

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Formatter;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using ApplicationService;
    using MetadataService;
    using Microsoft.OData.Edm;
    using Newtonsoft.Json;
    using Persistence.WNP;
    using Repository.WNP;
    using Services.FilterTransformer;
    using Utilities;

    /// <summary>
    /// Base class for all controllers working with WNP entities
    /// </summary>
    public class WNPEntityController : WNPController
    {
        private readonly ODataValidationSettings defaultODataValidationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPEntityController"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitofwork">The unitofwork.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        public WNPEntityController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitofwork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus)
            : base(metadataService, unitofwork, filterTransformer, actionConfigurator, commandBus)
        {
            this.defaultODataValidationSettings = new ODataValidationSettings()
            {
                AllowedQueryOptions =
                    AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Skip | AllowedQueryOptions.Expand,
            };
        }

        /// <summary>
        /// Gets this collection of entities.
        /// </summary>
        /// <returns>The collection of entities.</returns>
        public IHttpActionResult Get()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.queryOptions.Validate(this.defaultODataValidationSettings);

            var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType.Name);

            var skip = this.queryOptions.Skip?.Value ?? 0;
            var top = this.queryOptions.Top?.Value ?? 10;

            var sql =
                Sql.Builder.Select(this.GetDBColumnsList(this.queryOptions.SelectExpand, modelMapping))
                           .From($"{modelMapping.TableName}");

            var sqlWhere = this.filterTransformer.TransformFilterQueryOption(this.queryOptions.Filter);

            // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
            ConvertUtcParamsToLocalTime(sqlWhere);

            if (!string.IsNullOrWhiteSpace(sqlWhere.Clause))
            {
                sql = sql.Where(sqlWhere.Clause, sqlWhere.PositionalParameters);
            }

            var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.SkipTake<dynamic>(skip, top, sql);

            // create actual result object we will be sending over the wire
            var result = this.CreateResultList();

            foreach (var record in dbResults)
            {
                var entityInstance = this.CreateEdmEntity();

                var rawData = (IDictionary<string, object>)record;
                foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                {
                    var property = this.EdmEntityClrType.GetProperty(modelMapping.ColumnToModelMappings[key.ToUpperInvariant()]);
                    property.SetValue(entityInstance, Converters.Convert(rawData[key], property.PropertyType));
                }

                result.Add(entityInstance);
            }

            return this.CreateOkResponseList(result);
        }

        private static void ConvertUtcParamsToLocalTime(WhereClause sqlWhere)
        {
            for (int i = 0; i < sqlWhere.PositionalParameters.Length; i++)
            {
                DateTimeOffset? parameter = sqlWhere.PositionalParameters[i] as DateTimeOffset?;

                if (parameter != null)
                {
                    DateTime localTime = new DateTime(parameter.Value.ToLocalTime().Ticks);
                    DateTime localTimeAsUtc = DateTime.SpecifyKind(localTime, DateTimeKind.Utc);
                    sqlWhere.PositionalParameters[i] = (DateTimeOffset) localTimeAsUtc;
                }
            }
        }

        /// <summary>
        /// Gets this collection of navigation entities.
        /// </summary>
        /// <returns>The collection of entities.</returns>
        public IHttpActionResult GetNavigation()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.queryOptions.Validate(this.defaultODataValidationSettings);

            var model = this.metadataService.GetModelMapping(this.EdmEntityClrType.Name);

            var skip = this.queryOptions.Skip?.Value ?? 0;
            var top = this.queryOptions.Top?.Value ?? 10;

            var containedEntitySet = this.queryOptions.Context.NavigationSource as IEdmContainedEntitySet;
            if (containedEntitySet != null)
            {
                var navSource = containedEntitySet;
                var parentNavSource = navSource.ParentNavigationSource as IEdmEntitySet;
                if (parentNavSource != null)
                {
                    // get types
                    var parentEntityType = navSource.NavigationProperty.DeclaringEntityType();
                    var childEntityType = navSource.NavigationProperty.ToEntityType();

                    // get types' entity models
                    var parentEntityModel = this.metadataService.GetModelMapping(parentEntityType.Name);
                    var childEntityModel = this.metadataService.GetModelMapping(childEntityType.Name);

                    var sql = this.GetNavigationSql(parentEntityModel, childEntityModel, this.queryOptions, false);

                    var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.SkipTake<dynamic>(skip, top, sql);

                    // create actual result object we will be sending over the wire
                    var result = this.CreateResultList();

                    foreach (var record in dbResults)
                    {
                        var entityInstance = this.CreateEdmEntity();

                        var rawData = (IDictionary<string, object>)record;
                        foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                        {
                            var property = this.EdmEntityClrType.GetProperty(model.ColumnToModelMappings[key.ToUpperInvariant()]);
                            property.SetValue(entityInstance, Converters.Convert(rawData[key], property.PropertyType));
                        }

                        result.Add(entityInstance);
                    }

                    return this.CreateOkResponseList(result);
                }
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Handles actions bound to entities or entity collections, by redirecting request to correct method.
        /// </summary>
        /// <returns>Action results</returns>
        public async Task<IHttpActionResult> EntityActionHandler()
        {
            this.ConstructQueryOptions();
            var oDataPath = this.queryOptions.Context.Path;

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

            KeyValuePathSegment keySegment = null;
            if (!isCollectionWide)
            {
                keySegment = oDataPath.Segments[1] as KeyValuePathSegment;
            }

            return await this.InvokeAction(actionsContainerType, actionName, keySegment);
        }

        /// <summary>
        /// Gets single entity using primary key specified in Url.
        /// </summary>
        /// <returns>Single entity</returns>
        public IHttpActionResult GetSingleSimple()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.queryOptions.Validate(this.defaultODataValidationSettings);

            var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType);

            var orderedKey = GetRequestKey(this.queryOptions, modelMapping, 1);

            var sql =
                Sql.Builder.Select(this.GetDBColumnsList(this.queryOptions.SelectExpand, modelMapping))
                    .From($"{modelMapping.TableName}")
                    .Where(
                        orderedKey.Select((kvp, ind) => $"{modelMapping.ModelToColumnMappings[kvp.Key]}=@{ind}")
                            .Aggregate((s, s1) => $"{s} AND {s1}"),
                        orderedKey.Select(kvp => kvp.Value).ToArray());

            var dbResults = ((WNPUnitOfWork) this.unitOfWork).DbContext.Fetch<dynamic>(sql);

            if (dbResults.Count > 1)
            {
                return this.BadRequest("Request returned more than 1 record.");
            }

            if (dbResults.Count == 0)
            {
                return this.NotFound();
            }

            var entityInstance = this.CreateEdmEntity();

            var rawData = (IDictionary<string, object>) dbResults[0];
            foreach (var kk in rawData.Keys.Where(k => k != "peta_rn"))
            {
                var property = this.EdmEntityClrType.GetProperty(modelMapping.ColumnToModelMappings[kk.ToUpperInvariant()]);
                property.SetValue(entityInstance, Converters.Convert(rawData[kk], property.PropertyType));
            }

            return this.CreateSimpleOkResponse(this.EdmEntityClrType, entityInstance);
        }

        /// <summary>
        /// Get single result of contained child entity set by specified parent
        /// and child keys.
        /// </summary>
        /// <returns>Single entity</returns>
        public IHttpActionResult GetSingleByNavigation()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.queryOptions.Validate(this.defaultODataValidationSettings);

            var containedEntitySet = this.queryOptions.Context.NavigationSource as IEdmContainedEntitySet;
            if (containedEntitySet != null)
            {
                var navSource = containedEntitySet;
                var parentNavSource = navSource.ParentNavigationSource as IEdmEntitySet;
                if (parentNavSource != null)
                {
                    // get types
                    var parentEntityType = navSource.NavigationProperty.DeclaringEntityType();
                    var childEntityType = navSource.NavigationProperty.ToEntityType();

                    // get types' entity models
                    var parentEntityModel = this.metadataService.GetModelMapping(parentEntityType.Name);
                    var childEntityModel = this.metadataService.GetModelMapping(childEntityType.Name);

                    // check if navSource is a Collection so we know we should look into ManyRelations
                    // TODO: should we do this? How 1-1 relationship is defined?
                    if (navSource.Type.TypeKind == EdmTypeKind.Collection)
                    {
                        var sql = this.GetNavigationSql(parentEntityModel, childEntityModel, this.queryOptions, true);

                        Log.Debug("Generated SQL:{GeneratedSQL}\n\nParameters: {Parameters}\n", sql.SQL, sql.Arguments);

                        var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.Fetch<dynamic>(sql);
                        if (dbResults.Count > 1)
                        {
                            return this.BadRequest("Request returned more than 1 record.");
                        }

                        if (dbResults.Count == 0)
                        {
                            return this.NotFound();
                        }

                        // this should be instance of the requested child entity
                        var entityInstance = this.CreateEdmEntity();

                        var rawData = (IDictionary<string, object>)dbResults[0];
                        foreach (var kk in rawData.Keys.Where(k => k != "peta_rn"))
                        {
                            var property = this.EdmEntityClrType.GetProperty(childEntityModel.ColumnToModelMappings[kk.ToUpperInvariant()]);
                            property.SetValue(entityInstance, Converters.Convert(rawData[kk], property.PropertyType));
                        }

                        return this.CreateSimpleOkResponse(this.EdmEntityClrType, entityInstance);
                    }
                }

                return this.BadRequest("Parent EntitySet must be IEdmEntitySet");
            }

            return this.BadRequest("Only contained sets are currently supported");
        }

        private static KeyValuePair<string, object>[] GetRequestKey(ODataQueryOptions queryOptions, MetadataEntityModel modelMapping, int keyPosition)
        {
            var entityConfig = modelMapping.EntityConfiguration;

            bool hasCompositeKey;
            if (entityConfig.IsOwnerSpecific)
            {
                hasCompositeKey = entityConfig.Key?.Count() > 2;
            }
            else
            {
                hasCompositeKey = entityConfig.Key?.Count() > 1;
            }

            var jsonKey = queryOptions.Context.Path.Segments[keyPosition] as KeyValuePathSegment;
            if (jsonKey == null)
            {
                throw new ArgumentException("Invalid key specified");
            }

            var key = new Dictionary<string, object>();
            if (hasCompositeKey)
            {
                key = jsonKey.ToCompositeKeyDictionary();
            }
            else
            {
                if (entityConfig.IsOwnerSpecific)
                {
                    key.Add(
                        modelMapping.ColumnToModelMappings[entityConfig.Key.ToArray()[1].ToUpperInvariant()],
                        JsonConvert.DeserializeObject(jsonKey.Value));
                }
                else
                {
                    key.Add(
                        modelMapping.ColumnToModelMappings[entityConfig.Key.ToArray()[0].ToUpperInvariant()],
                        JsonConvert.DeserializeObject(jsonKey.Value));
                }
            }

            return key.ToArray();
        }

        private Sql GetNavigationSql(MetadataEntityModel parentEntityModel, MetadataEntityModel childEntityModel, ODataQueryOptions queryOptions, bool byChildKey)
        {
            var childTable = childEntityModel.TableName;
            var relConfig =
                parentEntityModel.EntityConfiguration.ManyRelations.FirstOrDefault(
                    ri => ri.TargetTableName == childTable);

            if (relConfig == null)
            {
                throw new ArgumentException($"Relationship configuration not found between {parentEntityModel.TableName} and {childTable}");
            }

            // make join
            var parentTable = parentEntityModel.TableName;
            var onClause = relConfig.MatchOn
                .Select(m => $"{parentTable}.{m.SourceColumn} = {childTable}.{m.TargetColumn}")
                .Aggregate((m1, m2) => $"{m1} AND {m2}");

            var dbColumnsList = this.GetDBColumnsList(queryOptions.SelectExpand, childEntityModel);

            var parentKey = GetRequestKey(queryOptions, parentEntityModel, 1);
            KeyValuePair<string, object>[] childKey = null;

            object[] filterArgs = null;

            var whereClause = $"{GenerateWhereBodyForKey(parentKey, parentTable, parentEntityModel)}";
            if (byChildKey)
            {
                childKey = GetRequestKey(queryOptions, childEntityModel, 3);
                whereClause +=
                    $" AND {GenerateWhereBodyForKey(childKey, childTable, childEntityModel, parentKey.Length)}";
            }
            else
            {
                var sqlFilter = this.filterTransformer.TransformFilterQueryOption(queryOptions.Filter, parentKey.Length);

                // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
                ConvertUtcParamsToLocalTime(sqlFilter);

                if (!string.IsNullOrWhiteSpace(sqlFilter.Clause))
                {
                    whereClause += $" AND {sqlFilter.Clause}";
                    filterArgs = sqlFilter.PositionalParameters;
                }
            }

            var whereArgs =
                parentKey.Select(kvp => kvp.Value).ToList();

            whereArgs.AddRange(childKey?.Select(kvp => kvp.Value) ?? new object[] { });
            whereArgs.AddRange(filterArgs ?? new object[] { });

            var sql =
                Sql.Builder.Select(dbColumnsList)
                    .From($"{parentTable}")
                    .InnerJoin($"{childTable}")
                    .On(onClause)
                    .Where(whereClause, whereArgs.ToArray());

            return sql;
        }

        private static string GenerateWhereBodyForKey(KeyValuePair<string, object>[] key, string table, MetadataEntityModel model, int offset = 0)
        {
            return key.Select(
                (kvp, ind) => $"{table}.{model.ModelToColumnMappings[kvp.Key]}=@{ind + offset}")
                .Aggregate((s, s1) => $"{s} AND {s1}");
        }

        /// <summary>
        /// Gets the property value from delta object if it is in delta, or returns current value.
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <typeparam name="TEntity">The type of the delta entity.</typeparam>
        /// <param name="delta">The delta.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="currentValue">The current value.</param>
        /// <returns>Property value.</returns>
        protected static T GetChangedOrCurrent<T, TEntity>(Delta<TEntity> delta, string propertyName, T currentValue)
                    where TEntity : class
        {
            if (delta.GetChangedPropertyNames().Contains(propertyName))
            {
                object value;
                delta.TryGetPropertyValue(propertyName, out value);
                if (value != null)
                {
                    return (T)value;
                }
            }

            return currentValue;
        }

        /// <summary>
        /// Creates the typed OK response from specified list of result objects.
        /// </summary>
        /// <param name="result">The list of result objects.</param>
        /// <returns>The typed OK result.</returns>
        protected IHttpActionResult CreateOkResponseList(object result)
                    => (IHttpActionResult)this.GetOkMethodList().Invoke(this, new[] { result });

        /// <summary>
        /// Prepares the typed Updated response from specified result object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The OData response for updated entity.</returns>
        protected Task<IHttpActionResult> PrepareUpdatedResponse<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Update failed. Updated entity not found."));
            }

            // create actual object that was sent over the wire
            var responseContent = this.CreateEdmEntity();

            var setFromEntityMethod = this.EdmEntityClrType.GetMethod("SetFromEntity");
            setFromEntityMethod.Invoke(responseContent, new object[] { entity });

            var result = (IHttpActionResult)this.GetUpdatedMethod().Invoke(this, new[] { responseContent });
            return Task.FromResult(result);
        }

        /// <summary>
        /// Prepares the Created response for specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The OData response for newly created entity.</returns>
        protected Task<IHttpActionResult> PrepareCreatedResponse<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Create failed. Persisted entity not found."));
            }

            // create actual object that was sent over the wire
            var responseContent = this.CreateEdmEntity();

            var setFromEntityMethod = this.EdmEntityClrType.GetMethod("SetFromEntity");
            setFromEntityMethod.Invoke(responseContent, new object[] { entity });

            var result = (IHttpActionResult)this.GetCreatedMethod().Invoke(this, new[] { responseContent });
            return Task.FromResult(result);
        }

        /// <summary>
        /// Prepares the Get response for single entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The OData response for single entity retrieval.</returns>
        protected Task<IHttpActionResult> PrepareGetResponse<TEntity>(TEntity entity)
        {
            if (entity == null)
            {
                return Task.FromResult<IHttpActionResult>(this.NotFound());
            }

            // create actual object that was sent over the wire
            var responseContent = this.CreateEdmEntity();

            var setFromEntityMethod = this.EdmEntityClrType.GetMethod("SetFromEntity");
            setFromEntityMethod.Invoke(responseContent, new object[] { entity });

            var result = (IHttpActionResult)this.GetOkMethod().Invoke(this, new[] { responseContent });
            return Task.FromResult(result);
        }

        /// <summary>
        /// Gets the statically typed entity from request.
        /// </summary>
        /// <typeparam name="TEntity">The static type of the entity.</typeparam>
        /// <returns>The statically typed entity.</returns>
        protected TEntity GetRequestEntity<TEntity>()
        {
            // create actual object that was sent over the wire
            var requestContent = this.CreateEdmEntity();

            var method = typeof(JsonConvert).GetGenericMethod("DeserializeObject", new Type[] { typeof(string) });
            requestContent = method.MakeGenericMethod(this.EdmEntityClrType).Invoke(null, new object[] { this.GetRequestContents(this.Request) });

            var getEntityMethod = this.EdmEntityClrType.GetMethod("GetEntity");

            TEntity entity = (TEntity)getEntityMethod.Invoke(requestContent, new object[] { });
            return entity;
        }

        /// <summary>
        /// Gets the statically typed entity delta from request.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// The statically typed entity delta.
        /// </returns>
        protected Delta<TEntity> GetRequestEntityDelta<TEntity>()
            where TEntity : class, new()
        {
            var odataFormatters = ODataMediaTypeFormatters.Create();
            var deltaType = typeof(Delta<>).MakeGenericType(this.EdmEntityClrType);
            var delta = this.CreateEdmEntityDelta();

            IEnumerable<MediaTypeFormatter> perRequestFormatters = odataFormatters.Select(
                (f) => f.GetPerRequestFormatterInstance(deltaType, this.Request, null));

            var edmEnityDelta = this.Request.Content.ReadAsAsync(deltaType, perRequestFormatters).Result;

            var getEntityDeltaMethod = this.EdmEntityClrType.GetMethod("GetEntityDelta");

            var converterObject = this.CreateEdmEntity();
            Delta<TEntity> entityDelta = (Delta<TEntity>)getEntityDeltaMethod.Invoke(converterObject, new object[] { edmEnityDelta });
            return entityDelta;
        }

        private string GetRequestContents(HttpRequestMessage request)
            => this.Request.Content.ReadAsStringAsync().Result;

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions, MetadataEntityModel model)
        {
            var fieldList = queryOptions?.RawSelect.Split(',') ?? model.ModelToColumnMappings.Keys.ToArray();
            return fieldList.Select(item => $"{model.TableName}.{model.ModelToColumnMappings[item]}").ToArray();
        }

        private MethodInfo GetOkMethodList()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.OkListMethod<{this.EdmEntityClrType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Ok")
                        .MakeGenericMethod(this.GetGenericListType()));

        private MethodInfo GetOkMethod()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.OkMethod<{this.EdmEntityClrType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Ok")
                        .MakeGenericMethod(this.EdmEntityClrType));

        private MethodInfo GetUpdatedMethod()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.UpdatedMethod<{this.EdmEntityClrType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Updated")
                        .MakeGenericMethod(this.EdmEntityClrType));

        private MethodInfo GetCreatedMethod()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.CreatedMethod<{this.EdmEntityClrType.FullName}>"),
                () => this.GetType()
                    .GetGenericMethod("Created", new Type[] { null })
                    .MakeGenericMethod(this.EdmEntityClrType));

        private IList CreateResultList()
            => (IList)Activator.CreateInstance(this.GetGenericListType());

        private object CreateEdmEntity()
            => Activator.CreateInstance(this.EdmEntityClrType);

        private object CreateEdmEntityDelta()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.List<{this.EdmEntityClrType.FullName}>"),
                () => typeof(Delta<>).MakeGenericType(this.EdmEntityClrType));

        private Type GetGenericListType()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.List<{this.EdmEntityClrType.FullName}>"),
                () => typeof(List<>).MakeGenericType(this.EdmEntityClrType));
    }
}