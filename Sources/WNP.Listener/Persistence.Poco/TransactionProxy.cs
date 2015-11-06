// <copyright file="TransactionProxy.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Poco
{
    using AsyncPoco;
    using Repository;

    public class TransactionProxy : ITransactionProxy
    {
        private readonly ITransaction _transaction;

        internal TransactionProxy(ITransaction transaction)
        {
            this._transaction = transaction;
        }

        public void Dispose()
        {
            this._transaction.Dispose();
        }

        public void Commit()
        {
            this._transaction.Complete();
        }
    }
}