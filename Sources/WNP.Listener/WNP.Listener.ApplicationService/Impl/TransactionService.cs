// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService.Impl
{
    using AMSLLC.Listener.Domain.Listener.Transaction;
    using Communication;
    using Repository;

    public class TransactionService : ITransactionService
    {
        public void Process(TransactionRequestMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create(new DomainBuilder(), null))
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionConfiguration>(
                        sourceRepository.Get(requestMessage.SourceApplicationId, requestMessage.DestinationApplicationId,
                            requestMessage.OperationKey));
                transactionExecution.Process();
            }
        }
    }
}