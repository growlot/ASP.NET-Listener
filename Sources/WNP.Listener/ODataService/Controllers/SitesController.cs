//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.FilterTransformer;

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
        /// Gets the specified Site by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The Site.</returns>
        public Task<IHttpActionResult> Get([FromODataUri] string key)
        {
            // we can infer model type from the ODataQueryOptions
            // we created earlier
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            var queryOptions = this.ConstructQueryOptions();

            var existingSite = this.GetExisting(key);

            return this.PrepareGetResponse(existingSite);
        }

        /// <summary>
        /// Adds new Site.
        /// </summary>
        /// <returns>The OData response for newly created Site.</returns>
        public Task<IHttpActionResult> Post()
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            var queryOptions = this.ConstructQueryOptions();
            var site = this.GetRequestEntity<SiteEntity>();
            return this.CreateSite(site);
        }

        /// <summary>
        /// Updates existing Site or creates new one if it didn't exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="site">The site delta.</param>
        /// <returns>
        /// The updated Site.
        /// </returns>
        public Task<IHttpActionResult> Patch([FromODataUri] string key)
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            var queryOptions = this.ConstructQueryOptions();
            var existingSite = this.GetExisting(key);

            if (existingSite != null)
            {
                var siteDelta = this.GetRequestEntityDelta<SiteEntity>();
                return this.UpdateSite(existingSite, siteDelta, key);
            }
            else
            {
                var site = this.GetRequestEntity<SiteEntity>();
                return this.CreateSite(site);
            }
        }

        private async Task<IHttpActionResult> UpdateSite(SiteEntity existingSite, Delta<SiteEntity> siteDelta, string key)
        {
            List<Task> commandResults = new List<Task>();

            var changedProperties = siteDelta.GetChangedPropertyNames();
            object value;

            if (changedProperties.Contains(nameof(SiteEntity.AccountNo))
                || changedProperties.Contains(nameof(SiteEntity.AccountName)))
            {
                var updateSiteBillingAccount = new UpdateSiteBillingAccountCommand()
                {
                    Owner = this.Owner,
                    SiteId = existingSite.Site.Value,
                    BillingAccountName = siteDelta.TryGetPropertyValue(nameof(SiteEntity.AccountName), out value) ? (string)value : existingSite.AccountName,
                    BillingAccountNumber = siteDelta.TryGetPropertyValue(nameof(SiteEntity.AccountNo), out value) ? (string)value : existingSite.AccountNo
                };

                commandResults.Add(this.commandBus.PublishAsync(updateSiteBillingAccount));
            }

            if (changedProperties.Contains(nameof(SiteEntity.SiteCountry))
                || changedProperties.Contains(nameof(SiteEntity.SiteState))
                || changedProperties.Contains(nameof(SiteEntity.SiteCity))
                || changedProperties.Contains(nameof(SiteEntity.SiteZipcode))
                || changedProperties.Contains(nameof(SiteEntity.SiteAddress))
                || changedProperties.Contains(nameof(SiteEntity.SiteAddress2)))
            {
                var updateSiteAddress = new UpdateSiteAddressCommand()
                {
                    Owner = this.Owner,
                    SiteId = existingSite.Site.Value,
                    Country = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteCountry), out value) ? (string)value : existingSite.SiteCountry,
                    State = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteState), out value) ? (string)value : existingSite.SiteState,
                    City = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteCity), out value) ? (string)value : existingSite.SiteCity,
                    Address1 = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteAddress), out value) ? (string)value : existingSite.SiteAddress,
                    Address2 = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteAddress2), out value) ? (string)value : existingSite.SiteAddress2,
                    Zip = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteZipcode), out value) ? (string)value : existingSite.SiteZipcode
                };

                commandResults.Add(this.commandBus.PublishAsync(updateSiteAddress));
            }

            if (changedProperties.Contains(nameof(SiteEntity.InterconnectUtility))
                || changedProperties.Contains(nameof(SiteEntity.IsInterconnect))
                || changedProperties.Contains(nameof(SiteEntity.PremiseNo))
                || changedProperties.Contains(nameof(SiteEntity.SiteDescription)))
            {
                var updateSiteDetails = new UpdateSiteDetails()
                {
                    Owner = this.Owner,
                    SiteId = existingSite.Site.Value,
                    Description = siteDelta.TryGetPropertyValue(nameof(SiteEntity.SiteDescription), out value) ? (string)value : existingSite.SiteDescription,
                    PremiseNumber = siteDelta.TryGetPropertyValue(nameof(SiteEntity.PremiseNo), out value) ? (string)value : existingSite.PremiseNo,
                    IsInterconnect = siteDelta.TryGetPropertyValue(nameof(SiteEntity.IsInterconnect), out value) ? (string)value : existingSite.IsInterconnect,
                    InterconnectUtilityName = siteDelta.TryGetPropertyValue(nameof(SiteEntity.InterconnectUtility), out value) ? (string)value : existingSite.InterconnectUtility
                };

                commandResults.Add(this.commandBus.PublishAsync(updateSiteDetails));
            }

            await Task.WhenAll(commandResults);

            var updateEntity = this.GetExisting(key);

            return await this.PrepareUpdatedResponse(updateEntity);
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
                Zip = site.SiteZipcode,
                IsInterconnect = site.IsInterconnect,
                InterconnectUtilityName = site.InterconnectUtility
            };

            await this.commandBus.PublishAsync(createSiteCommand);

            var createdSite = ((WNPUnitOfWork)this.unitOfWork).DbContext.SingleOrDefault<SiteEntity>($"WHERE {DBMetadata.Site.Owner}=@0 and {DBMetadata.Site.SiteDescription}=@1", this.Owner, site.SiteDescription);

            return await this.PrepareCreatedResponse(createdSite);
        }

        private SiteEntity GetExisting(string key)
        {
            var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType.Name);

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
    }
}
