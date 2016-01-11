// <copyright file="ODataQueryHandlerFactory.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using AMSLLC.Listener.ODataService.Services.FilterTransformer;

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using AMSLLC.Listener.MetadataService;
    using AMSLLC.Listener.Repository.WNP;

    /// <summary>
    /// Factory class for creating fluent OData request handlers
    /// </summary>
    public class ODataQueryHandlerFactory : IODataQueryHandlerFactory
    {
        private readonly IMetadataProvider metadataProvider;

        private readonly IWNPUnitOfWork unitOfWork;
        private readonly IFilterTransformer filterTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataQueryHandlerFactory"/> class.
        /// </summary>
        /// <param name="metadataProvider">The MetadataProvider.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="filterTransformer">The filter transformer</param>
        public ODataQueryHandlerFactory(IMetadataProvider metadataProvider, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
            this.filterTransformer = filterTransformer;
        }

        /// <summary>
        /// Creates new query handler that will return single result.
        /// </summary>
        /// <returns>IODataSingleResultQueryHandler implementation instance</returns>
        public IODataSingleResultQueryHandler SingleResultQuery()
        {
            return new ODataSingleResultQueryHandler(this.metadataProvider, this.unitOfWork);
        }

        /// <summary>
        /// Creates query handler that will return multiple results
        /// </summary>
        /// <returns>Instance of IODataMultipleResultsQueryHandler</returns>
        public IODataMultipleResultsQueryHandler MultipleResultsQueryHandler()
        {
            return new ODataMultipleResultsQueryHandler(this.metadataProvider, this.unitOfWork, this.filterTransformer);
        }

        /// <summary>
        /// Creates new query handler that will return single result from a navigation property.
        /// </summary>
        /// <returns>IODataNavigationSingleResultQueryHandler implementation instance</returns>
        public IODataNavigationSingleResultQueryHandler NavigationSingleResultQuery()
        {
            return new ODataNavigationSingleResultQueryHandler(this.metadataProvider, this.unitOfWork);
        }

        /// <summary>
        /// Creates query handler that will return multiple results by navigation property
        /// </summary>
        /// <returns>Instance of IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler NavigationMultipleResultsQuery()
        {
            return new ODataNavigationMultipleResultsQueryHandler(this.metadataProvider, this.unitOfWork, this.filterTransformer);
        }
    }
}