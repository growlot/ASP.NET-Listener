//-----------------------------------------------------------------------
// <copyright file="SitesController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
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
                var modelMapping = this.metadataService.GetModelMapping(this.EdmEntityClrType.Name);
                var premiseNumberFieldName = modelMapping.ColumnToModelMappings[DBMetadata.Site.PremiseNo.ToUpperInvariant()];
                if (modelMapping.FieldInfo[premiseNumberFieldName].IsPrimaryKey)
                {
                    var site = this.GetRequestEntity<SiteEntity>();
                    site.PremiseNo = key;
                    return this.CreateSite(site);
                }
                else
                {
                    return this.PrepareGetResponse(existingSite);
                }
            }
        }

        private async Task<IHttpActionResult> UpdateSite(SiteEntity existingSite, Delta<SiteEntity> siteDelta, string key)
        {
            var changedProperties = siteDelta.GetChangedPropertyNames();

            if (changedProperties.Contains(nameof(SiteEntity.InterconnectUtility))
                || changedProperties.Contains(nameof(SiteEntity.IsInterconnect)))
            {
                throw new NotImplementedException("Update of the properties InterconnectUtility, IsInterconnect, is not yet implemented");
                ////var updateSiteDetails = new UpdateSiteDetails()
                ////{
                ////    Owner = this.Owner,
                ////    SiteId = existingSite.Site.Value,
                ////    IsInterconnect = siteDelta.TryGetPropertyValue(nameof(SiteEntity.IsInterconnect), out value) ? (string)value : existingSite.IsInterconnect,
                ////    InterconnectUtilityName = siteDelta.TryGetPropertyValue(nameof(SiteEntity.InterconnectUtility), out value) ? (string)value : existingSite.InterconnectUtility
                ////};

                ////commandResults.Add(this.commandBus.PublishAsync(updateSiteDetails));
            }

            List<Task> commandResults = new List<Task>();

            if (changedProperties.Contains(nameof(SiteEntity.AccountNo))
                || changedProperties.Contains(nameof(SiteEntity.AccountName)))
            {
                var updateSiteBillingAccount = new UpdateSiteBillingAccountCommand()
                {
                    Owner = this.Owner,
                    SiteId = existingSite.Site.Value,
                    BillingAccountName = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.AccountName), existingSite.AccountName),
                    BillingAccountNumber = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.AccountNo), existingSite.AccountNo)
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
                    Country = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteCountry), existingSite.SiteCountry),
                    State = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteState), existingSite.SiteState),
                    City = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteCity), existingSite.SiteCity),
                    Address1 = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteAddress), existingSite.SiteAddress),
                    Address2 = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteAddress2), existingSite.SiteAddress2),
                    Zip = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteZipcode), existingSite.SiteZipcode)
                };

                commandResults.Add(this.commandBus.PublishAsync(updateSiteAddress));
            }

            if (changedProperties.Contains(nameof(SiteEntity.PremiseNo))
                || changedProperties.Contains(nameof(SiteEntity.SiteDescription)))
            {
                var updateSiteDetails = new UpdateSiteDetailsCommand()
                {
                    Owner = this.Owner,
                    SiteId = existingSite.Site.Value,
                    Description = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.SiteDescription), existingSite.SiteDescription),
                    PremiseNumber = GetChangedOrCurrent(siteDelta, nameof(SiteEntity.PremiseNo), existingSite.PremiseNo)
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
                IsInterconnect = site.IsInterconnect == "Y" ? true : false,
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

            var dbContext = ((WNPUnitOfWork)this.unitOfWork).DbContext;
            var result = dbContext.FirstOrDefault<SiteEntity>(sql);
            return result;
        }
    }
}
