// //-----------------------------------------------------------------------
// // <copyright file="DomainEventHandlerAttribute.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain
{
    using System;

    /// <summary>
    /// Marker attribute to identify which event should be handled by the given handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommunicationEventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationEventHandlerAttribute" /> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public CommunicationEventHandlerAttribute(string protocol)
        {
            this.Protocol = protocol;
        }

        /// <summary>
        /// Gets or sets the event handler type.
        /// </summary>
        /// <value>The type.</value>
        public string Protocol { get; set; }
    }
}