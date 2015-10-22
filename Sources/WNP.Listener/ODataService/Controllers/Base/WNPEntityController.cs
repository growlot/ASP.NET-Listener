﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.ODataService.Actions;
using AMSLLC.Listener.ODataService.Services;
using AMSLLC.Listener.ODataService.Services.FilterTransformer;
using AMSLLC.Listener.Persistence;
using AMSLLC.Listener.Utilities;
using Microsoft.OData.Edm;

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    public abstract class WNPEntityController : WNPController, IBoundActionsContainer
    {
        private readonly ODataValidationSettings defaultODataValidationSettings;

        protected WNPEntityController(IMetadataService metadataService, WNPDBContext dbContext,
            IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator)
        {
            defaultODataValidationSettings = new ODataValidationSettings()
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
            var queryOptions = ConstructQueryOptions();
            queryOptions.Validate(defaultODataValidationSettings);

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;

            var modelMapping = metadataService.GetModelMapping(oDataModelType.Name);

            var skip = queryOptions.Skip?.Value ?? 0;
            var top = queryOptions.Top?.Value ?? 10;

            // create actual result object we will be sending over the wire
            var result = this.CreateResultList(oDataModelType);

            var sql =
                Sql.Builder.Select(GetDBColumnsList(queryOptions.SelectExpand, modelMapping.ModelToColumnMappings))
                           .From($"{modelMapping.TableName}");

            var sqlWhere = filterTransformer.TransformFilterQueryOption(queryOptions.Filter);

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
                sql = sql.Where(sqlWhere.Clause, sqlWhere.PositionalParameters);

            var dbResults = dbContext.SkipTake<dynamic>(skip, top, sql);

            foreach (var record in dbResults)
            {
                var entityInstance = this.CreateResult(oDataModelType);

                var rawData = (IDictionary<string, object>)record;
                foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                {
                    var property = oDataModelType.GetProperty(modelMapping.ColumnToModelMappings[key.ToLowerInvariant()]);
                    property.SetValue(entityInstance, convertor.Convert(rawData[key], property.PropertyType));
                }

                result.Add(entityInstance);
            }

            return CreateOkResponse(oDataModelType, result);
        }

        public async Task<IHttpActionResult> EntityActionHandler()
        {
            var queryOptions = ConstructQueryOptions();
            var oDataPath = queryOptions.Context.Path;

            var isCollectionWide = oDataPath.PathTemplate == "~/entityset/action";

            var entitySetSegment = oDataPath.Segments[0] as EntitySetPathSegment;

            Debug.Assert(entitySetSegment != null, "entitySetSegment != null");
            var entityName = ((IEdmCollectionType)entitySetSegment.GetEdmType(null)).ElementType.ShortQualifiedName();

            var actionSegment = oDataPath.Segments[isCollectionWide ? 1 : 2] as BoundActionPathSegment;
            var fqnActionName = actionSegment?.ActionName;

            Debug.Assert(fqnActionName != null, "fqnActionName != null");
            var actionName = fqnActionName.Substring(fqnActionName.IndexOf("_", StringComparison.Ordinal) + 1);

            var actionsContainerType = metadataService.GetModelMapping(metadataService.GetEntityType(entityName)).ActionsContainer;
            if (actionsContainerType == null)
                return NotFound();

            KeyValuePathSegment keySegment = null;
            if (!isCollectionWide)
                keySegment = oDataPath.Segments[1] as KeyValuePathSegment;

            return await InvokeAction(actionsContainerType, actionName, keySegment);
        }

        public abstract string GetEntityTableName();

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions,
            Dictionary<string, string> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item]).ToArray() ?? mapping.Values.ToArray();

        /// <summary>
        /// Gets the list type for specified OData model type and adds it to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The list type for specified OData model type.</returns>
        private Type GetGenericListType(Type oDataModelType)
                    => MemoryCache.Default.GetOrAddExisting($"WNPController.List<{oDataModelType.FullName}>",
                            () => typeof(List<>).MakeGenericType(oDataModelType));

        /// <summary>
        /// Gets the OK method for specified OData model type and adds ti to the cash for faster future retrieval.
        /// </summary>
        /// <param name="oDataModelType">Type of the OData model.</param>
        /// <returns>The OK method for specified OData model type.</returns>
        private MethodInfo GetOkMethod(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting($"WNPController.OkMethod<{oDataModelType.FullName}>",
                () =>
                    GetType()
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                        .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                        .MakeGenericMethod(GetGenericListType(oDataModelType)));

        private IHttpActionResult CreateOkResponse(Type oDataModelType, object result)
            => (IHttpActionResult) GetOkMethod(oDataModelType).Invoke(this, new[] {result});

        private IList CreateResultList(Type oDataModelType)
            => (IList) Activator.CreateInstance(GetGenericListType(oDataModelType));
    }
}