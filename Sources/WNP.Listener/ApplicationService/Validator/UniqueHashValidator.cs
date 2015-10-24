// //-----------------------------------------------------------------------
// <copyright file="UniqueHashValidator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Validator
{
    using System.Threading.Tasks;
    using Domain.Listener.Transaction;
    using Repository;

    /// <summary>
    /// Unique hash validator
    /// </summary>
    public class UniqueHashValidator : IUniqueHashValidator
    {
        private readonly ITransactionRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueHashValidator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UniqueHashValidator(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="UniqueHashValidator"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        public bool Valid { get; private set; }

        /// <summary>
        /// Validates the specified hash code
        /// </summary>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>Task.</returns>
        public Task ValidateAsync(int enabledOperationId, string hash)
        {
            return this.repository.GetHashCountAsync(enabledOperationId, hash).ContinueWith(t => this.Valid = t.Result == 0);
        }
    }
}