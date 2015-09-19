using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using PetaPoco;
using WNP.Listener.MetadataService;
using WNP.Listener.MetadataService.Impl;
using WNP.Listener.ODataService.Services;
using WNP.Listener.ODataService.Services.FilterTransformer;

namespace WNP.Listener.ODataService.Controllers
{
    using Utilities;

    [EnableQuery]
    public class WNPController : ODataController
    {
        private readonly IMetadataService _metadataService;        
        private readonly IFilterTransformer _filterTransformer;
        private readonly IAutoConvertor _convertor;

        private readonly WNPDBContext _dbContext;

        private readonly ODataValidationSettings _defaultODataValidationSettings;

        public WNPController(IMetadataService metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor)
        {
            _metadataService = metadataService;
            _dbContext = dbContext;
            _filterTransformer = filterTransformer;
            _convertor = convertor;

            _defaultODataValidationSettings = new ODataValidationSettings()
            {
                AllowedQueryOptions =
                    AllowedQueryOptions.Select | AllowedQueryOptions.Filter | AllowedQueryOptions.Top |
                    AllowedQueryOptions.Skip,
            };
        }

        public IHttpActionResult Get()
        {
            // constructing oData options since we're can not using generic return type
            // without first generating Controller dynamically
            var queryOptions = ConstructQueryOptions();
            queryOptions.Validate(_defaultODataValidationSettings);

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;

            var modelMapping = _metadataService.ODataModelMappings[oDataModelType.FullName];

            var skip = queryOptions.Skip?.Value ?? 0;
            var top = queryOptions.Top?.Value ?? 10;

            // create actual result object we will be sending over the wire
            var result = CreateResultList(oDataModelType);

            var dbResults = _dbContext.SkipTake<dynamic>(skip, top,
                Sql.Builder.Select(GetDBColumnsList(queryOptions.SelectExpand, modelMapping.ModelToColumnMappings))
                           .From(modelMapping.TableName));

            foreach (var record in dbResults)
            {
                var entityInstance = Activator.CreateInstance(oDataModelType);

                var rawData = (IDictionary<string, object>)record;
                foreach (var key in rawData.Keys.Where(key => key != "peta_rn"))
                {
                    var property = oDataModelType.GetProperty(modelMapping.ColumnToModelMappings[key]);
                    property.SetValue(entityInstance, _convertor.Convert(rawData[key], property.PropertyType));
                }

                result.Add(entityInstance);
            }

            return CreateOkResponse(oDataModelType, result);
        }

        private string[] GetDBColumnsList(SelectExpandQueryOption queryOptions,
            Dictionary<string, MetadataServiceImpl.ODataToDatabaseColumnInfo> mapping)
            => queryOptions?.RawSelect.Split(',').Select(item => mapping[item].DatabaseColumnName).ToArray() ?? new [] { DBMetadata.ALL };

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

            var type = _metadataService.ODataModelAssembly.GetType(modelTypeFullName);
            var model = oDataProperties.Model;

            return new ODataQueryOptions(new ODataQueryContext(model, type, oDataPath), Request);
        }
    }
}