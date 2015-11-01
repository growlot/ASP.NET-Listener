//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.FilterTransformer;
    using Utilities;
    using System.Web;
    using ApplicationService;

    /// <summary>
    /// Controller for Sites.
    /// </summary>
    public class SitesController : WNPEntityController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SitesController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="test">The test.</param>
        public SitesController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus, CurrentUnitOfWork test)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus, test)
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

            var queryOptions = this.ConstructQueryOptions();
            var oDataModelType = queryOptions.Context.ElementClrType;
            var site = this.GetRequestEntity<SiteEntity>(oDataModelType);
            return this.CreateSite(site);
        }

        private async Task<IHttpActionResult> CreateSite(SiteEntity site)
        {
            var createSiteCommand = new CreateSiteCommand()
            {
                Address1 = site.SiteAddress,
                Address2 = site.SiteAddress2,
                BillingAccountName = site.AccountName,
                BillingAccountNumber = site.AccountNo,
                City = site.SiteCity,
                Country = site.SiteCountry,
                Description = site.SiteDescription,
                Owner = this.Owner,
                PremiseNumber = site.PremiseNo,
                State = site.SiteState,
                Zip = site.SiteZipcode
            };

            await this.commandBus.PublishAsync(createSiteCommand);

            return this.PrepareCreatedResponse(site.SiteDescription);
        }

        /// <summary>
        /// Gets the specified Site by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public Task<IHttpActionResult> Get([FromODataUri] string key)
        {
            // we can infer model type from the ODataQueryOptions
            // we created earlier
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            this.unitOfWork = (IWNPUnitOfWork)this.Request.GetUnitOfWork();

            var queryOptions = this.ConstructQueryOptions();
            var oDataModelType = queryOptions.Context.ElementClrType;

            var existingEntity = this.GetExisting(key, oDataModelType);

            if (existingEntity != null)
            {
                // create actual object that was sent over the wire
                var responseContent = this.CreateResult(oDataModelType);

                var setFromEntityMethod = oDataModelType.GetMethod("SetFromEntity");
                setFromEntityMethod.Invoke(responseContent, new object[] { existingEntity });

                return Task.FromResult(this.CreateOkResponse(oDataModelType, responseContent));
            }

            return Task.FromResult<IHttpActionResult>(this.NotFound());
        }

        private IHttpActionResult PrepareCreatedResponse(string description)
        {
            var createdSite = ((WNPUnitOfWork)this.unitOfWork).DbContext.SingleOrDefault<SiteEntity>($"WHERE {DBMetadata.Site.Owner}=@0 and {DBMetadata.Site.SiteDescription}=@1", this.Owner, description);

            if (createdSite == null)
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Can not prepare response for Create opreation, because Site with Owner [{this.Owner}] and Description [{description}] was not found."));
            }

            // create actual object that was sent over the wire
            var responseContent = this.CreateResult(this.EdmEntityClrType);

            var setFromEntityMethod = this.EdmEntityClrType.GetMethod("SetFromEntity");
            setFromEntityMethod.Invoke(responseContent, new object[] { createdSite });

            return this.CreateCreatedResponse(this.EdmEntityClrType, responseContent);
        }

        private SiteEntity GetExisting(string key, Type oDataModelType)
        {
            var modelMapping = this.metadataService.GetModelMapping(oDataModelType.Name);

            var premiseNumberFieldName = modelMapping.ColumnToModelMappings[DBMetadata.Site.PremiseNo.ToUpperInvariant()];

            Sql sql = null;
            if (modelMapping.FieldInfo[premiseNumberFieldName].IsPrimaryKey)
            {
                sql = Sql.Builder
                    .Select(modelMapping.ModelToColumnMappings.Values.ToArray())
                    .From(DBMetadata.Site.FullTableName)
                    .Where($"{DBMetadata.Site.PremiseNo}=@0", key);
            }
            else
            {
                sql = Sql.Builder
                    .Select(modelMapping.ModelToColumnMappings.Values.ToArray())
                    .From(DBMetadata.Site.FullTableName)
                    .Where($"{DBMetadata.Site.Site}=@0", int.Parse(key, CultureInfo.InvariantCulture));
            }

            return ((WNPUnitOfWork)this.unitOfWork).DbContext.FirstOrDefault<SiteEntity>(sql);
        }

        public Task<IHttpActionResult> Patch([FromODataUri] string key)
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            var queryOptions = this.ConstructQueryOptions();
            var oDataModelType = queryOptions.Context.ElementClrType;

            // create actual object that was sent over the wire
            var requestContent = this.CreateResult(oDataModelType);

            var site = this.GetRequestEntity<SiteEntity>(oDataModelType);
            var existingEntity = this.GetExisting(key, oDataModelType);

            if (existingEntity != null)
            {
                // do update
                var httpResponse = this.ResponseMessage(new HttpResponseMessage(System.Net.HttpStatusCode.SeeOther));
                httpResponse.Response.Headers.Location = new Uri(StringUtilities.Invariant($"{this.Request.RequestUri}('{existingEntity.PremiseNo}')"));
                return Task.FromResult<IHttpActionResult>(httpResponse);
            }
            else
            {
                // do insert
            }

            return Task.FromResult(this.CreateUpdatedResponse(oDataModelType, requestContent));
        }
    }
}
