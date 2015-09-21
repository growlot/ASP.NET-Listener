// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Impl
{
    using System.Threading.Tasks;
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Domain.Listener.Transaction;
    using AMSLLC.Listener.Repository;
    using Communication;

    public class TransactionService : ITransactionService
    {
        public async Task Process(TransactionRequestMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.Get(requestMessage.SourceApplicationId, requestMessage.DestinationApplicationId,
                            requestMessage.OperationKey);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();
                ((IOriginator)transactionExecution).SetMemento(memento);
                await Task.WhenAll(transactionExecution.Process(requestMessage.TransactionId));
            }
        }
    }
}