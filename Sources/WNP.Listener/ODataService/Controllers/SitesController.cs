//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using Newtonsoft.Json.Linq;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Services;
    using Services.FilterTransformer;
    using Utilities;

    /// <summary>
    /// Controller for Sites.
    /// </summary>
    public class SitesController : WNPEntityController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitesController"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="convertor">The convertor.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        public SitesController(IMetadataProvider metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator)

            : base(metadataService, dbContext, filterTransformer, actionConfigurator)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.Site.FullTableName;

        /// <summary>
        /// Adds new Site.
        /// </summary>
        /// <returns>The newly created Site, or redirect to existing Site resource.</returns>
        public Task<IHttpActionResult> Post()
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            // constructing oData options since we can not using generic return type
            // without first generating Controller dynamically
            var queryOptions = this.ConstructQueryOptions();

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;

            // get the model describing this type
            var modelMapping = this.metadataService.GetModelMapping(oDataModelType.Name);

            // create actual result object we will be sending over the wire
            var requestContent = this.CreateResult(oDataModelType);

            var request = JObject.Parse(this.GetRequestContents(this.Request));

            ////var method = typeof(JsonConvert).GetGenericMethod("DeserializeObject", new Type[] { typeof(string) });
            ////requestContent = method.MakeGenericMethod(oDataModelType).Invoke(null, new object[] { GetRequestContents(Request) });

            var serviceRequest = new CreateSiteCommand();

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
            return Task.FromResult<IHttpActionResult>(this.Ok());
        }

        public Task<IHttpActionResult> Get([FromODataUri] int key)
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            // constructing oData options since we can not using generic return type
            // without first generating Controller dynamically
            var queryOptions = this.ConstructQueryOptions();

            // we can infer model type from the ODataQueryOptions
            // we created earlier
            var oDataModelType = queryOptions.Context.ElementClrType;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates specified Site.
        /// </summary>
        /// <param name="key">The site identifier.</param>
        /// <returns>The result as HTTP response.</returns>
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
            HttpContent requestContent = this.Request.Content;
            string content = requestContent.ReadAsStringAsync().Result;

            return content;
        }
    }
}
