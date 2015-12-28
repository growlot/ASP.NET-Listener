// <copyright file="IFilterTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.FilterTransformer
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
        /// <param name="filterQueryOption">The OData filter query option</param>
        /// <param name="positionalArgsOffset">Offset for positional SQL arguments</param>
        /// <returns>SQL WHERE clause</returns>
        WhereClause TransformFilterQueryOption(FilterQueryOption filterQueryOption, int positionalArgsOffset = 0);
    }

    /// <summary>
    /// Container object for SQL WHERE clause and positional arguments bound to it.
    /// </summary>
    public class WhereClause
    {
        /// <summary>
        /// Gets or sets the SQL WHERE clause.
        /// </summary>
        public string Clause { get; set; }

        /// <summary>
        /// Gets or sets positional arguments for the SQL WHERE clause.
        /// </summary>
        public object[] PositionalParameters { get; set; }
    }
}