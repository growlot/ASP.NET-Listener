using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
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
