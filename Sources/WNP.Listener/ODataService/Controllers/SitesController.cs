//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Linq;
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
    using Newtonsoft.Json;
    using Domain.WNP.SiteAggregate;
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

            // create actual object that was sent over the wire
            var requestContent = this.CreateResult(oDataModelType);

            var method = typeof(JsonConvert).GetGenericMethod("DeserializeObject", new Type[] { typeof(string) });
            requestContent = method.MakeGenericMethod(oDataModelType).Invoke(null, new object[] { this.GetRequestContents(this.Request) });

            var getEntityMethod = oDataModelType.GetMethod("GetEntity");

            SiteEntity site = (SiteEntity)getEntityMethod.Invoke(requestContent, new object[] { });

            if (!string.IsNullOrWhiteSpace(site.PremiseNo))
            {
                var modelMapping = this.metadataService.GetModelMapping(oDataModelType.Name);
                var sql = Sql.Builder
                    .Select(modelMapping.ModelToColumnMappings.Values.ToArray())
                    .From(DBMetadata.Site.FullTableName)
                    .Where($"{DBMetadata.Site.PremiseNo}=@0", site.PremiseNo);

                var existingEnitty = this.dbContext.FirstOrDefault<SiteEntity>(sql);
                if (existingEnitty != null)
                {
                    var httpResponse = this.ResponseMessage(new HttpResponseMessage(System.Net.HttpStatusCode.SeeOther));
                    httpResponse.Response.Headers.Location = new Uri(StringUtilities.Invariant($"{this.Request.RequestUri}('{existingEnitty.PremiseNo}')"));
                    return Task.FromResult<IHttpActionResult>(httpResponse);
                }
            }

            if (site.CreateBy != null
                || site.CreateDate != null
                || site.ModBy != null
                || site.ModDate != null
                || site.Owner != null
                || site.Site != null)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest("Field can not be set in the call"));
            }

            PhysicalAddress siteAddres = null;
            if (site.SiteAddress != null
                || site.SiteAddress2 != null
                || site.SiteCity != null
                || site.SiteCountry != null
                || site.SiteState != null
                || site.SiteZipcode != null)
            {
                siteAddres = new PhysicalAddressBuilder()
                    .WithAddressLine1(site.SiteAddress)
                    .WithAddressLine2(site.SiteAddress2)
                    .WithCity(site.SiteCity)
                    .WithCountry(site.SiteCountry)
                    .WithState(site.SiteState)
                    .WithZipCode(site.SiteZipcode);
            }

            BillingAccount account = null;
            if (site.AccountName != null
                || site.AccountNo != null)
            {
                account = new BillingAccount(site.AccountName, site.AccountNo);
            }

            var serviceRequest = new CreateSiteCommand()
            {
                Account = account,
                Address = siteAddres,
                Description = site.SiteDescription,
                Owner = 0,
                PremiseNumber = site.PremiseNo
            };

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
            // we can infer model type from the ODataQueryOptions
            // we created earlier
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

            var modelMapping = this.metadataService.GetModelMapping(oDataModelType.Name);
            var sql = Sql.Builder
                .Select(modelMapping.ModelToColumnMappings.Values.ToArray())
                .From(DBMetadata.Site.FullTableName)
                .Where($"{DBMetadata.Site.PremiseNo}=@0", key);

            var existingEnitty = this.dbContext.FirstOrDefault<SiteEntity>(sql);
            if (existingEnitty != null)
            {
                // create actual object that was sent over the wire
                var responseContent = this.CreateResult(oDataModelType);

                var setFromEntityMethod = oDataModelType.GetMethod("SetFromEntity");
                setFromEntityMethod.Invoke(responseContent, new object[] { existingEnitty });

                return Task.FromResult(this.CreateOkResponse(oDataModelType, responseContent));
            }

            return Task.FromResult<IHttpActionResult>(this.NotFound());
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
