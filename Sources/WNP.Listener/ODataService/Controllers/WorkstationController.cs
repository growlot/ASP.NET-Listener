// <copyright file="WorkstationController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AMSLLC.Listener.ODataService.Services;

    using ApplicationService;
    using ApplicationService.Commands;
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Persistence.WNP.Metadata;
    using Repository.WNP;
    using Services.FilterTransformer;

    /// <summary>
    /// Controller for workstations
    /// </summary>
    public class WorkstationController : WNPEntityControllerBase, IBoundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkstationController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        public WorkstationController(
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
        public override string GetEntityTableName() => DBMetadata.Workstation.FullTableName;

        /// <summary>
        /// Executes business rule that changes equipment state.
        /// </summary>
        /// <param name="workstation">The workstation name.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <param name="boxNumber">The box number.</param>
        /// <param name="palletNumber">The pallet number.</param>
        /// <param name="shelfId">The shelf identifier.</param>
        /// <param name="issuedTo">The issued to.</param>
        /// <param name="vehicleNumber">The vehicle number.</param>
        /// <param name="location">The location.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation result.
        /// </returns>
        [BoundAction]
        public async Task<IHttpActionResult> ExecuteBusinessRule(
            [BoundEntityKey] string workstation,
            string actionName,
            string equipmentType,
            string equipmentNumber,
            string boxNumber = null,
            string palletNumber = null,
            string shelfId = null,
            string issuedTo = null,
            string vehicleNumber = null,
            string location = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var executeBusinessRuleCommand = new ExecuteBusinessRuleCommand()
            {
                Workstation = workstation,
                EquipmentType = equipmentType,
                EquipmentNumber = equipmentNumber,
                ActionName = actionName,
                BoxNumber = boxNumber,
                PalletNumber = palletNumber,
                ShelfId = shelfId,
                IssuedTo = issuedTo,
                VehicleNumber = vehicleNumber,
                Location = location
            };

            await this.CommandBus.PublishAsync(executeBusinessRuleCommand);
            return this.StatusCode(HttpStatusCode.NoContent);
        }
    }
}
