// //-----------------------------------------------------------------------
// // <copyright file="ExecutionContext.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using AMSLLC.Core;

    public class ExecutionContext : IExecutionContext
    {
        private readonly ConcurrentBag<StatusMessage> _errorMessages = new ConcurrentBag<StatusMessage>();

        public bool Valid => this._errorMessages.IsEmpty;

        public void AddError(string message, string associatedProperty = null)
        {
            this._errorMessages.Add(new StatusMessage {Message = message, Target = associatedProperty});
        }

        public IReadOnlyCollection<StatusMessage> GetErrors()
        {
            return this._errorMessages.ToList().AsReadOnly();
        }
    }
}