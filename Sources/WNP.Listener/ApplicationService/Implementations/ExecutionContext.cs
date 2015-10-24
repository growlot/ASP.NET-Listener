// //-----------------------------------------------------------------------
// <copyright file="ExecutionContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Implements <see cref="IExecutionContext"/>
    /// </summary>
    public class ExecutionContext : IExecutionContext
    {
        private readonly ConcurrentBag<ErrorMessage> errorMessages = new ConcurrentBag<ErrorMessage>();

        /// <inheritdoc/>
        public bool Valid => this.errorMessages.IsEmpty;

        /// <inheritdoc/>
        public void AddError(string message)
        {
            this.AddError(message, null);
        }

        /// <inheritdoc/>
        public void AddError(string message, string associatedProperty)
        {
            this.errorMessages.Add(new ErrorMessage { Message = message, Property = associatedProperty });
        }

        /// <inheritdoc/>
        public IReadOnlyCollection<ErrorMessage> GetErrors()
        {
            return this.errorMessages.ToList().AsReadOnly();
        }
    }
}