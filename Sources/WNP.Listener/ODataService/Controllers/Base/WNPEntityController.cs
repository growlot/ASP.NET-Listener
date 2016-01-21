// <copyright file="WNPEntityController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using System;
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
    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.OData.Edm;
    using Newtonsoft.Json;
    using Persistence.Poco;
    using Persistence.WNP;
    using Repository.WNP;
    using Serilog;
    using Services.Filter;
    using Services.Query;
    using Utilities;

    /// <summary>
    /// Base class for all controllers working with WNP entities
    /// </summary>
    public class WNPEntityController : WNPController
    {
        private readonly IODataQueryHandlerFactory queryHandlerFactory;

        private readonly ODataValidationSettings defaultODataValidationSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPEntityController"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The unitOfWork.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query builder</param>
        public WNPEntityController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitOfWork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus,
            IODataQueryHandlerFactory queryHandlerFactory)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus)
        {
            this.queryHandlerFactory = queryHandlerFactory;

            this.defaultODataValidationSettings = new ODataValidationSettings()
            {
                AllowedQueryOptions =
                    AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Skip | AllowedQueryOptions.Expand,
            };
        }

        /// <summary>
        /// Handles actions bound to entities or entity collections, by redirecting request to correct method.
        /// </summary>
        /// <returns>Action results</returns>
        public async Task<IHttpActionResult> EntityActionHandler()
        {
            this.ConstructQueryOptions();
            var oDataPath = this.QueryOptions.Context.Path;

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

            var actionsContainerType =
                this.MetadataService.GetModelMapping(this.MetadataService.GetEntityType(this.EdmEntityClrType.FullName))
                    .ActionsContainer;

            if (actionsContainerType == null)
            {
                return this.NotFound();
            }

            return await this.InvokeAction(actionsContainerType, actionName, keySegment);
        }

        /// <summary>
        /// Gets this collection of entities.
        /// </summary>
        /// <returns>The collection of entities.</returns>
        public async Task<IHttpActionResult> Get()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.QueryOptions.Validate(this.defaultODataValidationSettings);

            var skip = this.QueryOptions.Skip?.Value ?? 0;
            var top = this.QueryOptions.Top?.Value ?? 10;

            var selectedFields = this.QueryOptions.SelectExpand?.RawSelect?.Split(',');
            var expands =
                this.QueryOptions.SelectExpand?.SelectExpandClause?.SelectedItems?.OfType<ExpandedNavigationSelectItem>().ToArray();

            var resultInstances =
                await this.queryHandlerFactory.MultipleResultsQueryHandler()
                    .OnType(this.EdmEntityClrType)
                    .Expand(expands?.Select(item => item.NavigationSource.EntityType().Name).ToArray())
                    .SelectFields(selectedFields)
                    .Filter(this.QueryOptions.Filter)
                    .Skip(skip)
                    .Top(top)
                    .FetchAsync();

            return this.CreateOkResponseList(resultInstances);
        }

        /// <summary>
        /// Gets this collection of navigation entities.
        /// </summary>
        /// <returns>The collection of entities.</returns>
        public async Task<IHttpActionResult> GetNavigation()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.QueryOptions.Validate(this.defaultODataValidationSettings);

            // currently, not possible to use
            var skip = this.QueryOptions.Skip?.Value ?? 0;
            var top = this.QueryOptions.Top?.Value ?? 10;

            var selectedFields = this.QueryOptions.SelectExpand?.RawSelect?.Split(',');

            var context = this.QueryOptions.Context;

            var parentKeyPathSegment = context.Path.Segments[1] as KeyValuePathSegment;
            var rawParentKey = parentKeyPathSegment?.Value;

            var navSource = context.NavigationSource as IEdmContainedEntitySet;

            // get types
            var navProperty = navSource?.NavigationProperty;

            var parentEntityType = navProperty?.DeclaringEntityType();
            var childEntityType = navProperty?.ToEntityType();

            Debug.Assert(parentEntityType != null, "parentEntityType != null");
            Debug.Assert(childEntityType != null, "childEntityType != null");

            // get types' entity models
            var parentType =
                this.MetadataService.GetEntityType(StringUtilities.Invariant($"{this.MetadataService.ODataModelNamespace}.{parentEntityType.Name}"));
            var childType =
                this.MetadataService.GetEntityType(StringUtilities.Invariant($"{this.MetadataService.ODataModelNamespace}.{childEntityType.Name}"));

            var expands =
                this.QueryOptions.SelectExpand?.SelectExpandClause?.SelectedItems?.OfType<ExpandedNavigationSelectItem>().ToArray();

            var resultInstances =
                await this.queryHandlerFactory.NavigationMultipleResultsQuery()
                    .OnTypes(parentType, childType)
                    .WithKey(rawParentKey)
                    .Expand(expands?.Select(item => item.NavigationSource.EntityType().Name).ToArray())
                    .SelectFields(selectedFields)
                    .Filter(this.QueryOptions.Filter)
                    .Skip(skip)
                    .Top(top)
                    .FetchAsync();

            return this.CreateOkResponseList(resultInstances);
        }

        /// <summary>
        /// Gets single entity using primary key specified in Url asyncronously.
        /// </summary>
        /// <returns>Single entity</returns>
        public async Task<IHttpActionResult> GetSingleSimple()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.QueryOptions.Validate(this.defaultODataValidationSettings);

            var selectedFields = this.QueryOptions.SelectExpand?.RawSelect?.Split(',');

            var keyPathSegment = this.QueryOptions.Context.Path.Segments[1] as KeyValuePathSegment;
            var rawKey = keyPathSegment?.Value;

            var expands =
                this.QueryOptions.SelectExpand?.SelectExpandClause?.SelectedItems?.OfType<ExpandedNavigationSelectItem>()
                    .ToArray();

            try
            {
                var entityInstance =
                    await this.queryHandlerFactory.SingleResultQuery()
                        .OnType(this.EdmEntityClrType)
                        .WithKey(rawKey)
                        .Expand(expands?.Select(item => item.NavigationSource.EntityType().Name).ToArray())
                        .SelectFields(selectedFields)
                        .FetchAsync();

                return this.CreateSimpleOkResponse(this.EdmEntityClrType, entityInstance);
            }
            catch (InvalidNumberOfRecordsException e)
            {
                return this.BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get single result of contained child entity set by specified parent
        /// and child keys.
        /// </summary>
        /// <returns>Single entity</returns>
        public async Task<IHttpActionResult> GetSingleByNavigation()
        {
            // constructing oData options since we can not use generic return type
            // without first generating Controller dynamically
            this.ConstructQueryOptions();
            this.QueryOptions.Validate(this.defaultODataValidationSettings);

            var selectedFields = this.QueryOptions.SelectExpand?.RawSelect?.Split(',');

            var parentKeyPathSegment = this.QueryOptions.Context.Path.Segments[1] as KeyValuePathSegment;
            var childKeyPathSegment = this.QueryOptions.Context.Path.Segments[3] as KeyValuePathSegment;

            var rawParentKey = parentKeyPathSegment?.Value;
            var rawChildKey = childKeyPathSegment?.Value;

            var expands =
                this.QueryOptions.SelectExpand?.SelectExpandClause?.SelectedItems?.OfType<ExpandedNavigationSelectItem>()
                    .ToArray();

            var navSource = this.QueryOptions.Context.NavigationSource as IEdmContainedEntitySet;

            // get types
            var parentEntityType = navSource?.NavigationProperty.DeclaringEntityType();
            var childEntityType = navSource?.NavigationProperty.ToEntityType();

            Debug.Assert(parentEntityType != null, "parentEntityType != null");
            Debug.Assert(childEntityType != null, "childEntityType != null");

            // get types' entity models
            var parentType =
                this.MetadataService.GetEntityType(StringUtilities.Invariant($"{this.MetadataService.ODataModelNamespace}.{parentEntityType.Name}"));
            var childType =
                this.MetadataService.GetEntityType(StringUtilities.Invariant($"{this.MetadataService.ODataModelNamespace}.{childEntityType.Name}"));

            try
            {
                var entityInstance =
                    await this.queryHandlerFactory.NavigationSingleResultQuery()
                        .OnTypes(parentType, childType)
                        .WithKeys(rawParentKey, rawChildKey)
                        .Expand(expands?.Select(item => item.NavigationSource.EntityType().Name).ToArray())
                        .SelectFields(selectedFields)
                        .FetchAsync();

                return this.CreateSimpleOkResponse(this.EdmEntityClrType, entityInstance);
            }
            catch (InvalidNumberOfRecordsException e)
            {
                return this.BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets the property value from delta object if it is in delta, or returns current value.
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="delta">The delta.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="currentValue">The current value.</param>
        /// <returns>Property value.</returns>
        protected static T GetChangedOrCurrent<T>(Delta delta, string propertyName, T currentValue)
        {
            if (delta == null)
            {
                throw new ArgumentNullException(nameof(delta), "Can not get property value if delta object is not specified.");
            }

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
        /// Gets the request key.
        /// </summary>
        /// <param name="modelMapping">The model mapping.</param>
        /// <param name="keyPosition">The key position.</param>
        /// <returns>The key used in request.</returns>
        protected KeyValuePair<string, object>[] GetRequestKey(MetadataEntityModel modelMapping, int keyPosition)
        {
            if (modelMapping == null)
            {
                throw new ArgumentNullException(nameof(modelMapping), "Can not get key from request if model mapping is not provided.");
            }

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

            var jsonKey = this.QueryOptions.Context.Path.Segments[keyPosition] as KeyValuePathSegment;
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

        /// <summary>
        /// Gets the request key.
        /// </summary>
        /// <param name="keyPosition">The key position.</param>
        /// <returns>The key used in request.</returns>
        protected KeyValuePair<string, object>[] GetRequestKey(int keyPosition)
        {
            var modelMapping = this.MetadataService.GetModelMapping(this.EdmEntityClrType);
            return this.GetRequestKey(modelMapping, keyPosition);
        }

        /// <summary>
        /// Gets the entity with specified fields only.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key from metadata.</param>
        /// <param name="modelMapping">The model mapping.</param>
        /// <param name="dbFieldNames">The fields that has to be selected.</param>
        /// <returns>The entity with specified fields retrieved.</returns>
        protected Task<TEntity> GetEntityAsync<TEntity>(KeyValuePair<string, object>[] key, MetadataEntityModel modelMapping, params object[] dbFieldNames)
        {
            var sql =
                Sql.Builder.Select(dbFieldNames)
                    .From(modelMapping.TableName)
                    .Where(
                        key.Select((kvp, ind) => StringUtilities.Invariant($"{modelMapping.ModelToColumnMappings[kvp.Key]}=@{ind}"))
                            .Aggregate((s, s1) => StringUtilities.Invariant($"{s} AND {s1}")),
                        key.Select(kvp => kvp.Value).ToArray());

            return ((WNPUnitOfWork)this.UnitOfWork).DbContext.FirstOrDefaultAsync<TEntity>(sql);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The entity.</returns>
        protected Task<TEntity> GetEntityAsync<TEntity>(KeyValuePair<string, object>[] key)
        {
            return this.GetEntityAsync<TEntity>(key, this.MetadataService.GetModelMapping(this.EdmEntityClrType), "*");
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
                throw;
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
            requestContent = method.MakeGenericMethod(this.EdmEntityClrType).Invoke(null, new object[] { this.GetRequestContents() });

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

            IEnumerable<MediaTypeFormatter> perRequestFormatters = odataFormatters.Select(
                (f) => f.GetPerRequestFormatterInstance(deltaType, this.Request, null));

            var edmEnityDelta = this.Request.Content.ReadAsAsync(deltaType, perRequestFormatters).Result;

            var getEntityDeltaMethod = this.EdmEntityClrType.GetMethod("GetEntityDelta");

            var converterObject = this.CreateEdmEntity();
            Delta<TEntity> entityDelta = (Delta<TEntity>)getEntityDeltaMethod.Invoke(converterObject, new object[] { edmEnityDelta });
            return entityDelta;
        }

        private string GetRequestContents()
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

        private object CreateEdmEntity(ExpandedNavigationSelectItem[] expandedItems = null)
        {
            var entityInstance = Activator.CreateInstance(this.EdmEntityClrType);
            if (expandedItems != null)
            {
                foreach (var item in expandedItems)
                {
                    var propertyName = ((NavigationPropertySegment)item.PathToNavigationProperty.FirstSegment).NavigationProperty.Name;

                    var property = this.EdmEntityClrType.GetProperty(propertyName);
                    var propInstance = Activator.CreateInstance(property.PropertyType);

                    property.SetValue(entityInstance, propInstance);
                }
            }

            return entityInstance;
        }

        private Type GetGenericListType()
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.List<{this.EdmEntityClrType.FullName}>"),
                () => typeof(List<>).MakeGenericType(this.EdmEntityClrType));
    }
}