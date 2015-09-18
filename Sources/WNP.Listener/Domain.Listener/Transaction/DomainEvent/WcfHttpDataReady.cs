// //-----------------------------------------------------------------------
// // <copyright file="WcfHttpDataReady.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent
{
    using System;

    /// <summary>
    /// WcfDataReady event arguments
    /// </summary>
    public class WcfHttpDataReady : IEvent
    {
        /// <summary>
        /// Gets or sets the target endpoint.
        /// </summary>
        /// <value>The target endpoint.</value>
        public Uri TargetEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        /// <value>The message body.</value>
        public object Message { get; set; }
    }
}