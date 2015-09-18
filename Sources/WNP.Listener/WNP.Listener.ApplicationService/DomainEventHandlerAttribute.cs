// //-----------------------------------------------------------------------
// // <copyright file="DomainEventHandlerAttribute.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService
{
    using System;
    using AMSLLC.Listener.Domain;
    using Utilities;

    /// <summary>
    /// Marker attribute to identify which event should be handled by the given handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DomainEventHandlerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventHandlerAttribute"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DomainEventHandlerAttribute(Type type)
        {
            FailFast.AssertTrue(() => typeof (IEvent).IsAssignableFrom(type), "Type must be of IEvent");
            Type = type;
        }

        /// <summary>
        /// Gets or sets the event handler type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type { get; set; }
    }
}