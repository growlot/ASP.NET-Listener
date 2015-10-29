// <copyright file="TransactionDataRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Repository;

    /// <summary>
    /// Transaction data repository
    /// </summary>
    public class TransactionDataRepository : ITransactionDataRepository
    {
        private readonly IPersistenceAdapter persistence;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDataRepository"/> class.
        /// </summary>
        /// <param name="persistence">The persistence.</param>
        public TransactionDataRepository(IPersistenceAdapter persistence)
        {
            this.persistence = persistence;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionDataRepository" /> class.
        /// </summary>
        ~TransactionDataRepository()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Saves the data asynchronous.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <param name="data">The data.</param>
        /// <returns>Task.</returns>
        public Task SaveDataAsync(Guid recordId, object data)
        {
            return this.persistence.InsertAsync(new TransactionMessageDatumEntity { RecordKey = recordId, MessageData = JsonConvert.SerializeObject(data) });
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }
    }
}