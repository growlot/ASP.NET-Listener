// //-----------------------------------------------------------------------
// <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using Commands;
    using Communication;
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Repository;

    /// <summary>
    /// Implements <see cref="ITransactionService"/>
    /// </summary>
    public class TransactionService : ITransactionService
    {
        /// <inheritdoc/>
        public async Task<string> Open(OpenTransactionCommand requestMessage)
        {
            string returnValue = null;
            using (var scope = ApplicationServiceScope.Create())
            {
                var transactionRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();

                // var enabledOperationId = await transactionRepository.GetEnabledOperation(requestMessage.CompanyCode,
                //    requestMessage.SourceApplicationKey, requestMessage.OperationKey);
                var fieldConfigurationMemento = await transactionRepository.GetFieldConfigurationsAsync(
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey,
                    requestMessage.OperationKey);

                var memento = new TransactionRegistryMemento(
                    0,
                    null,
                    null,
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey,
                    requestMessage.OperationKey,
                    TransactionStatusType.InProgress,
                    requestMessage.User,
                    scope.ScopeCreated,
                    null,
                    requestMessage.Data,
                    null,
                    null);

                var transactionRegistry = scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                var fieldConfigurations = new List<FieldConfiguration>(fieldConfigurationMemento.Select(
                    s =>
                    {
                        var item = new FieldConfiguration();
                        ((IOriginator)item).SetMemento(s);
                        return item;
                    }));

                transactionRegistry.Create(scope.ScopeCreated, fieldConfigurations);

                await transactionRepository.CreateTransactionRegistryAsync(transactionRegistry);

                returnValue = transactionRegistry.RecordKey;
            }

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task Process(ProcessTransactionCommand requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetExecutionContextAsync(requestMessage.RecordKey);

                var dataString = await sourceRepository.GetTransactionDataAsync(requestMessage.RecordKey);
                var transactionExecution =
                    scope.DomainBuilder.Create<TransactionExecution>();

                ((IOriginator)transactionExecution).SetMemento(memento);

                ExpandoObject data = string.IsNullOrWhiteSpace(dataString) ? null : JsonConvert.DeserializeObject<ExpandoObject>(dataString);

                await
                    Task.WhenAll(
                        transactionExecution.Process(data));

                await sourceRepository.UpdateHashAsync(transactionExecution.Id, transactionExecution.TransactionHash);
            }
        }

        /// <inheritdoc/>
        public async Task Success(SucceedTransactionCommand transactionSuccessMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetRegistryEntry(transactionSuccessMessage.RecordKey);
                var transactionRegistry =
                    scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Succeed(scope.ScopeCreated);
                await sourceRepository.UpdateTransactionRegistryAsync(transactionRegistry);
            }
        }

        /// <inheritdoc/>
        public async Task Failed(FailTransactionCommand transactionFailedMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                var memento =
                    await
                        sourceRepository.GetRegistryEntry(transactionFailedMessage.RecordKey);
                var transactionRegistry =
                    scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                transactionRegistry.Fail(scope.ScopeCreated, transactionFailedMessage.Message, transactionFailedMessage.Details);
                await sourceRepository.UpdateTransactionRegistryAsync(transactionRegistry);
            }
        }

        /// <inheritdoc/>
        public async Task Skipped(SkipTransactionCommand requestMessage)
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

                transactionRegistry.Skip(scope.ScopeCreated);
                await sourceRepository.UpdateTransactionRegistryAsync(transactionRegistry);
            }
        }
    }
}