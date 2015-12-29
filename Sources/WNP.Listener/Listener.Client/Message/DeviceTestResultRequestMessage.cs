// <copyright file="DeviceTestResultRequestMessage.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Message
{
    using System;

    /// <summary>
    /// Class DeviceTestResultRequestMessage.
    /// </summary>
    public class DeviceTestResultRequestMessage : BaseListenerRequestMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTestResultRequestMessage"/> class.
        /// </summary>
        /// <param name="entityCategory">The entity category.</param>
        public DeviceTestResultRequestMessage(string entityCategory)
            : base(entityCategory, "Test")
        {
        }

        /// <summary>
        /// Gets or sets the entity key.
        /// </summary>
        /// <value>The entity key.</value>
        public string EntityKey { get; set; }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>The test date.</value>
        public DateTime TestDate { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public string Owner { get; set; }
    }
}
