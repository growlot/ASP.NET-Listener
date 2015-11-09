// <copyright file="MeterTestResultsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using AMSLLC.Listener.ApplicationService;
    using AMSLLC.Listener.Repository.WNP;
    using Base;
    using MetadataService;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Services;
    using Services.FilterTransformer;

    /// <summary>
    /// Controller for MeterTestResults entity
    /// </summary>
    public class MeterTestResultsController : WNPEntityControllerBase
    {

        public MeterTestResultsController(IMetadataProvider metadataService, IWNPUnitOfWork unitofwork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus)
            : base(metadataService, unitofwork, filterTransformer, actionConfigurator, commandBus)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.MeterTestResults.FullTableName;
    }
}