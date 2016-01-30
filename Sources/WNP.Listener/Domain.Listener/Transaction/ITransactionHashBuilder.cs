// <copyright file="ITransactionHashBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Transaction key builder
    /// </summary>
    public interface ITransactionHashBuilder
    {
        /// <summary>
        /// Creates the transaction key
        /// </summary>
        /// <param name="hashData">The hash data.</param>
        /// <param name="hashSequenceSelector">The hash sequence selector.</param>
        /// <returns>Transaction key</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Need generic to pass function")]
        string Create(
            ICollection<HashData> hashData,
            Func<FieldConfiguration, short?> hashSequenceSelector);
    }
}