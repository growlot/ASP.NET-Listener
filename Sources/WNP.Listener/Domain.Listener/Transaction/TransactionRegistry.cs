// //-----------------------------------------------------------------------
// // <copyright file="TransactionRegistry.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using Communication;
    using Core;

    /// <summary>
    /// Transaction Registry domain model
    /// </summary>
    public class TransactionRegistry : Entity<int>, IAggregateRoot, IOriginator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistry"/> class.
        /// </summary>
        public TransactionRegistry()
        {
            this.KeyBuilder = ApplicationIntegration.DependencyResolver.ResolveType<ITransactionKeyBuilder>();
        }

        private ITransactionKeyBuilder KeyBuilder { get; }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionKey { get; private set; }

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
        /// Setup new transaction registry entry
        /// </summary>
        /// <param name="createdDateTime">The created date time.</param>
        public void Create(DateTime createdDateTime)
        {
            this.TransactionKey = this.KeyBuilder.Create();
            this.CreatedDateTime = createdDateTime;
            this.Status = TransactionStatusType.InProgress;
        }

        /// <summary>
        /// Succeeed the current transaction
        /// </summary>
        /// <param name="scopeDateTime">The scope date time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Succeed(DateTime scopeDateTime)
        {
            this.UpdatedDateTime = scopeDateTime;
            this.Status = TransactionStatusType.Success;
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
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionRegistryMemento)memento;
            this.TransactionKey = myMemento.TransactionKey;
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
        }
    }
}