// <copyright file="IFilterTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Filter
{
    using System.Web.OData.Query;

    /// <summary>
    /// Interface for filter to SQL WHERE clause transformer.
    /// </summary>
    public interface IFilterTransformer
    {
        /// <summary>
        /// Transforms OData FilterQueryOption to SQL WHERE clause.
        /// </summary>
        /// <param name="newFilterQueryOption">The OData filter query option</param>
        /// <returns>SQL WHERE clause</returns>
        WhereClause TransformFilterQueryOption(FilterQueryOption newFilterQueryOption);

        /// <summary>
        /// Transforms OData FilterQueryOption to SQL WHERE clause.
        /// </summary>
        /// <param name="newFilterQueryOption">The OData filter query option</param>
        /// <param name="newPositionalArgsOffset">Offset for positional SQL arguments</param>
        /// <returns>SQL WHERE clause</returns>
        WhereClause TransformFilterQueryOption(FilterQueryOption newFilterQueryOption, int newPositionalArgsOffset);
    }
}