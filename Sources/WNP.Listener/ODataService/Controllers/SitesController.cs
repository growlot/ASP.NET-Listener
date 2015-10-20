//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using MetadataService;
    using Services;
    using Services.FilterTransformer;
    using Newtonsoft.Json.Linq;
    using System.Net.Http;
    using System;
    using ApplicationService.Messages;
    using System.Web.OData;
    using Persistence;

    public class SitesController : WNPController
    {
        public SitesController(IMetadataService metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IAutoConvertor convertor, IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator)
        {
        }
                
        public Task<IHttpActionResult> Post()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(BadRequest(ModelState));
            }

            // constructing oData options since we can not using generic return type
            // without first generating Controller dynamically
            var queryOptions = base.ConstructQueryOptions();

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;

            // get the model describing this type
            var modelMapping = metadataService.GetModelMapping(oDataModelType.Name);
            
            // create actual result object we will be sending over the wire
            var requestContent = base.CreateResult(oDataModelType);

            var request = JObject.Parse(GetRequestContents(Request));

            ////var method = typeof(JsonConvert).GetGenericMethod("DeserializeObject", new Type[] { typeof(string) });
            ////requestContent = method.MakeGenericMethod(oDataModelType).Invoke(null, new object[] { GetRequestContents(Request) });

            var serviceRequest = new AddSiteRequestMessage();

            ////var requestData = (IDictionary<string, object>)request;
            ////foreach (var key in requestData.Keys)
            ////{
            ////    var property = oDataModelType.GetProperty(modelMapping.ColumnToModelMappings[key.ToLowerInvariant()]);
            ////    property.SetValue(entityInstance, convertor.Convert(rawData[key], property.PropertyType));
            ////}

            ////result.Add(entityInstance);


            ////// convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
            ////for (int i = 0; i < sqlWhere.PositionalParameters.Length; i++)
            ////{
            ////    DateTimeOffset? parameter = sqlWhere.PositionalParameters[i] as DateTimeOffset?;

            ////    if (parameter != null)
            ////    {
            ////        DateTime localTime = new DateTime(parameter.Value.ToLocalTime().Ticks);
            ////        DateTime localTimeAsUtc = DateTime.SpecifyKind(localTime, DateTimeKind.Utc);
            ////        sqlWhere.PositionalParameters[i] = (DateTimeOffset)localTimeAsUtc;
            ////    }
            ////}


            ////return CreateOkResponse(oDataModelType, result);
            return Task.FromResult<IHttpActionResult>(Ok());
        }

        public Task<IHttpActionResult> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(BadRequest(ModelState));
            }

            // constructing oData options since we can not using generic return type
            // without first generating Controller dynamically
            var queryOptions = base.ConstructQueryOptions();

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Put([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Patch([FromODataUri] int key)
        {
            throw new NotImplementedException();
        }

        private string GetRequestContents(HttpRequestMessage request)
        {
            HttpContent requestContent = Request.Content;
            string content = requestContent.ReadAsStringAsync().Result;

            return content;
        }
    }
}
