<<<<<<< HEAD:Sources/WNP.Listener/Persistence.Listener/TransactionProxy.cs
﻿namespace AMSLLC.Listener.Persistence.Listener
=======
﻿namespace Persistence.Poco
>>>>>>> WNP batch updated:Sources/WNP.Listener/Persistence.Poco/TransactionProxy.cs
{
    using AMSLLC.Listener.Repository;
    using AsyncPoco;

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
