// //-----------------------------------------------------------------------
// <copyright file="ITransactionService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Threading.Tasks;
    using Commands;

    /// <summary>
    /// Interface for transaction management
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Opens the specified request message.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>The string.</returns>
        Task<Guid> Open(OpenTransactionCommand requestMessage);

        /// <summary>
        /// Opens the given batch transaction
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        Task<Guid> Open(OpenBatchTransactionCommand requestMessage);

        /// <summary>
        /// Processes the specified request message.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>The emtpy task</returns>
        Task Process(ProcessTransactionCommand requestMessage);

        /// <summary>
        /// Successes the specified transaction success message.
        /// </summary>
        /// <param name="transactionSuccessMessage">The transaction success message.</param>
        /// <returns>The emtpy task</returns>
        Task Success(SucceedTransactionCommand transactionSuccessMessage);

        /// <summary>
        /// Faileds the specified transaction failed message.
        /// </summary>
        /// <param name="transactionFailedMessage">The transaction failed message.</param>
        /// <returns>The emtpy task</returns>
        Task Failed(FailTransactionCommand transactionFailedMessage);

        /// <summary>
        /// Skippeds the specified request message.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>The emtpy task</returns>
        Task Skipped(SkipTransactionCommand requestMessage);
    }
}