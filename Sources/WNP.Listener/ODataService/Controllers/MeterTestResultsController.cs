// <copyright file="MeterTestResultsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using AMSLLC.Listener.ApplicationService;
    using AMSLLC.Listener.ODataService.Services;
    using AMSLLC.Listener.Repository.WNP;
    using Base;
    using MetadataService;
    using Persistence.WNP.Metadata;
    using Services.FilterTransformer;

    /// <summary>
    /// Controller for MeterTestResults entity
    /// </summary>
    public class MeterTestResultsController : WNPEntityControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestResultsController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        public MeterTestResultsController(
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
        public override string GetEntityTableName() => DBMetadata.MeterTestResults.FullTableName;
    }
}