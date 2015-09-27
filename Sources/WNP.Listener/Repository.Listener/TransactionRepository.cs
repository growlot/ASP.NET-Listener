// //-----------------------------------------------------------------------
// // <copyright file="TransactionRepository.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Repository.Listener
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Listener.Transaction;

    public class TransactionRepository : ITransactionRepository
    {
        public Task<IMemento> Get(string transactionId)
        {
            throw new NotImplementedException();
        }

        public Task Create(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task Update(TransactionExecution transaction)
        {
            throw new NotImplementedException();
        }

        public Task<IMemento> Get(string sourceApplicationId, string companyId,
            string operationKey)
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Finalizes an instance of the <see cref="TransactionRepository" /> class.
        /// </summary>
        ~TransactionRepository()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
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