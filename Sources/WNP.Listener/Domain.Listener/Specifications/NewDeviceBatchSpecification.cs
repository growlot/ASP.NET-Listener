//-----------------------------------------------------------------------
// <copyright file="NewDeviceBatchSpecification.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Model
{
    using Model;

    /// <summary>
    /// Specification that checks if device batch is new, not ppresent in the repository.
    /// </summary>
    public sealed class NewDeviceBatchSpecification : ISpecification<DeviceBatch, int>
    {
        /// <summary>
        /// The transaction log repository
        /// </summary>
        private readonly ITransactionLogRepository transactionLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewDeviceBatchSpecification"/> class.
        /// </summary>
        /// <param name="transactionLogRepository">The transaction log repository.</param>
        public NewDeviceBatchSpecification(ITransactionLogRepository transactionLogRepository)
        {
            this.transactionLogRepository = transactionLogRepository;
        }

        /// <summary>
        /// Determines whether specified device batch is already present in repository.
        /// </summary>
        /// <param name="deviceBatch">The device batch.</param>
        /// <returns>True if device does not exist in repository, false otherwise.</returns>
        public bool IsSatisfiedBy(DeviceBatch deviceBatch)
        {
            DeviceBatch foundDevice = this.transactionLogRepository.FindDeviceBatchByBatchNumber(DeviceBatch.BatchNumber);
            return foundDevice == null;
        }
    }
}
