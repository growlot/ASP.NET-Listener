// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

using AMSLLC.Listener.Communication;
using AMSLLC.Listener.Domain.Listener.Transaction;
using AMSLLC.Listener.Repository;

namespace AMSLLC.Listener.ApplicationService.Impl
{
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