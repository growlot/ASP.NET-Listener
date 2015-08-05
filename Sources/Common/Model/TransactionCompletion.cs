//-----------------------------------------------------------------------
// <copyright file="TransactionCompletion.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Data model class representing TransactionCompletion
    /// </summary>
    public class TransactionCompletion
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCompletion"/> class.
        /// </summary>
        public TransactionCompletion()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionCompletion"/> class.
        /// </summary>
        /// <param name="id">The transaction completion identifier.</param>
        public TransactionCompletion(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction completion identifier.
        /// </summary>
        /// <value>
        /// The transaction completion identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction completion description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
