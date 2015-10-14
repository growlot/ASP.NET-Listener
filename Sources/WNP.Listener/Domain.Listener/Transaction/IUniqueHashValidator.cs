// //-----------------------------------------------------------------------
// // <copyright file="IUniqueHashValidator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Threading.Tasks;

    /// <summary>
    /// Hash validator interface
    /// </summary>
    public interface IUniqueHashValidator : IDomainValidator
    {
        /// <summary>
        /// Validates the specified hash.
        /// </summary>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>Task.</returns>
        Task ValidateAsync(int enabledOperationId, string hash);
    }
}