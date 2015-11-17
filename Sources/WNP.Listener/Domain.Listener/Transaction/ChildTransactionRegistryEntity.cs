// <copyright file="ChildTransactionRegistryEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using Communication;
    using Shared;

    /// <summary>
    /// Child transaction registry entry
    /// </summary>
    public class ChildTransactionRegistryEntity : Entity<int>
    {
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public TransactionStatusType Status { get; set; }

        /// <summary>
        /// Gets or sets the record key.
        /// </summary>
        /// <value>The record key.</value>
        public Guid RecordKey { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>The created date time.</value>
        public DateTime CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the transaction key.
        /// </summary>
        /// <value>The transaction key.</value>
        public string IncomingHash { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>The summary.</value>
        public Dictionary<string, object> Summary { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the entity category operation id.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; set; }

        /// <summary>
        /// Gets or sets the updated date time.
        /// </summary>
        /// <value>The updated date time.</value>
        public DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int? Priority { get; set; }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionRegistryMemento)memento;
            this.RecordKey = myMemento.RecordKey;
            this.Status = myMemento.Status;
            this.CreatedDateTime = myMemento.CreatedDateTime;
            this.Data = myMemento.Data;
            this.Id = myMemento.TransactionId;
            this.IncomingHash = myMemento.IncomingHash;
            this.EntityCategoryOperationId = myMemento.EntityCategoryOperationId;
            this.UpdatedDateTime = myMemento.UpdatedDateTime;
            this.Message = myMemento.Message;
            this.Details = myMemento.Details;
            this.Priority = myMemento.Priority;
        }
    }
}