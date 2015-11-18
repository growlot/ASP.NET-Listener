// <copyright file="WorkstationController.cs" company="Advanced Metering Services LLC">
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
        public WorkstationController(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.Workstation.FullTableName;

        /// <summary>
        /// Performs the transition of equipment state based on configured tracking rules.
        /// </summary>
        /// <param name="workstation">The workstation name.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation result.</returns>
        [BoundAction]
        public Task<IHttpActionResult> PerformTransition(
            [BoundEntityKey] string workstation,
            string actionName,
            string equipmentType,
            string equipmentNumber)
        {
            return Task.FromResult((IHttpActionResult)this.StatusCode(HttpStatusCode.NoContent));
        }
    }
}
