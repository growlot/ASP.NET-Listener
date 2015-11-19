// <copyright file="ReadingController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApplicationService;
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.FilterTransformer;
    using Persistence.WNP;

    /// <summary>
    /// Controller for meter readings
    /// </summary>
    public class ReadingController : WNPEntityControllerBase, IBoundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        public ReadingController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.Reading.FullTableName;

        /// <summary>
        /// Adds new Reading.
        /// </summary>
        /// <returns>The OData response for newly created Reading.</returns>
        public Task<IHttpActionResult> Post()
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest(this.ModelState));
            }

            this.ConstructQueryOptions();
            var reading = this.GetRequestEntity<ReadingEntity>();
            return this.CreateReading(reading);
        }

        private async Task<IHttpActionResult> CreateReading(ReadingEntity reading)
        {
            ////var createSiteCommand = new CreateSiteCommand()
            ////{
            ////    Address1 = site.SiteAddress,
            ////    Address2 = site.SiteAddress2,
            ////    BillingAccountName = site.AccountName,
            ////    BillingAccountNumber = site.AccountNo,
            ////    City = site.SiteCity,
            ////    Country = site.SiteCountry,
            ////    Description = site.SiteDescription,
            ////    Owner = this.Owner,
            ////    PremiseNumber = site.PremiseNo,
            ////    State = site.SiteState,
            ////    Zip = site.SiteZipcode,
            ////    IsInterconnect = site.IsInterconnect == "Y" ? true : false,
            ////    InterconnectUtilityName = site.InterconnectUtility
            ////};

            ////await this.commandBus.PublishAsync(createSiteCommand);

            ////var createdSite = ((WNPUnitOfWork)this.unitOfWork).DbContext.SingleOrDefault<SiteEntity>($"WHERE {DBMetadata.Site.Owner}=@0 and {DBMetadata.Site.SiteDescription}=@1", this.Owner, site.SiteDescription);
            reading.ReadIndex = 100;
            return await this.PrepareCreatedResponse(reading);
        }

    }
}
