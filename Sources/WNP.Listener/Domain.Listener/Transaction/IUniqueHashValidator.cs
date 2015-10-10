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
        /// <param name="companyId">The company identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>Task.</returns>
        Task ValidateAsync(int companyId, string hash);
    }
}