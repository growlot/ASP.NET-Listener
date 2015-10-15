﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.ODataService.Services;
using AMSLLC.Listener.ODataService.Services.FilterTransformer;
using AMSLLC.Listener.Persistence;
using AMSLLC.Listener.Persistence.Metadata;
using AMSLLC.Listener.Utilities;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace AMSLLC.Listener.ODataService.Controllers
{
    [EnableQuery]
    public class WNPController : ODataController
    {
        protected readonly IMetadataService metadataService;
        protected readonly IFilterTransformer filterTransformer;
        protected readonly IAutoConvertor convertor;

        private readonly WNPDBContext dbContext;

        private readonly ODataValidationSettings defaultODataValidationSettings;

        public WNPController(IMetadataService metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor)
        {
            this.metadataService = metadataService;
            this.dbContext = dbContext;
            this.filterTransformer = filterTransformer;
            this.convertor = convertor;

            defaultODataValidationSettings = new ODataValidationSettings()
            {
                AllowedQueryOptions =
                    AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Skip,
            };
        }

        public SingleResult Single()
        {
            return new SingleResult<object>(null);
        }

        public IHttpActionResult Get()
        {
            // constructing oData options since we're can not using generic return type
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
            var result = CreateResultList(oDataModelType);

            var sql =
                Sql.Builder.Select(GetDBColumnsList(queryOptions.SelectExpand, modelMapping.ModelToColumnMappings))
                           .From($"{modelMapping.TableName}");

            var sqlWhere = filterTransformer.TransformFilterQueryOption(queryOptions.Filter);

            // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
            for(int i = 0; i < sqlWhere.PositionalParameters.Length; i++)
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
                var entityInstance = Activator.CreateInstance(oDataModelType);

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

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions,
            Dictionary<string, string> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item]).ToArray() ?? mapping.Values.ToArray();

        private IHttpActionResult CreateOkResponse(Type oDataModelType, object result)
            => (IHttpActionResult) GetOkMethod(oDataModelType).Invoke(this, new[] {result});
    
        private IList CreateResultList(Type oDataModelType)
            => (IList) Activator.CreateInstance(GetResultType(oDataModelType));

        private MethodInfo GetOkMethod(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting($"WNPController.OkMethod<{oDataModelType.FullName}>",
                    () =>
                        GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                            .First(mInfo => mInfo.Name == "Ok" && mInfo.IsGenericMethod)
                            .MakeGenericMethod(GetResultType(oDataModelType)));

        private Type GetResultType(Type oDataModelType)
            => MemoryCache.Default.GetOrAddExisting($"WNPController.List<{oDataModelType.FullName}>",
                    () => typeof (List<>).MakeGenericType(oDataModelType));

        private ODataQueryOptions ConstructQueryOptions()
        {
            var oDataProperties = Request.ODataProperties();

            var oDataPath = oDataProperties.Path;

            string modelTypeFullName;
            switch (oDataProperties.Path.EdmType.TypeKind)
            {
                case EdmTypeKind.None:
                    throw new NotSupportedException("EdmTypeKind.None not yet supported");
                case EdmTypeKind.Primitive:
                    throw new NotSupportedException("EdmTypeKind.Primitive not yet supported");
                case EdmTypeKind.Entity:
                    throw new NotSupportedException("EdmTypeKind.Entity not yet supported");
                case EdmTypeKind.Complex:
                    throw new NotSupportedException("EdmTypeKind.Complex not yet supported");
                case EdmTypeKind.Collection:
                    modelTypeFullName =
                        ((EdmCollectionType) oDataProperties.Path.EdmType).ElementType.FullName();
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

            var type = metadataService.ODataModelAssembly.GetType(modelTypeFullName);
            var model = oDataProperties.Model;

            return new ODataQueryOptions(new ODataQueryContext(model, type, oDataPath), Request);
        }
    }
}