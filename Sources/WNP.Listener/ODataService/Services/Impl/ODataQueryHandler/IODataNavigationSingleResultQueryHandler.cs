// <copyright file="IODataNavigationSingleResultQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;

    /// <summary>
    /// Interface for OData query handler that returns single navigation object as a result.
    /// </summary>
    public interface IODataNavigationSingleResultQueryHandler
    {
        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="parentClrType">Parent CLR type</param>
        /// <param name="childClrType">Child CLR type</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        IODataNavigationSingleResultQueryHandler OnTypes(Type parentClrType, Type childClrType);

        /// <summary>
        /// Sets the "raw", json-formatted key of entity to be retrieved.
        /// </summary>
        /// <param name="rawParentKey">The json-formatted parent key</param>
        /// <param name="rawChildKey">The json-formatted child key</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        IODataNavigationSingleResultQueryHandler WithKeys(string rawParentKey, string rawChildKey);

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        IODataNavigationSingleResultQueryHandler SelectFields(string[] fields);

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        IODataNavigationSingleResultQueryHandler Expand(string[] expands);

        /// <summary>
        /// Fetches the fully formatted resulting instance of the requested entity.
        /// </summary>
        /// <returns>Instance of the entity class, defined in OData model assembly.</returns>
        object Fetch();
    }
}