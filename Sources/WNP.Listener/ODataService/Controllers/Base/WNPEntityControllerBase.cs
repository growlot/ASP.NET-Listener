// <copyright file="WNPEntityControllerBase.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using ApplicationService;
    using MetadataService;
    using Repository.WNP;
    using Services.Filter;
    using Services.Query;

    /// <summary>
    /// Base class for all entity specific WNP controllers
    /// </summary>
    public abstract class WNPEntityControllerBase : WNPEntityController, IWNPEntityController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WNPEntityControllerBase" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="queryHandlerFactory">The query handler factory.</param>
        protected WNPEntityControllerBase(IMetadataProvider metadataService, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer, IActionConfigurator actionConfigurator, ICommandBus commandBus, IODataQueryHandlerFactory queryHandlerFactory)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus, queryHandlerFactory)
        {
        }

        /// <inheritdoc/>
        public abstract string GetEntityTableName();
    }
}
