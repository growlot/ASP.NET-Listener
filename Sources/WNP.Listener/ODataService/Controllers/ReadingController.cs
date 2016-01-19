// <copyright file="ReadingController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using Core;
    using MetadataService;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.Filter;
    using Services.Query;

    /// <summary>
    /// Controller for meter readings
    /// </summary>
    public class ReadingController : WNPEntityControllerBase, IBoundActionsContainer
    {
        private IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public ReadingController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitOfWork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus,
            IODataQueryHandlerFactory queryHandlerFactory,
            IDateTimeProvider dateTimeProvider)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus, queryHandlerFactory)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.Reading.FullTableName;

        /// <summary>
        /// Adds new Reading.
        /// </summary>
        /// <returns>The OData response for newly created Reading.</returns>
        public async Task<IHttpActionResult> Post()
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.ConstructQueryOptions();
            var electricMeterModelMapping = this.MetadataService.GetModelMappingByTableName(DBMetadata.EqpMeter.FullTableName);

            var electricMeterKey = this.GetRequestKey(electricMeterModelMapping, 1);
            if (electricMeterKey == null)
            {
                return this.BadRequest($"Invalid key specified for the {electricMeterModelMapping.ClassName}.");
            }

            var existingElectricMeter = await this.GetEntityAsync<EqpMeterEntity>(electricMeterKey, electricMeterModelMapping, DBMetadata.EqpMeter.EqpNo);

            if (existingElectricMeter == null)
            {
                return this.NotFound();
            }

            this.ConstructQueryOptions();
            var reading = this.GetRequestEntity<ReadingEntity>();
            reading.EqpNo = existingElectricMeter.EqpNo;
            if (!reading.ReadDate.HasValue)
            {
                reading.ReadDate = this.dateTimeProvider.Now();
        }

            return await this.AddReadingAsync(reading);
        }

        private async Task<IHttpActionResult> AddReadingAsync(ReadingEntity reading)
        {
            var addReading = new AddElectricMeterReadingCommand()
            {
                EquipmentNumber = reading.EqpNo,
                Annunciator = reading.Annunciator,
                Date = reading.ReadDate.Value,
                Label = reading.ReadLabel,
                Occasion = string.IsNullOrEmpty(reading.EventFlag) ? null : (char?)reading.EventFlag.ToCharArray()[0],
                Source = reading.ReadSrc,
                User = reading.ReadBy,
                Value = reading.Reading
            };

            await this.CommandBus.PublishAsync(addReading);

            var createdReading = await ((WNPUnitOfWork)this.UnitOfWork).DbContext.SingleOrDefaultAsync<ReadingEntity>(
                $@"
SELECT *
FROM {DBMetadata.Reading.FullTableName}
WHERE {DBMetadata.Reading.Owner}=@0 and {DBMetadata.Reading.EqpNo}=@1 and {DBMetadata.Reading.ReadDate}=@2 and {DBMetadata.Reading.ReadLabel}=@3",
                this.Owner,
                reading.EqpNo,
                reading.ReadDate,
                reading.ReadLabel);
            return await this.PrepareCreatedResponse(createdReading);
        }
    }
}
