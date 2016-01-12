// <copyright file="IODataFunctionToSqlConvertor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Filter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OData.Core.UriParser.Semantic;

    /// <summary>
    /// Interface defining OData function convertion to SQL function.
    /// </summary>
    public interface IODataFunctionToSqlConvertor
    {
        /// <summary>
        /// Determines whether the specified function name is supported.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <returns><c>true</c> if OData function can be converted to SQL function; <c>false</c> otherwise.</returns>
        bool IsSupported(string functionName);

        /// <summary>
        /// Converts the specified OData function to SQL function.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>SQL function string.</returns>
        string Convert(string functionName, Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);
    }
}