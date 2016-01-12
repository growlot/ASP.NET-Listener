//-----------------------------------------------------------------------
// <copyright file="ElectricMetersController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services;
    using Services.FilterTransformer;
    using Utilities;

    /// <summary>
    /// Controller for electric meters
    /// </summary>
    [ActionPrefix("ElectricMeter")]
    public class ElectricMetersController : WNPEntityControllerBase, IBoundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElectricMetersController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        public ElectricMetersController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitOfWork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus,
            IODataQueryHandlerFactory queryHandlerFactory)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus, queryHandlerFactory)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.EqpMeter.FullTableName;

        /// <summary>
        /// Uninstalls meter.
        /// Example URI: ~/ElectricMeters('1')/AMSLLC.Listener.Listener.Uninstall
        /// POST data:
        /// {userName: "user", installationDate: "2015-07-07"}
        /// </summary>
        /// <param name="equipmentNumber">Electric meter equipment number used as a key parameter to select specific meter</param>
        /// <param name="uninstallUser">User name of the users who did the uninstallation</param>
        /// <param name="uninstallDate">Time when uninstall was performed</param>
        /// <param name="uninstallReason">The reason for uninstallation.</param>
        /// <param name="uninstallServiceOrderStarted">The uninstall service order started.</param>
        /// <param name="uninstallServiceOrderCompleted">The uninstall service order completed.</param>
        /// <returns>
        /// The result of action.
        /// </returns>
        [BoundAction]
        public async Task<IHttpActionResult> Uninstall(
            [BoundEntityKey] string equipmentNumber,
            string uninstallUser,
            DateTime uninstallDate,
            string uninstallReason,
            DateTime? uninstallServiceOrderStarted = null,
            DateTime? uninstallServiceOrderCompleted = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.ConstructQueryOptions();
            var meterModelMapping = this.MetadataService.GetModelMappingByTableName(DBMetadata.EqpMeter.FullTableName);

            var meterKey = this.GetRequestKey(meterModelMapping, 1);
            if (meterKey == null)
            {
                return this.BadRequest($"Invalid key specified for the {meterModelMapping.ClassName}.");
            }

            var existingMeter = this.GetEntity<EqpMeterEntity>(meterKey, meterModelMapping, DBMetadata.EqpMeter.EqpNo, DBMetadata.EqpMeter.Site, DBMetadata.EqpMeter.Circuit);
            if (existingMeter == null)
            {
                return this.NotFound();
            }

            if (!existingMeter.Site.HasValue || !existingMeter.Circuit.HasValue)
            {
                throw new InvalidOperationException(StringUtilities.Invariant($"Meter can not be uninstalled, becasue it is not currently installed."));
            }

            var uninstallMeterCommand = new UninstallMeterCommand()
            {
                CircuitId = existingMeter.Circuit.Value,
                EquipmentNumber = existingMeter.EqpNo,
                SiteId = existingMeter.Site.Value,
                UninstallDate = uninstallDate,
                UninstallReason = uninstallReason,
                UninstallServiceOrderCompleted = uninstallServiceOrderCompleted,
                UninstallServiceOrderStarted = uninstallServiceOrderStarted,
                UninstallUser = uninstallUser
            };

            await this.CommandBus.PublishAsync(uninstallMeterCommand);
            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Example URI: ~/ElectricMeters/AMSLLC.Listener.ElectricMeter_ColTest
        /// POST data:
        /// {mystr: "user"}
        /// </summary>
        /// <param name="mystr">Test string</param>
        /// <returns>Modified test string.</returns>
        [BoundAction]
        [CollectionWideAction]
        public string ColTest(string mystr)
        {
            return mystr + "_col";
        }
    }
}
