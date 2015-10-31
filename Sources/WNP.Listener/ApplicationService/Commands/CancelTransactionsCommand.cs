// <copyright file="CancelTransactionsCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Cancel transactions command
    /// </summary>
    public class CancelTransactionsCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelTransactionsCommand"/> class.
        /// </summary>
        public CancelTransactionsCommand()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelTransactionsCommand"/> class.
        /// </summary>
        /// <param name="recordKeys">The record keys.</param>
        public CancelTransactionsCommand(Collection<Guid> recordKeys)
        {
            if (recordKeys != null)
            {
                foreach (var recordKey in recordKeys)
                {
                    this.RecordKeys.Add(recordKey);
                }
            }
        }

        /// <summary>
        /// Gets the record keys.
        /// </summary>
        /// <value>The record keys.</value>
        public Collection<Guid> RecordKeys { get; } = new Collection<Guid>();
    }
}