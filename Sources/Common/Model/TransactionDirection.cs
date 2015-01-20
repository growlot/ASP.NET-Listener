//-----------------------------------------------------------------------
// <copyright file="TransactionDirection.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Data model class representing TransactionDirection 
    /// </summary>
    public class TransactionDirection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDirection"/> class.
        /// </summary>
        public TransactionDirection()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionDirection"/> class.
        /// </summary>
        /// <param name="id">The transaction direction identifier.</param>
        public TransactionDirection(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the transaction direction identifier.
        /// </summary>
        /// <value>
        /// The transaction direction identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction direction description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}