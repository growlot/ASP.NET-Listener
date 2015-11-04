// <copyright file="WNPEntityController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

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

    public abstract class WNPEntityController : WNPController, IBoundActionsContainer
    {
        private readonly ODataValidationSettings defaultODataValidationSettings;


        protected WNPEntityController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitofwork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus,
            CurrentUnitOfWork test = null)
            : base(metadataService, unitofwork, filterTransformer, actionConfigurator, commandBus, test)
        {
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

            var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType.Name);

            var skip = queryOptions.Skip?.Value ?? 0;
            var top = queryOptions.Top?.Value ?? 10;

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

            KeyValuePathSegment keySegment = null;
            if (!isCollectionWide)
            {
                keySegment = oDataPath.Segments[1] as KeyValuePathSegment;
            }

            return await this.InvokeAction(actionsContainerType, actionName, keySegment);
        }

        /// <inheritdoc/>
        public abstract string GetEntityTableName();

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

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions, Dictionary<string, string> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item]).ToArray() ?? mapping.Values.ToArray();

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