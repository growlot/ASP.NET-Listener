// <copyright file="MeterTestResultsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using Base;
    using MetadataService;
    using Persistence.WNP;
    using Persistence.WNP.Metadata;
    using Services;
    using Services.FilterTransformer;

    /// <summary>
    /// Controller for MeterTestResults entity
    /// </summary>
    public class MeterTestResultsController : WNPEntityController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestResultsController"/> class.
        /// </summary>
        public MeterTestResultsController(IMetadataProvider metadataService, WNPDBContext dbContext, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator) : base(metadataService, dbContext, filterTransformer, actionConfigurator)
        {
        }

        /// <inheritdoc/>
        public override string GetEntityTableName() => DBMetadata.MeterTestResults.FullTableName;
    }
}