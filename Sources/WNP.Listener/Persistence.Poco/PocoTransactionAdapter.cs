// <copyright file="PocoTransactionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Poco
{
    using System;
    using Repository;

    /// <summary>
    /// Poco specific implementation for <see cref="ITransactionAdapter"/>
    /// </summary>
    public class PocoTransactionAdapter : ITransactionAdapter
    {
        private readonly ITransaction transaction;

        internal PocoTransactionAdapter(ITransaction transaction)
        {
            this.transaction = transaction;
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.transaction.Dispose();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            this.transaction.Complete();
        }

        /// <inheritdoc/>
        public void Rollback()
        {
        }
    }
}
