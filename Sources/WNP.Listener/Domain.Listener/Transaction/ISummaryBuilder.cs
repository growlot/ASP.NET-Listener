// //-----------------------------------------------------------------------
// // <copyright file="ISummaryBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Summary builder interface
    /// </summary>
    public interface ISummaryBuilder
    {
        /// <summary>
        /// Builds the specified summary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        void Build(object data, Dictionary<string, object> summary, IEnumerable<FieldConfiguration> fieldConfigurations);
    }
}