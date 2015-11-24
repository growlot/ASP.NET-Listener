// <copyright file="WNPEntityController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
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
    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.OData.Edm;
    using Newtonsoft.Json;
    using Persistence.WNP;
    using Repository.WNP;
    using Serilog;
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

            var dbColumnList = new DbColumnList(this.queryOptions.SelectExpand, modelMapping);

            var sql =
                Sql.Builder.Select(dbColumnList.GetQueryColumnList())
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
                    var property = this.EdmEntityClrType.GetProperty(dbColumnList.GetModelColumnByDbQueryName(key.ToUpperInvariant()));
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
                    var dbColumnList = sql.Item2;

                    var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.SkipTake<dynamic>(skip, top, sql.Item1);

                    // create actual result object we will be sending over the wire
                    var result = this.CreateResultList();

                    foreach (var record in dbResults)
                    {
                        var entityInstance = this.CreateEdmEntity();

                        var rawData = (IDictionary<string, object>)record;
                        foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                        {
                            var property =
                                this.EdmEntityClrType.GetProperty(
                                    dbColumnList.GetModelColumnByDbQueryName(key.ToUpperInvariant()));

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

            BoundActionPathSegment actionSegment = null;
            KeyValuePathSegment keySegment = null;
            switch (oDataPath.PathTemplate)
            {
                case "~/entityset/action":
                    actionSegment = oDataPath.Segments[1] as BoundActionPathSegment;
                    break;

                case "~/entityset/key/action":
                    actionSegment = oDataPath.Segments[2] as BoundActionPathSegment;
                    keySegment = oDataPath.Segments[1] as KeyValuePathSegment;
                    break;

                case "~/entityset/key/navigation/action":
                    actionSegment = oDataPath.Segments[3] as BoundActionPathSegment;
                    break;

                case "~/entityset/key/navigation/key/action":
                    actionSegment = oDataPath.Segments[4] as BoundActionPathSegment;
                    break;
            }

            var fqnActionName = actionSegment?.ActionName;

            Debug.Assert(fqnActionName != null, "fqnActionName != null");
            var actionName = fqnActionName.Substring(fqnActionName.LastIndexOf(".", StringComparison.Ordinal) + 1);

            var actionsContainerType = this.metadataService.GetModelMapping(this.metadataService.GetEntityType(this.EdmEntityClrType.FullName)).ActionsContainer;
            if (actionsContainerType == null)
            {
                return this.NotFound();
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

            var model = this.metadataService.GetModelMapping(this.EdmEntityClrType);

            var orderedKey = this.GetRequestKey(model, 1);
            var expands =
                this.queryOptions.SelectExpand?.SelectExpandClause.SelectedItems.OfType<ExpandedNavigationSelectItem>();

            var expandedItems = expands as ExpandedNavigationSelectItem[] ?? expands?.ToArray();

            var relatedEntityModels =
                expandedItems?.Select(
                    item => this.metadataService.GetModelMapping(item.NavigationSource.EntityType().Name)).ToArray();

            var dbColumnList = new DbColumnList(
                this.queryOptions.SelectExpand,
                model,
                relatedEntityModels);

            var sql =
                Sql.Builder.Select(dbColumnList.GetQueryColumnList())
                    .From($"{model.TableName}");

            var modelConfig = model.EntityConfiguration;
            var parentTable = modelConfig.TableName;

            if (expandedItems != null)
            {
                foreach (var expand in expandedItems)
                {
                    var entityToExpand = expand.NavigationSource.EntityType().Name;
                    var entityModel = this.metadataService.GetModelMapping(entityToExpand);

                    var relConfig = modelConfig.Relations.FirstOrDefault(
                        information => information.TargetTableName == entityModel.TableName);

                    var onClause = relConfig.MatchOn
                        .Select(m => $"{parentTable}.{m.SourceColumn} = {entityModel.TableName}.{m.TargetColumn}")
                        .Aggregate((m1, m2) => $"{m1} AND {m2}");

                    sql = sql.LeftJoin(entityModel.TableName).On(onClause);
                }
            }

            sql =
                sql.Where(
                    orderedKey.Select((kvp, ind) => $"{model.TableName}.{model.ModelToColumnMappings[kvp.Key]}=@{ind}")
                        .Aggregate((s, s1) => $"{s} AND {s1}"), orderedKey.Select(kvp => kvp.Value).ToArray());

            var dbContext = ((WNPUnitOfWork)this.unitOfWork).DbContext;

            var dbResults = dbContext.Fetch<dynamic>(sql);

            if (expandedItems == null && dbResults.Count > 1)
            {
                return this.BadRequest("Request returned more than 1 record.");
            }

            if (dbResults.Count == 0)
            {
                return this.NotFound();
            }

            var entityInstance = this.CreateEdmEntity(expandedItems);

            foreach (var dbResult in dbResults)
            {
                var rawData = (IDictionary<string, object>)dbResult;
                var expandInstances = new Dictionary<Type, object>();
                var emptyRelations = new List<string>();

                relatedEntityModels?.Map(
                    entityModel =>
                        {
                            var colList = dbColumnList.GetQueryColumnListForModel(entityModel);
                            if (rawData.Where(kvp => colList.Contains(kvp.Key)).All(kvp => kvp.Value == null))
                            {
                                emptyRelations.Add(entityModel.ClassName);
                            }
                        });

                foreach (var kk in rawData.Keys.Where(k => k != "peta_rn"))
                {
                    var entityModel = dbColumnList.GetEntityModelByDbQueryName(kk);
                    var fieldName = dbColumnList.GetModelColumnByDbQueryName(kk);

                    if (entityModel == model)
                    {
                        var property = this.EdmEntityClrType.GetProperty(fieldName);
                        property.SetValue(entityInstance, Converters.Convert(rawData[kk], property.PropertyType));
                    }
                    else
                    {
                        // all values for this relation is null, so don't try to map
                        if (emptyRelations.Contains(entityModel.ClassName))
                        {
                            continue;
                        }

                        var clrType =
                            this.metadataService.GetEntityType(
                                $"{this.metadataService.ODataModelNamespace}.{entityModel.ClassName}");

                        object instance = null;
                        if (expandInstances.ContainsKey(clrType))
                        {
                            instance = expandInstances[clrType];
                        }
                        else
                        {
                            instance = Activator.CreateInstance(clrType);
                            expandInstances.Add(clrType, instance);
                        }

                        var property = clrType.GetProperty(fieldName);
                        property.SetValue(instance, Converters.Convert(rawData[kk], property.PropertyType));
                    }
                }

                if (expandedItems != null)
                {
                    foreach (var item in expandedItems)
                    {
                        var clrType = this.metadataService.GetEntityType(item.NavigationSource.EntityType().ShortQualifiedName());

                        if (expandInstances.ContainsKey(clrType))
                        {
                            var navProperty = (NavigationPropertySegment)item.PathToNavigationProperty.FirstSegment;
                            var singleResult = navProperty.EdmType.TypeKind != EdmTypeKind.Collection;

                            var propertyName = navProperty.NavigationProperty.Name;
                            var expandProperty = this.EdmEntityClrType.GetProperty(propertyName);

                            if (singleResult)
                            {
                                expandProperty.SetValue(entityInstance, expandInstances[clrType]);
                            }
                            else
                            {
                                var expandPropertyInstance = expandProperty.GetMethod.Invoke(entityInstance, new object[] { });
                                var addMethod = expandPropertyInstance.GetType().GetMethod("Add");
                                addMethod.Invoke(expandPropertyInstance, new object[] { expandInstances[clrType] });
                            }
                        }
                    }
                }
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
                        var dbColumnList = sql.Item2;

                        Log.Debug("Generated SQL:{GeneratedSQL}\n\nParameters: {Parameters}\n", sql.Item1.SQL, sql.Item1.Arguments);

                        var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.Fetch<dynamic>(sql.Item1);
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
                            var property =
                                this.EdmEntityClrType.GetProperty(
                                    dbColumnList.GetModelColumnByDbQueryName(kk.ToUpperInvariant()));

                            property.SetValue(entityInstance, Converters.Convert(rawData[kk], property.PropertyType));
                        }

                        return this.CreateSimpleOkResponse(this.EdmEntityClrType, entityInstance);
                    }
                }

                return this.BadRequest("Parent EntitySet must be IEdmEntitySet");
            }

            return this.BadRequest("Only contained sets are currently supported");
        }

        /// <summary>
        /// Gets the request key.
        /// </summary>
        /// <param name="modelMapping">The model mapping.</param>
        /// <param name="keyPosition">The key position.</param>
        /// <returns>The key used in request.</returns>
        protected KeyValuePair<string, object>[] GetRequestKey(MetadataEntityModel modelMapping, int keyPosition)
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

            var jsonKey = this.queryOptions.Context.Path.Segments[keyPosition] as KeyValuePathSegment;
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
                key.Add(
                    modelMapping.ColumnToModelMappings[entityConfig.Key.ToArray()[0].ToUpperInvariant()],
                    JsonConvert.DeserializeObject(jsonKey.Value));
            }

            return key.ToArray();
        }

        private Tuple<Sql, DbColumnList> GetNavigationSql(MetadataEntityModel parentEntityModel, MetadataEntityModel childEntityModel, ODataQueryOptions queryOptions, bool byChildKey)
        {
            var childTable = childEntityModel.TableName;
            var relConfig =
                parentEntityModel.EntityConfiguration.Relations.FirstOrDefault(
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

            var dbColumnsList = new DbColumnList(queryOptions.SelectExpand, childEntityModel);

            var parentKey = this.GetRequestKey(parentEntityModel, 1);
            KeyValuePair<string, object>[] childKey = null;

            object[] filterArgs = null;

            var whereClause = $"{GenerateWhereBodyForKey(parentKey, parentTable, parentEntityModel)}";
            if (byChildKey)
            {
                childKey = this.GetRequestKey(childEntityModel, 3);
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
                Sql.Builder.Select(dbColumnsList.GetQueryColumnList())
                    .From($"{parentTable}")
                    .InnerJoin($"{childTable}")
                    .On(onClause)
                    .Where(whereClause, whereArgs.ToArray());

            return new Tuple<Sql, DbColumnList>(sql, dbColumnsList);
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
            try
            {
            var setFromEntityMethod = this.EdmEntityClrType.GetMethod("SetFromEntity");
            setFromEntityMethod.Invoke(responseContent, new object[] { entity });
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

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

        private object CreateEdmEntity(ExpandedNavigationSelectItem[] expandedItems = null)
        {
            var entityInstance = Activator.CreateInstance(this.EdmEntityClrType);
            if (expandedItems != null)
            {
                foreach (var item in expandedItems)
                {
                    var propertyName = ((NavigationPropertySegment)item.PathToNavigationProperty.FirstSegment).NavigationProperty.Name;
                    //var propertyName = item.NavigationSource.Name;

                    var property = this.EdmEntityClrType.GetProperty(propertyName);
                    var propInstance = Activator.CreateInstance(property.PropertyType);

                    property.SetValue(entityInstance, propInstance);
                }
            }

            return entityInstance;
        }

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