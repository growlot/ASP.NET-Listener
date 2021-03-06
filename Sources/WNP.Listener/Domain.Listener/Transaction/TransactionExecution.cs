﻿// <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Shared;

    /// <summary>
    /// Transaction execution
    /// </summary>
    public class TransactionExecution : AggregateRoot<int>, IWithDomainBuilder, ITransactionExecutionData
    {
        /// <summary>
        /// The domain event bus
        /// </summary>
        private readonly IDomainEventBus domainEventBus;

        /// <summary>
        /// The hash builder
        /// </summary>
        private readonly ITransactionHashBuilder hashBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionExecution" /> class.
        /// </summary>
        /// <param name="domainEventBus">The domain event bus.</param>
        /// <param name="hashBuilder">The hash builder.</param>
        public TransactionExecution(IDomainEventBus domainEventBus, ITransactionHashBuilder hashBuilder)
        {
            this.domainEventBus = domainEventBus;
            this.hashBuilder = hashBuilder;
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public Guid RecordKey { get; private set; }

        /// <summary>
        /// Gets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; private set; }

        /// <summary>
        /// Gets or sets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        public string OutgoingHash { get; set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public ReadOnlyCollection<IntegrationEndpointConfiguration> EndpointConfigurations { get; private set; } = new ReadOnlyCollection<IntegrationEndpointConfiguration>(new IntegrationEndpointConfiguration[0]);

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        public ReadOnlyCollection<FieldConfiguration> FieldConfigurations { get; private set; } = new ReadOnlyCollection<FieldConfiguration>(new FieldConfiguration[0]);

        /// <summary>
        /// Gets the child transactions.
        /// </summary>
        /// <value>The child transactions.</value>
        public ReadOnlyCollection<ChildTransactionEntity> ChildTransactions { get; private set; } = new ReadOnlyCollection<ChildTransactionEntity>(new ChildTransactionEntity[0]);

        /// <summary>
        /// Gets or sets the domain builder.
        /// </summary>
        /// <value>The domain builder.</value>
        public virtual IDomainBuilder DomainBuilder { get; set; }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; private set; }

        /// <summary>
        /// Gets the duplicate transactions.
        /// </summary>
        /// <value>The duplicate hash count.</value>
        public Collection<Guid> DuplicateTransactions { get; } = new Collection<Guid>();

        /// <summary>
        /// Gets the operation transaction key.
        /// </summary>
        /// <value>The operation transaction key.</value>
        public Guid OperationTransactionKey { get; private set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>The status.</value>
        public TransactionStatusType Status { get; private set; }

        /// <summary>
        /// Gets a value indicating whether if transaction should be automaticaly succeeded if delivered
        /// </summary>
        /// <value>The automatic complete.</value>
        public bool AutoSucceed { get; private set; }

        /// <summary>
        /// Retries execution of the transaction
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task Retry()
        {
            return this.Process(
                new[]
                {
                    TransactionStatusType.Canceled,
                    TransactionStatusType.Pending,
                    TransactionStatusType.Failed
                });
        }

        /// <summary>
        /// Retries execution of the transaction
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task ForceRetry()
        {
            return this.Process(
                new[]
                {
                    TransactionStatusType.Canceled,
                    TransactionStatusType.Failed,
                    TransactionStatusType.Pending,
                    TransactionStatusType.Invalid,
                    TransactionStatusType.Skipped,
                    TransactionStatusType.Processing
                });
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task Process()
        {
            return this.Process(new[] { TransactionStatusType.Pending, });
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionExecutionMemento)memento;
            this.RecordKey = myMemento.RecordKey;
            this.Id = myMemento.TransactionId;
            this.Status = myMemento.Status;
            this.EntityCategoryOperationId = myMemento.EntityCategoryOperationId;
            this.EndpointConfigurations = new ReadOnlyCollection<IntegrationEndpointConfiguration>(myMemento.EndpointConfigurations.Select(cfgMemento => this.DomainBuilder.Create<IntegrationEndpointConfiguration>(cfgMemento)).ToList());
            this.Data = myMemento.Data;
            this.AutoSucceed = myMemento.AutoSucceed;
            this.OperationTransactionKey = myMemento.OperationTransactionKey;
            this.FieldConfigurations = new ReadOnlyCollection<FieldConfiguration>(new List<FieldConfiguration>(myMemento.FieldConfigurations.Select(s =>
            {
                var itm = new FieldConfiguration();
                ((IOriginator)itm).SetMemento(s);
                return itm;
            })));

            this.ChildTransactions = new ReadOnlyCollection<ChildTransactionEntity>(new List<ChildTransactionEntity>(myMemento.ChildTransactions.Select(s =>
            {
                var itm = new ChildTransactionEntity { DomainBuilder = this.DomainBuilder };
                ((IOriginator)itm).SetMemento(s);
                return itm;
            })));

            foreach (var duplicateRecord in myMemento.DuplicateRecords)
            {
                this.DuplicateTransactions.Add(duplicateRecord);
            }
        }

        private static List<IDomainEvent> ProcessTransaction(ITransactionExecutionData transactionExecutionData, ITransactionHashBuilder hashBuilder, bool autoSucceed = false)
        {
            var returnValue = new List<IDomainEvent>();
            var processor = ApplicationIntegration.DependencyResolver.ResolveType<IEndpointDataProcessor>();

            var preparedData = processor.Process(transactionExecutionData.Data, transactionExecutionData.FieldConfigurations);
            var hData = new HashData
            {
                Data = transactionExecutionData.Data,
                OperationTransactionKey = transactionExecutionData.OperationTransactionKey
            };

            hData.FieldConfiguration.AddRange(transactionExecutionData.FieldConfigurations);
            string hashCode = hashBuilder.Create(
                new List<HashData>
                {
                    hData
                },
                f => f.OutgoingSequence);

            transactionExecutionData.OutgoingHash = hashCode;

            if (transactionExecutionData.DuplicateTransactions.Any())
            {
                returnValue.Add(new TransactionSkipped(transactionExecutionData.RecordKey));
            }
            else
            {
                var eventData = new TransactionDataReady
                {
                    Data = new TransactionMessage
                    {
                        Data = preparedData.Data
                    },
                    RecordKey = transactionExecutionData.RecordKey,
                    TransactionHash = transactionExecutionData.OutgoingHash,
                    AutoSucceed = autoSucceed
                };

                foreach (var cfg in transactionExecutionData.EndpointConfigurations)
                {
                    eventData.Endpoint.Add(cfg);
                }

                returnValue.Add(eventData);
            }

            return returnValue;
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="statusTypes">The status types.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        private Task Process(TransactionStatusType[] statusTypes)
        {
            if (!statusTypes.Contains(this.Status))
            {
                return Task.Factory.StartNew(() => { });
            }

            var endpointExecutionData = this.ChildTransactions.Any()
                ? this.ProcessBatch(statusTypes)
                : new Dictionary<int, List<IDomainEvent>>()
                {
                    {
                        0, ProcessTransaction(this, this.hashBuilder, this.AutoSucceed)
                    }
                };
            var ordered = endpointExecutionData.OrderBy(s => s.Key).ToList();
            var task = this.domainEventBus.PublishBulk(ordered.First().Value);
            foreach (KeyValuePair<int, List<IDomainEvent>> keyValuePair in ordered.Skip(1).ToList())
            {
                task = task.ContinueWith(
                    t =>
                    {
                        if (t.IsFaulted)
                        {
                            t.Exception.Handle(
                                ex =>
                                {
                                    Debug.WriteLine(t.Exception.Flatten());
                                    return false;
                                });
                            throw t.Exception;
                        }
                        else
                        {
                            return this.domainEventBus.PublishBulk(keyValuePair.Value);
                        }
                    }).Unwrap();
            }

            return task;
        }

        private Dictionary<int, List<IDomainEvent>> ProcessBatch(
           TransactionStatusType[] statuses)
        {
            var returnValue = new Dictionary<int, List<IDomainEvent>>();

            if (this.DuplicateTransactions.Any())
            {
                var block = new List<IDomainEvent>();
                returnValue[0] = block;
                var canceled = new TransactionsCanceled();
                foreach (var source in this.ChildTransactions.Where(s => s.Status == TransactionStatusType.Pending))
                {
                    canceled.RecordKeys.Add(source.RecordKey);
                }

                if (canceled.RecordKeys.Any())
                {
                    block.Add(canceled);
                }

                block.Add(new TransactionSkipped(this.RecordKey));
            }
            else
            {
                returnValue[int.MinValue] = new List<IDomainEvent>();
                returnValue[int.MinValue].AddRange(ProcessTransaction(this, this.hashBuilder));

                foreach (var childTransactionEntity in
                    this.ChildTransactions.Where(
                        s =>
                            statuses.Contains(s.Status)))
                {
                    var priority = childTransactionEntity.Priority ?? 0;
                    if (!returnValue.ContainsKey(priority))
                    {
                        returnValue[priority] = new List<IDomainEvent>();
                    }

                    childTransactionEntity.Dirty = true;

                    returnValue[priority].AddRange(ProcessTransaction(childTransactionEntity, this.hashBuilder, true));
                }
            }

            return returnValue;
        }
    }
}