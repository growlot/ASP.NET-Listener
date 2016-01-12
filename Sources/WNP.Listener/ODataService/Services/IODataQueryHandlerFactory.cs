// <copyright file="IODataQueryHandlerFactory.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services
{
    using Implementations.ODataQueryHandler;

    /// <summary>
    /// OData query handler factory interface.
    /// </summary>
    public interface IODataQueryHandlerFactory
    {
        /// <summary>
        /// Creates query handler that will return single result
        /// </summary>
        /// <returns>Instance of IODataSingleResultQueryHandler</returns>
        IODataSingleResultQueryHandler SingleResultQuery();

        /// <summary>
        /// Creates query handler that will return multiple results
        /// </summary>
        /// <returns>Instance of IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler MultipleResultsQueryHandler();

        /// <summary>
        /// Creates query handler that will return single result by navigation property
        /// </summary>
        /// <returns>Instance of IODataNavigationSingleResultQueryHandler</returns>
        IODataNavigationSingleResultQueryHandler NavigationSingleResultQuery();

        /// <summary>
        /// Creates query handler that will return multiple results by navigation property
        /// </summary>
        /// <returns>Instance of IODataNavigationMultipleResultsQueryHandler</returns>
        IODataNavigationMultipleResultsQueryHandler NavigationMultipleResultsQuery();
    }
}