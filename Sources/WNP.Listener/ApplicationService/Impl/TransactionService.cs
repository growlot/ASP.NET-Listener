// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Impl
{
    using System.Threading.Tasks;
    using Communication;
    using Domain;
    using Domain.Listener.Transaction;
    using Repository;

    public class TransactionService : ITransactionService
    {
        public Task Open(OpenTransactionRequestMessage requestMessage)
        {
            throw new System.NotImplementedException();
        }

        public async Task Process(ProcessTransactionRequestMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.Get(requestMessage.TransactionId);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();
                ((IOriginator) transactionExecution).SetMemento(memento);
                await Task.WhenAll(transactionExecution.Process(requestMessage.Data));
            }
        }

        public Task Success(TransactionSuccessMessage transactionSuccessMessage)
        {
            throw new System.NotImplementedException();
        }

        public Task Failed(TransactionFailedMessage transactionFailedMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}