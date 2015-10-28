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

    /// <summary>
    /// Transaction Registry domain model
    /// </summary>
    public class TransactionRegistry : Entity<int>, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistry" /> class.
        /// </summary>
        /// <param name="keyBuilder">The key builder.</param>
        /// <param name="transactionKeyBuilder">The transaction key builder.</param>
        /// <param name="summaryBuilder">The summary builder.</param>
        public TransactionRegistry(IRecordKeyBuilder keyBuilder, ITransactionKeyBuilder transactionKeyBuilder, ISummaryBuilder summaryBuilder)
        {
            this.KeyBuilder = keyBuilder;
            this.TransactionKeyBuilder = transactionKeyBuilder;
            this.SummaryBuilder = summaryBuilder;
        }

        /// <summary>
        /// Gets the transaction record key.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string RecordKey { get; private set; }

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
        public string TransactionKey { get; private set; }

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
        /// Gets or sets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EnabledOperationId { get; set; }

        private IRecordKeyBuilder KeyBuilder { get; }

        private ITransactionKeyBuilder TransactionKeyBuilder { get; }

        private ISummaryBuilder SummaryBuilder { get; }

        /// <summary>
        /// Setup new transaction registry entry
        /// </summary>
        /// <param name="createdDateTime">The created date time.</param>
        /// <param name="fieldConfigurations">The field configurations.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Review this")]
        public void Create(DateTime createdDateTime, Dictionary<int, IEnumerable<FieldConfiguration>> fieldConfigurations)
        {
            this.RecordKey = this.KeyBuilder.Create();
            this.CreatedDateTime = createdDateTime;
            if (fieldConfigurations != null && fieldConfigurations.ContainsKey(this.EnabledOperationId))
            {
                this.TransactionKey = this.TransactionKeyBuilder.Create(this.Data, fieldConfigurations[this.EnabledOperationId]);
                this.SummaryBuilder.Build(this.Data, this.Summary, fieldConfigurations[this.EnabledOperationId]);
            }

            this.Status = TransactionStatusType.InProgress;
            foreach (var childTransactionRegistryEntity in this.ChildTransactions)
            {
                childTransactionRegistryEntity.RecordKey = this.KeyBuilder.Create();
                childTransactionRegistryEntity.CreatedDateTime = createdDateTime;
                childTransactionRegistryEntity.TransactionKey = this.TransactionKeyBuilder.Create(childTransactionRegistryEntity.Data, fieldConfigurations?[childTransactionRegistryEntity.EnabledOperationId]);
                childTransactionRegistryEntity.Status = TransactionStatusType.InProgress;
                this.SummaryBuilder.Build(childTransactionRegistryEntity.Data, childTransactionRegistryEntity.Summary, fieldConfigurations?[childTransactionRegistryEntity.EnabledOperationId]);
            }
        }

        /// <summary>
        /// Succeed the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        public void Succeed(DateTime scopeDateTime)
        {
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
        /// Fails the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        public void Fail(DateTime scopeDateTime, string message, string details)
        {
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
            this.TransactionKey = myMemento.TransactionKey;
            this.EnabledOperationId = myMemento.EnabledOperationId;
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