// //-----------------------------------------------------------------------
// // <copyright file="ITransactionKeyBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Transaction key builder
    /// </summary>
    public interface ITransactionKeyBuilder
    {
        /// <summary>
        /// Creates the transaction key
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <returns>Transaction key</returns>
        string Create(object data, IEnumerable<FieldConfiguration> fieldConfigurations);
    }
}