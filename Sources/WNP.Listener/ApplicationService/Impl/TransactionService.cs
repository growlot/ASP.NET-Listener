// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Impl
{
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using Communication;
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Repository;

    public class TransactionService : ITransactionService
    {
        public async Task<string> Open(OpenTransactionRequestMessage requestMessage)
        {
            string returnValue = null;
            using (var scope = ApplicationServiceScope.Create())
            {
                var transactionRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento = new TransactionRegistryMemento(null, requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey, requestMessage.OperationKey, TransactionStatusType.InProgress,
                    requestMessage.User, scope.ScopeDateTime, null, JsonConvert.SerializeObject(requestMessage.Data), null, null);

                var transactionRegistry = scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Create(scope.ScopeDateTime);

                await transactionRepository.Create(transactionRegistry);

                returnValue = transactionRegistry.TransactionKey;
            }
            return returnValue;
        }

        public async Task Process(ProcessTransactionRequestMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetExecutionContext(requestMessage.TransactionKey);

                var dataString = await sourceRepository.GetTransactionData(requestMessage.TransactionKey);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();

                ((IOriginator)transactionExecution).SetMemento(memento);
                await
                    Task.WhenAll(
                        transactionExecution.Process(JsonConvert.DeserializeObject<ExpandoObject>(dataString)));
            }
        }

        public async Task Success(TransactionSuccessMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetRegistryEntry(requestMessage.TransactionKey);
                var transactionRegistry =
                    scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Succeed(scope.ScopeDateTime);
                await sourceRepository.Update(transactionRegistry);
            }
        }

        public async Task Failed(TransactionFailedMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetRegistryEntry(requestMessage.TransactionKey);
                var transactionRegistry =
                    scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Fail(scope.ScopeDateTime, requestMessage.Message, requestMessage.Details);
                await sourceRepository.Update(transactionRegistry);
            }
        }
    }
}