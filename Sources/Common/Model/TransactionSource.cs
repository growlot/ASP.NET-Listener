//-----------------------------------------------------------------------
// <copyright file="TransactionSource.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing TransactionStatus 
    /// </summary>
    public class TransactionSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSource"/> class.
        /// </summary>
        public TransactionSource()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSource"/> class.
        /// </summary>
        /// <param name="id">The transaction source identifier.</param>
        public TransactionSource(int id)
        {
            this.Id = id;
            this.Description = string.Empty;
        }

        /// <summary>
        /// Gets or sets the transaction source identifier.
        /// </summary>
        /// <value>
        /// The transaction source identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the transaction source description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
