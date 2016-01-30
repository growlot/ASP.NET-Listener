// <copyright file="HashData.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Class HashData.
    /// </summary>
    public class HashData
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the operation transaction key.
        /// </summary>
        /// <value>The operation transaction key.</value>
        public Guid OperationTransactionKey { get; set; }

        /// <summary>
        /// Gets the field configuration.
        /// </summary>
        /// <value>The field configuration.</value>
        public FieldConfigurationCollection FieldConfiguration { get; } = new FieldConfigurationCollection();
    }
}