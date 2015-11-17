// //-----------------------------------------------------------------------
// <copyright file="TransactionRegistry.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Communication;
    using Shared;

    /// <summary>
    /// Transaction Registry domain model
    /// </summary>
    public class TransactionRegistry : AggregateRoot<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistry" /> class.
        /// </summary>
        /// <param name="keyBuilder">The key builder.</param>
        /// <param name="transactionHashBuilder">The transaction key builder.</param>
        /// <param name="summaryBuilder">The summary builder.</param>
        public TransactionRegistry(IRecordKeyBuilder keyBuilder, ITransactionHashBuilder transactionHashBuilder, ISummaryBuilder summaryBuilder)
        {
            this.KeyBuilder = keyBuilder;
            this.TransactionHashBuilder = transactionHashBuilder;
            this.SummaryBuilder = summaryBuilder;
        }

        /// <summary>
        /// Gets the transaction record key.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public Guid RecordKey { get; private set; }

        /// <summary>
        /// Gets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; private set; }

        /// <summary>
        /// Gets the application key.
        /// </summary>
        /// <value>The application key.</value>
        public string ApplicationKey { get; private set; }

        /// <summary>
        /// Gets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; private set; }

        /// <summary>
        /// Gets the transaction status.
        /// </summary>
        /// <value>The transaction status.</value>
        public TransactionStatusType Status { get; private set; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the created date time.
        /// </summary>
        /// <value>The created date time.</value>
        public DateTime CreatedDateTime { get; private set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>The updated date time.</value>
        public DateTime? UpdatedDateTime { get; private set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; private set; }

        /// <summary>
        /// Gets or sets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string IncomingHash { get; private set; }

        /// <summary>
        /// Gets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public Dictionary<string, object> Summary { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the child transactions.
        /// </summary>
        /// <value>The child transactions.</value>
        public ReadOnlyCollection<ChildTransactionRegistryEntity> ChildTransactions { get; private set; } = new ReadOnlyCollection<ChildTransactionRegistryEntity>(new ChildTransactionRegistryEntity[0]);

        /// <summary>
        /// Gets or sets the entity category operation id.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; set; }

        private IRecordKeyBuilder KeyBuilder { get; }

        private ITransactionHashBuilder TransactionHashBuilder { get; }

        private ISummaryBuilder SummaryBuilder { get; }

        /// <summary>
        /// Setup new transaction registry entry
        /// </summary>
        /// <param name="createdDateTime">The created date time.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Review this")]
        public virtual void Create(DateTime createdDateTime, Dictionary<int, IEnumerable<FieldConfiguration>> fieldConfigurations)
        {
            this.RecordKey = Guid.Parse(this.KeyBuilder.Create());
            this.CreatedDateTime = createdDateTime;
            if (fieldConfigurations != null)
            {
                var hashElements =
                    this.ChildTransactions
                        .ToDictionary<ChildTransactionRegistryEntity, object, FieldConfigurationCollection>(
                            childTransactionRegistryEntity => childTransactionRegistryEntity.Data,
                            childTransactionRegistryEntity =>
                               new FieldConfigurationCollection(fieldConfigurations?[childTransactionRegistryEntity.EntityCategoryOperationId]));

                if (!hashElements.Any() && fieldConfigurations.ContainsKey(this.EntityCategoryOperationId))
                {
                    // assuming non-batch transaction
                    hashElements.Add(this.Data, new FieldConfigurationCollection(fieldConfigurations[this.EntityCategoryOperationId]));
                }

                this.IncomingHash =
                    this.TransactionHashBuilder.Create(
                        hashElements,
                        f => f.IncomingSequence);

                if (fieldConfigurations.ContainsKey(this.EntityCategoryOperationId))
                {
                    this.SummaryBuilder.Build(this.Data, this.Summary, fieldConfigurations[this.EntityCategoryOperationId]);
                }
            }

            this.Status = TransactionStatusType.Pending;
            foreach (var childTransactionRegistryEntity in this.ChildTransactions)
            {
                childTransactionRegistryEntity.RecordKey = Guid.Parse(this.KeyBuilder.Create());
                childTransactionRegistryEntity.CreatedDateTime = createdDateTime;
                childTransactionRegistryEntity.IncomingHash =
                    this.TransactionHashBuilder.Create(
                        new Dictionary<object, FieldConfigurationCollection>
                        {
                            {
                                childTransactionRegistryEntity.Data,
                               new FieldConfigurationCollection(fieldConfigurations?[childTransactionRegistryEntity.EntityCategoryOperationId])
                            }
                        },
                        f => f.IncomingSequence);
                childTransactionRegistryEntity.Status = TransactionStatusType.Pending;
                this.SummaryBuilder.Build(childTransactionRegistryEntity.Data, childTransactionRegistryEntity.Summary, fieldConfigurations?[childTransactionRegistryEntity.EntityCategoryOperationId]);
            }
        }

        /// <summary>
        /// Succeed the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        public void Succeed(DateTime scopeDateTime)
        {
            foreach (var source in this.ChildTransactions.Where(s => s.Status == TransactionStatusType.Processing))
            {
                source.Status = TransactionStatusType.Success;
                source.UpdatedDateTime = scopeDateTime;
            }

            if (this.ChildTransactions.Any()
                && !this.ChildTransactions.All(s => s.Status == TransactionStatusType.Success || s.Status == TransactionStatusType.Skipped))
            {
                return;
            }

            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Success;
        }

        /// <summary>
        /// Skips the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        public void Skip(DateTime scopeDateTime)
        {
            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Skipped;
        }

        /// <summary>
        /// Processing specified transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        public void Processing(DateTime scopeDateTime)
        {
            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Processing;
        }

        /// <summary>
        /// Cancels the transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        public void Cancel(DateTime scopeDateTime)
        {
            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Canceled;
        }

        /// <summary>
        /// Fails the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        public void Fail(DateTime scopeDateTime, string message, string details)
        {
            foreach (var source in this.ChildTransactions.Where(s => s.Status == TransactionStatusType.Pending))
            {
                source.Status = TransactionStatusType.Canceled;
                source.UpdatedDateTime = scopeDateTime;
            }

            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Failed;
            this.Message = message;
            this.Details = details;
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionRegistryMemento)memento;
            this.RecordKey = myMemento.RecordKey;
            this.CompanyCode = myMemento.CompanyCode;
            this.ApplicationKey = myMemento.ApplicationKey;
            this.OperationKey = myMemento.OperationKey;
            this.Status = myMemento.Status;
            this.UserName = myMemento.UserName;
            this.CreatedDateTime = myMemento.CreatedDateTime;
            this.UpdatedDateTime = myMemento.UpdatedDateTime;
            this.Data = myMemento.Data;
            this.Message = myMemento.Message;
            this.Details = myMemento.Details;
            this.Id = myMemento.TransactionId;
            this.IncomingHash = myMemento.IncomingHash;
            this.EntityCategoryOperationId = myMemento.EntityCategoryOperationId;
            this.ChildTransactions = new ReadOnlyCollection<ChildTransactionRegistryEntity>(myMemento.ChildTransactions.Select(s =>
            {
                var childMemento = (TransactionRegistryMemento)s;
                ChildTransactionRegistryEntity e = new ChildTransactionRegistryEntity();
                ((IOriginator)e).SetMemento(childMemento);
                return e;
            }).ToList());
        }
    }
}