// //-----------------------------------------------------------------------
// // <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using Communication;
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Repository;
    using Utilities;
    using Formatting = Newtonsoft.Json.Formatting;

    public class TransactionService : ITransactionService
    {
        public async Task<string> Open(OpenTransactionRequestMessage requestMessage)
        {
            string returnValue = null;
            using (var scope = ApplicationServiceScope.Create())
            {
                var transactionRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento = new TransactionRegistryMemento(0, null, requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey, requestMessage.OperationKey, TransactionStatusType.InProgress,
                    requestMessage.User, requestMessage.Header, scope.ScopeDateTime, null, requestMessage.Data, null, null);

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
                var headerString = await sourceRepository.GetTransactionHeader(requestMessage.TransactionKey);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();

                ((IOriginator)transactionExecution).SetMemento(memento);



                var newDict = string.IsNullOrWhiteSpace(headerString) ? null : JsonConvert.DeserializeObject<Dictionary<string, object>>(headerString);
                ExpandoObject data = string.IsNullOrWhiteSpace(dataString) ? null : JsonConvert.DeserializeObject<ExpandoObject>(dataString);

                await
                    Task.WhenAll(
                        transactionExecution.Process(data, newDict));

                await sourceRepository.UpdateHash(transactionExecution.Id, transactionExecution.TransactionHash);
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

        public async Task Skipped(TransactionSkippedRequestMessage requestMessage)
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

                transactionRegistry.Skip(scope.ScopeDateTime);
                await sourceRepository.Update(transactionRegistry);
            }
        }
    }
}