// //-----------------------------------------------------------------------
// <copyright file="TransactionService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.Listener.Transaction;
    using Model;
    using Newtonsoft.Json;
    using Repository;
    using Serilog;
    using Shared;

    /// <summary>
    /// Implements <see cref="ITransactionService"/>
    /// </summary>
    public class TransactionService : ITransactionService
    {
        private const string BatchOperationName = "Batch";
        private const string BatchEntityCategory = "Batch";

        /// <inheritdoc/>
        public async Task<Guid> Open(OpenTransactionCommand requestMessage)
        {
            Guid returnValue = Guid.Empty;
            using (var scope = ApplicationServiceScope.Create())
            {
                var transactionRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();

                // var enabledOperationId = await transactionRepository.GetEnabledOperation(requestMessage.CompanyCode,
                //    requestMessage.SourceApplicationKey, requestMessage.OperationKey);
                var fieldConfigurationMemento = await transactionRepository.GetFieldConfigurationsAsync(
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey);

                Log.Logger.Information(
                    "Searching for enabled operation. Application Key: {0}, Company Code: {1}, Operation Name: {2}, Entity Name: {3}",
                    requestMessage.SourceApplicationKey,
                    requestMessage.CompanyCode,
                    requestMessage.OperationKey,
                    requestMessage.EntityName);

                var enabledOperations = await transactionRepository.GetEnabledEntityOperations();
                var enabledOperation = enabledOperations.Single(s => string.Compare(s.ApplicationKey, requestMessage.SourceApplicationKey, StringComparison.InvariantCulture) == 0
                && string.Compare(s.CompanyCode, requestMessage.CompanyCode, StringComparison.InvariantCulture) == 0
                && string.Compare(s.OperationName, requestMessage.OperationKey, StringComparison.InvariantCulture) == 0
                && string.Compare(s.EntityName, requestMessage.EntityName, StringComparison.InvariantCulture) == 0);

                var memento = new TransactionRegistryMemento(
                    0,
                    Guid.Empty,
                    null,
                    null,
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey,
                    requestMessage.OperationKey,
                    TransactionStatusType.Pending,
                    requestMessage.User,
                    scope.ScopeCreated,
                    null,
                    requestMessage.Data,
                    null,
                    null,
                    enabledOperation.EnabledOperationId,
                    null);

                var transactionRegistry = scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                var fieldConfigurations = fieldConfigurationMemento.Values.SelectMany(s => s).Cast<FieldConfigurationMemento>().GroupBy(o => o.EntityCategoryOperationId).ToDictionary(g => g.Key, g => g.Select(s =>
                  {
                      var item = new FieldConfiguration();
                      ((IOriginator)item).SetMemento(s);
                      return item;
                  }));

                transactionRegistry.Create(scope.ScopeCreated, fieldConfigurations);
                Log.Logger.Information("Opening transaction {0}", transactionRegistry.RecordKey);
                await transactionRepository.CreateTransactionRegistryAsync(transactionRegistry);

                returnValue = transactionRegistry.RecordKey;
            }

            return returnValue;
        }

        /// <summary>
        /// Opens the given batch transaction
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public async Task<Guid> Open(OpenBatchTransactionCommand requestMessage)
        {
            Guid returnValue = Guid.Empty;
            using (var scope = ApplicationServiceScope.Create())
            {
                var transactionRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();

                var fieldConfigurationMemento = await transactionRepository.GetFieldConfigurationsAsync(
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey);

                var enabledOperations = await transactionRepository.GetEnabledEntityOperations();
                var enabledOperation = enabledOperations.Single(s => string.Compare(s.ApplicationKey, requestMessage.SourceApplicationKey, StringComparison.InvariantCulture) == 0
                && string.Compare(s.CompanyCode, requestMessage.CompanyCode, StringComparison.InvariantCulture) == 0
                && string.Compare(s.OperationName, BatchOperationName, StringComparison.InvariantCulture) == 0
                && string.Compare(s.EntityName, BatchEntityCategory, StringComparison.InvariantCulture) == 0);

                List<TransactionRegistryMemento> batch = new List<TransactionRegistryMemento>();

                foreach (BatchTransactionEntry transaction in requestMessage.Batch)
                {
                    var enabledChildOperation =
                        enabledOperations.Single(
                            s =>
                                string.Compare(
                                    s.ApplicationKey,
                                    requestMessage.SourceApplicationKey,
                                    StringComparison.InvariantCulture) == 0 &&
                                string.Compare(
                                    s.CompanyCode,
                                    requestMessage.CompanyCode,
                                    StringComparison.InvariantCulture) == 0 &&
                                string.Compare(
                                    s.OperationName,
                                    transaction.OperationKey,
                                    StringComparison.InvariantCulture) == 0 &&
                                string.Compare(
                                    s.EntityName,
                                    transaction.EntityCategory,
                                    StringComparison.InvariantCulture) == 0);

                    batch.Add(new TransactionRegistryMemento(
                        0,
                        Guid.Empty,
                        transaction.Priority,
                        null,
                        requestMessage.CompanyCode,
                        requestMessage.SourceApplicationKey,
                        transaction.OperationKey,
                        TransactionStatusType.Pending,
                        transaction.User,
                        scope.ScopeCreated,
                        null,
                        transaction.Data,
                        null,
                        null,
                        enabledChildOperation.EnabledOperationId,
                        null));
                }

                var memento = new TransactionRegistryMemento(
                    0,
                    Guid.Empty,
                    null,
                    null,
                    requestMessage.CompanyCode,
                    requestMessage.SourceApplicationKey,
                    BatchOperationName,
                    TransactionStatusType.Pending,
                    requestMessage.User,
                    scope.ScopeCreated,
                    null,
                    JsonConvert.SerializeObject(new { BatchNumber = requestMessage.BatchNumber, Size = batch.Count }),
                    null,
                    null,
                    enabledOperation.EnabledOperationId,
                    batch);

                var transactionRegistry = scope.DomainBuilder.Create<TransactionRegistry>();
                ((IOriginator)transactionRegistry).SetMemento(memento);

                var fieldConfigurations = fieldConfigurationMemento.Values.SelectMany(s => s).Cast<FieldConfigurationMemento>().GroupBy(o => o.EntityCategoryOperationId).ToDictionary(g => g.Key, g => g.Select(s =>
                {
                    var item = new FieldConfiguration();
                    ((IOriginator)item).SetMemento(s);
                    return item;
                }));

                transactionRegistry.Create(scope.ScopeCreated, fieldConfigurations);
                Log.Logger.Information("Opening batch transaction {0} with {1} children", transactionRegistry.RecordKey, transactionRegistry.ChildTransactions.Count);
                await transactionRepository.CreateTransactionRegistryAsync(transactionRegistry);

                returnValue = transactionRegistry.RecordKey;
            }

            return returnValue;
        }

        /// <inheritdoc/>
        public async Task Process(ProcessTransactionCommand requestMessage)
        {
            try
            {
                using (var scope = ApplicationServiceScope.Create())
                {
                    var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                    var memento = await sourceRepository.GetExecutionContextAsync(requestMessage.RecordKey);

                    // var dataString = await sourceRepository.GetTransactionDataAsync(requestMessage.RecordKey);
                    var transactionExecution = scope.DomainBuilder.Create<TransactionExecution>();

                    ((IOriginator)transactionExecution).SetMemento(memento);
                    Log.Logger.Information(
                        "{0} endpoints found for {1}",
                        transactionExecution.EndpointConfigurations.Count,
                        transactionExecution.RecordKey);
                    switch (requestMessage.RetryPolicy)
                    {
                        case RetryPolicyType.Retry:
                            Log.Logger.Information(
                                "Retrying transaction {0} with {1} children",
                                transactionExecution.RecordKey,
                                transactionExecution.ChildTransactions.Count);
                            await transactionExecution.Retry();
                            break;
                        case RetryPolicyType.Force:
                            Log.Logger.Information(
                                "Forcing transaction retry for {0} with {1} children",
                                transactionExecution.RecordKey,
                                transactionExecution.ChildTransactions.Count);
                            await transactionExecution.ForceRetry();
                            break;
                        default:
                            Log.Logger.Information(
                                "Processing transaction {0} with {1} children",
                                transactionExecution.RecordKey,
                                transactionExecution.ChildTransactions.Count);
                            await transactionExecution.Process();
                            break;
                    }

                    var hashList =
                        new Dictionary<Guid, string>(
                            transactionExecution.ChildTransactions.Where(c => c.Dirty)
                                .ToDictionary(s => s.RecordKey, s => s.OutgoingHash))
                        {
                            {
                                transactionExecution.RecordKey, transactionExecution.OutgoingHash
                            }
                        };
                    Log.Logger.Debug("Updating hash list {0}", JsonConvert.SerializeObject(hashList));
                    await sourceRepository.UpdateHashAsync(hashList);
                }
            }
            catch (Exception exc)
            {
                Log.Logger.Information("Error while processing {0}. Failing transaction.", requestMessage.RecordKey);
                await this.Failed(
                    new FailTransactionCommand
                    {
                        RecordKey = requestMessage.RecordKey,
                        Details = exc.StackTrace,
                        Message = exc.Message
                    });
                throw;
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
                Log.Logger.Information("Succeeding transaction {0}", transactionRegistry.RecordKey);
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
                Log.Logger.Information("Failing transaction {0}", transactionRegistry.RecordKey);
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
                Log.Logger.Information("Skipping transaction {0}", transactionRegistry.RecordKey);
                await sourceRepository.UpdateTransactionRegistryAsync(transactionRegistry);
            }
        }

        /// <inheritdoc/>
        public async Task Processing(
            ProcessingTransactionCommand requestMessage)
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

                transactionRegistry.Processing(scope.ScopeCreated);
                await sourceRepository.UpdateTransactionRegistryAsync(transactionRegistry);
            }
        }

        /// <inheritdoc/>
        public async Task Cancel(
            CancelTransactionsCommand requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var sourceRepository = scope.RepositoryBuilder.Create<ITransactionRepository>();
                Collection<TransactionRegistry> modifiedRegistries = new Collection<TransactionRegistry>();
                foreach (var recordKey in requestMessage.RecordKeys)
                {
                    var memento =
                        await
                            sourceRepository.GetRegistryEntry(recordKey);
                    var transactionRegistry =
                        scope.DomainBuilder.Create<TransactionRegistry>();
                    ((IOriginator)transactionRegistry).SetMemento(memento);

                    transactionRegistry.Cancel(scope.ScopeCreated);
                    modifiedRegistries.Add(transactionRegistry);
                }

                Log.Logger.Information("Canceling transactions {0}", JsonConvert.SerializeObject(modifiedRegistries.Select(s => s.RecordKey)));
                await sourceRepository.UpdateTransactionRegistryBulkAsync(modifiedRegistries);
            }
        }
    }
}