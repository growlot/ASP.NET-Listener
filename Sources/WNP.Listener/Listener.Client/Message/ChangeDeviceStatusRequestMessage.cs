// <copyright file="ChangeDeviceStatusRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    /// <summary>
    /// Change device status request
    /// </summary>
    public class ChangeDeviceStatusRequestMessage : BaseListenerRequestMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDeviceStatusRequestMessage"/> class.
        /// </summary>
        /// <param name="entityCategory">The entity category.</param>
        public ChangeDeviceStatusRequestMessage(string entityCategory)
            : base(entityCategory, "StateChange")
        {
        }

        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        /// <value>The entity key.</value>
        public string EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }
    }
}
