// <copyright file="ChildTransactionEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Shared;

    /// <summary>
    /// Child transaction entity
    /// </summary>
    public class ChildTransactionEntity : Entity<Guid>, ITransactionExecutionData
    {
        /// <summary>
        /// Gets or sets the domain builder.
        /// </summary>
        /// <value>The domain builder.</value>
        public IDomainBuilder DomainBuilder { get; set; }

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
        /// Gets or sets the transaction hash.
        /// </summary>
        /// <value>The transaction hash.</value>
        public string OutgoingHash { get; set; }

        /// <summary>
        /// Gets the enabled entity operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; private set; }

        /// <summary>
        /// Gets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public Guid RecordKey => this.Id;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets the duplicate transaction.
        /// </summary>
        /// <value>The duplicate transaction.</value>
        public Collection<Guid> DuplicateTransactions { get; } = new Collection<Guid>();

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public TransactionStatusType Status { get; set; }

        /// <summary>
        /// Gets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int? Priority { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this transaction is dirty.
        /// </summary>
        /// <value>The dirty.</value>
        public bool Dirty { get; set; }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionExecutionMemento)memento;
            this.Id = myMemento.RecordKey;
            this.EntityCategoryOperationId = myMemento.EntityCategoryOperationId;
            this.Data = myMemento.Data;
            this.Status = myMemento.Status;
            this.Priority = myMemento.Priority;
            this.EndpointConfigurations = new ReadOnlyCollection<IntegrationEndpointConfiguration>(myMemento.EndpointConfigurations.Select(cfgMemento => this.DomainBuilder.Create<IntegrationEndpointConfiguration>(cfgMemento)).ToList());
            this.FieldConfigurations = new ReadOnlyCollection<FieldConfiguration>(new List<FieldConfiguration>(myMemento.FieldConfigurations.Select(s =>
            {
                var itm = new FieldConfiguration();
                ((IOriginator)itm).SetMemento(s);
                return itm;
            })));

            foreach (var duplicateRecord in myMemento.DuplicateRecords)
            {
                this.DuplicateTransactions.Add(duplicateRecord);
            }
        }
    }
}