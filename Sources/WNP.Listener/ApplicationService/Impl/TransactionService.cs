﻿// //-----------------------------------------------------------------------
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

                //var enabledOperationId = await transactionRepository.GetEnabledOperation(requestMessage.CompanyCode,
                //    requestMessage.SourceApplicationKey, requestMessage.OperationKey);

                var fieldConfigurationMemento = await transactionRepository.GetFieldConfigurations(requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey, requestMessage.OperationKey);

                var memento = new TransactionRegistryMemento(0, null, null, requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey, requestMessage.OperationKey, TransactionStatusType.InProgress,
                    requestMessage.User, scope.ScopeDateTime, null, requestMessage.Data, null, null);

                var transactionRegistry = scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Create(scope.ScopeDateTime, new List<FieldConfiguration>(fieldConfigurationMemento.Select(
                    s =>
                    {
                        var itm = new FieldConfiguration();
                        ((IOriginator)itm).SetMemento(s);
                        return itm;
                    })));

                await transactionRepository.Create(transactionRegistry);

                returnValue = transactionRegistry.RecordKey;
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
                        sourceRepository.GetExecutionContext(requestMessage.RecordKey);

                var dataString = await sourceRepository.GetTransactionData(requestMessage.RecordKey);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();

                ((IOriginator)transactionExecution).SetMemento(memento);

                ExpandoObject data = string.IsNullOrWhiteSpace(dataString) ? null : JsonConvert.DeserializeObject<ExpandoObject>(dataString);

                await
                    Task.WhenAll(
                        transactionExecution.Process(data));

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
                        sourceRepository.GetRegistryEntry(requestMessage.RecordKey);
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
                        sourceRepository.GetRegistryEntry(requestMessage.RecordKey);
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
                        sourceRepository.GetRegistryEntry(requestMessage.RecordKey);
                var transactionRegistry =
                    scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Skip(scope.ScopeDateTime);
                await sourceRepository.Update(transactionRegistry);
            }
        }
    }
}