//-----------------------------------------------------------------------
// <copyright file="DeviceTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;

    /// <summary>
    /// Data model class representing device test
    /// </summary>
    public class DeviceTest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTest"/> class.
        /// </summary>
        public DeviceTest()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTest"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public DeviceTest(int id)
        {
            this.Id = id;
            this.Device = null;
            this.TestDate = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        /// <value>
        /// The device.
        /// </value>
        public Device Device { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        public DateTime TestDate { get; set; }
    }
}
