// //-----------------------------------------------------------------------
// // <copyright file="IExecutionContext.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;
    using Core;

    public interface IExecutionContext
    {
        bool Valid { get; }
        void AddError(string message, string associatedProperty = null);

        IReadOnlyCollection<StatusMessage> GetErrors();
    }
}