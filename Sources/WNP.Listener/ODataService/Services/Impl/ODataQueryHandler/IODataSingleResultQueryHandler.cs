// <copyright file="IODataSingleResultQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for OData query handler that returns single object as a result.
    /// </summary>
    public interface IODataSingleResultQueryHandler
    {
        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="clrType">The CLR type</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        IODataSingleResultQueryHandler OnType(Type clrType);

        /// <summary>
        /// Sets the "raw", json-formatted key of entity to be retrieved.
        /// </summary>
        /// <param name="rawKey">The json-formatted key</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        IODataSingleResultQueryHandler WithKey(string rawKey);

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        IODataSingleResultQueryHandler SelectFields(string[] fields);

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        IODataSingleResultQueryHandler Expand(string[] expands);

        /// <summary>
        /// Fetches the fully formatted resulting instance of the requested entity.
        /// </summary>
        /// <returns>Instance of the entity class, defined in OData model assembly.</returns>
        object FetchSingle();
    }
}