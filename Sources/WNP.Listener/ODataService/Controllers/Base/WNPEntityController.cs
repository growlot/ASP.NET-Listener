﻿// <copyright file="WNPEntityController.cs" company="Advanced Metering Services LLC">
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
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using MetadataService;
    using Microsoft.OData.Edm;
    using Newtonsoft.Json;
    using Persistence.WNP;
    using Repository.WNP;
    using Services.FilterTransformer;
    using Utilities;
    using System.Web;
    using ApplicationService;

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

            var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.SkipTake<dynamic>(skip, top, sql);

            foreach (var record in dbResults)
            {
                var entityInstance = this.CreateResult(oDataModelType);

                var rawData = (IDictionary<string, object>)record;
                foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                {
                    var property = oDataModelType.GetProperty(modelMapping.ColumnToModelMappings[key.ToUpperInvariant()]);
                    property.SetValue(entityInstance, Converters.Convert(rawData[key], property.PropertyType));
                }

                result.Add(entityInstance);
            }

            return this.CreateOkResponseList(oDataModelType, result);
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

        public abstract string GetEntityTableName();

        /// <summary>
        /// Creates the typed OK (HTTP 200) response from specified list of result objects.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <param name="result">The list of result objects.</param>
        /// <returns>The typed HTTP 200 result.</returns>
        protected IHttpActionResult CreateOkResponseList(Type oDataModelType, object result)
                    => (IHttpActionResult)this.GetOkMethodList(oDataModelType).Invoke(this, new[] { result });

        /// <summary>
        /// Creates the typed OK (HTTP 200) response from specified result object.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <param name="result">The result object.</param>
        /// <returns>The typed HTTP 200 result.</returns>
        protected IHttpActionResult CreateOkResponse(Type oDataModelType, object result)
                    => (IHttpActionResult)this.GetOkMethod(oDataModelType).Invoke(this, new[] { result });

        /// <summary>
        /// Creates the typed Updated response from specified result object.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <param name="result">The result object.</param>
        /// <returns>The typed Updated result.</returns>
        protected IHttpActionResult CreateUpdatedResponse(Type oDataModelType, object result)
                    => (IHttpActionResult)this.GetUpdatedMethod(oDataModelType).Invoke(this, new[] { result });

        /// <summary>
        /// Creates the typed Created response from specified result object.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <param name="result">The result object.</param>
        /// <returns>The typed Updated result.</returns>
        protected IHttpActionResult CreateCreatedResponse(Type oDataModelType, object result)
                    => (IHttpActionResult)this.GetCreatedMethod(oDataModelType).Invoke(this, new[] { result });

        /// <summary>
        /// Gets the statically typed entity from requet.
        /// </summary>
        /// <typeparam name="TEntity">The static type of the entity.</typeparam>
        /// <param name="oDataModelType">The dynamic type of the OData entity.</param>
        /// <returns>The statically typed entity.</returns>
        protected TEntity GetRequestEntity<TEntity>(Type oDataModelType)
        {
            // create actual object that was sent over the wire
            var requestContent = this.CreateResult(oDataModelType);

            var method = typeof(JsonConvert).GetGenericMethod("DeserializeObject", new Type[] { typeof(string) });
            requestContent = method.MakeGenericMethod(oDataModelType).Invoke(null, new object[] { this.GetRequestContents(this.Request) });

            var getEntityMethod = oDataModelType.GetMethod("GetEntity");

            TEntity site = (TEntity)getEntityMethod.Invoke(requestContent, new object[] { });
            return site;
        }

        private string GetRequestContents(HttpRequestMessage request)
            => this.Request.Content.ReadAsStringAsync().Result;

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions, Dictionary<string, string> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item]).ToArray() ?? mapping.Values.ToArray();

        /// <summary>
        /// Gets the list type for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The list type for specified OData model type.</returns>
        private Type GetGenericListType(Type oDataModelType)
                    => MemoryCache.Default.GetOrAddExisting(
                        StringUtilities.Invariant($"WNPController.List<{oDataModelType.FullName}>"),
                        () => typeof(List<>).MakeGenericType(oDataModelType));

        /// <summary>
        /// Gets the OK method for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The OK method for specified OData model type.</returns>
        private MethodInfo GetOkMethodList(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.OkListMethod<{oDataModelType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Ok")
                        .MakeGenericMethod(this.GetGenericListType(oDataModelType)));

        /// <summary>
        /// Gets the OK method for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The OK method for specified OData model type.</returns>
        private MethodInfo GetOkMethod(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.OkMethod<{oDataModelType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Ok")
                        .MakeGenericMethod(oDataModelType));

        /// <summary>
        /// Gets the Updated method for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The OK method for specified OData model type.</returns>
        private MethodInfo GetUpdatedMethod(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.UpdatedMethod<{oDataModelType.FullName}>"),
                () => this.GetType()
                        .GetGenericMethod("Updated")
                        .MakeGenericMethod(oDataModelType));


        private MethodInfo GetCreatedMethod(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting(
                StringUtilities.Invariant($"WNPController.CreatedMethod<{oDataModelType.FullName}>"),
                () => this.GetType()
                    .GetGenericMethod("Created", new Type[] { null })
                    .MakeGenericMethod(oDataModelType));

        private IList CreateResultList(Type oDataModelType)
            => (IList) Activator.CreateInstance(this.GetGenericListType(oDataModelType));
    }
}