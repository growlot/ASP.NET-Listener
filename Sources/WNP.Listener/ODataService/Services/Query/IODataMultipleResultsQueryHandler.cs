// <copyright file="IODataMultipleResultsQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Query
{
    using System;
    using System.Collections.Generic;
    using System.Web.OData.Query;

    /// <summary>
    /// Interface for OData query handler that returns multiple objects as a result.
    /// </summary>
    public interface IODataMultipleResultsQueryHandler
    {
        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="clrType">Parent CLR type</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler OnType(Type clrType);

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler SelectFields(string[] fields);

        /// <summary>
        /// Defines OData filter to be applied to the current query.
        /// TODO: currently, seems to be the only adequate way to put this info is via FilterQueryOption.
        /// </summary>
        /// <param name="filterQueryOption">The OData filter query option</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler Filter(FilterQueryOption filterQueryOption);

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler Expand(string[] expands);

        /// <summary>
        /// Defines how much records should be skipped.
        /// </summary>
        /// <param name="count">Skip count</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler Skip(int? count);

        /// <summary>
        /// Defines how much records should be taken.
        /// </summary>
        /// <param name="count">Top count</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        IODataMultipleResultsQueryHandler Top(int? count);

        /// <summary>
        /// Fetches the fully formatted collection of resulting instances of the requested entity.
        /// </summary>
        /// <returns>Set of the entity class instances, defined in OData model assembly.</returns>
        IEnumerable<object> Fetch();
    }
}