// <copyright file="BatchTransactionEntry.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Model
{
    /// <summary>
    /// Batch transaction entry
    /// </summary>
    public class BatchTransactionEntry
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public string User { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }

        /// <summary>
        /// Gets or sets the operation key.
        /// </summary>
        /// <value>
        /// The operation key.
        /// </value>
        public string OperationKey { get; set; }

        /// <summary>
        /// Gets or sets the entity category.
        /// </summary>
        /// <value>The entity category.</value>
        public string EntityCategory { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }
    }
}