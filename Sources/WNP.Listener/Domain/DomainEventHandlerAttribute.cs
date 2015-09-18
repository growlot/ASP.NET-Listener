// //-----------------------------------------------------------------------
// // <copyright file="DomainEventHandlerAttribute.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain
{
    using System;
    using Core;

    /// <summary>
    /// Marker attribute to identify which event should be handled by the given handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DomainEventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventHandlerAttribute" /> class.
        /// </summary>
        /// <param name="eventType">Type of the data.</param>
        public DomainEventHandlerAttribute(Type eventType)
        {
            FailFast.AssertTrue(() => typeof(IEvent).IsAssignableFrom(eventType), "Type must be of {0}".FormatWith(typeof(IEvent).Name));
            this.EventType = eventType;
        }

        /// <summary>
        /// Gets or sets the event handler type.
        /// </summary>
        /// <value>The type.</value>
        public Type EventType { get; set; }
    }
}