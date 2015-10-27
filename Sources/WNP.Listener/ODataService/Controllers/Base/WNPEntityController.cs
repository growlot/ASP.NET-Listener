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
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using MetadataService;
    using Microsoft.OData.Edm;
    using Persistence.WNP;
    using Services;
    using Services.FilterTransformer;
    using Utilities;

    public abstract class WNPEntityController : WNPController, IBoundActionsContainer
    {
        private readonly ODataValidationSettings defaultODataValidationSettings;

        protected WNPEntityController(
            IMetadataProvider metadataService,
            WNPDBContext dbContext,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, actionConfigurator)
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
                sql = sql.Where(sqlWhere.Clause, sqlWhere.PositionalParameters);

            var dbResults = this.dbContext.SkipTake<dynamic>(skip, top, sql);

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
                return this.NotFound();

            KeyValuePathSegment keySegment = null;
            if (!isCollectionWide)
                keySegment = oDataPath.Segments[1] as KeyValuePathSegment;

            return await this.InvokeAction(actionsContainerType, actionName, keySegment);
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
                    this.GetType()
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                        .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                        .MakeGenericMethod(this.GetGenericListType(oDataModelType)));

        private IHttpActionResult CreateOkResponse(Type oDataModelType, object result)
            => (IHttpActionResult) this.GetOkMethod(oDataModelType).Invoke(this, new[] {result});

        private IList CreateResultList(Type oDataModelType)
            => (IList) Activator.CreateInstance(this.GetGenericListType(oDataModelType));
    }
}