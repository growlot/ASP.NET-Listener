// <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Communication;
    using Core;

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
        /// Gets or sets the duplicate transactions.
        /// </summary>
        /// <value>The duplicate hash count.</value>
        public Collection<Guid> DuplicateTransactions { get; } = new Collection<Guid>();

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task Process()
        {
            var endpointExecutionData = this.ChildTransactions.Any() ? this.ProcessBatch() : ProcessTransaction(this, this.hashBuilder);
            return this.domainEventBus.PublishBulk(endpointExecutionData);
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
            this.EntityCategoryOperationId = myMemento.EntityCategoryOperationId;
            this.EndpointConfigurations = new ReadOnlyCollection<IntegrationEndpointConfiguration>(myMemento.EndpointConfigurations.Select(cfgMemento => this.DomainBuilder.Create<IntegrationEndpointConfiguration>(cfgMemento)).ToList());
            this.Data = myMemento.Data;
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

        private static ICollection<IDomainEvent> ProcessTransaction(ITransactionExecutionData transactionExecutionData, ITransactionHashBuilder hashBuilder)
        {
            var returnValue = new List<IDomainEvent>();
            var processor = ApplicationIntegration.DependencyResolver.ResolveType<IEndpointDataProcessor>();

            var preparedData = processor.Process(transactionExecutionData.Data, transactionExecutionData.FieldConfigurations);
            string hashCode = hashBuilder.Create(
                new Dictionary<object, FieldConfigurationCollection>
                {
                    {
                        transactionExecutionData.Data,
                        new FieldConfigurationCollection(transactionExecutionData.FieldConfigurations)
                    }
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
                    TransactionHash = transactionExecutionData.OutgoingHash
                };

                foreach (var cfg in transactionExecutionData.EndpointConfigurations)
                {
                    eventData.Endpoint.Add(cfg);
                }

                returnValue.Add(eventData);
            }

            return returnValue;
        }

        private ICollection<IDomainEvent> ProcessBatch()
        {
            var returnValue = new List<IDomainEvent>();

            if (this.DuplicateTransactions.Any())
            {
                var canceled = new TransactionsCanceled();
                foreach (var source in this.ChildTransactions.Where(s => s.Status == TransactionStatusType.Pending))
                {
                    canceled.RecordKeys.Add(source.RecordKey);
                }

                if (canceled.RecordKeys.Any())
                {
                    returnValue.Add(canceled);
                }

                returnValue.Add(new TransactionSkipped(this.RecordKey));
            }
            else
            {
                // if (this.Status == TransactionStatusType.Pending)
                // {
                //     this.Status = TransactionStatusType.Processing;
                // }
                foreach (
                    var childTransactionEntity in
                        this.ChildTransactions.Where(s => s.Status == TransactionStatusType.Pending))
                {
                    returnValue.AddRange(ProcessTransaction(childTransactionEntity, this.hashBuilder));
                }
            }

            return returnValue;
        }
    }
}