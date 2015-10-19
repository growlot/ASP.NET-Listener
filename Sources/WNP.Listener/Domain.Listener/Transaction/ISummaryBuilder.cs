//-----------------------------------------------------------------------
// <copyright file="ISummaryBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for building summary.
    /// </summary>
    public interface ISummaryBuilder
    {
        /// <summary>
        /// Builds the summary.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="summary">The summary.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        void Build(string data, Dictionary<string, object> summary, IEnumerable<FieldConfiguration> fieldConfigurations);
    }
}