//-----------------------------------------------------------------------
// <copyright file="DeviceTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;

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
            this.MeterTestSteps = new List<MeterTestStep>();
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

        /// <summary>
        /// Gets or sets the end of test date.
        /// </summary>
        /// <value>
        /// The end of test date.
        /// </value>
        public DateTime TestDateStop { get; set; }

        /// <summary>
        /// Gets or sets the primary test reason.
        /// </summary>
        /// <value>
        /// The primary test reason.
        /// </value>
        public string PrimaryTestReason { get; set; }

        /// <summary>
        /// Gets or sets the secondary test reason.
        /// </summary>
        /// <value>
        /// The secondary test reason.
        /// </value>
        public string SecondaryTestReason { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the tester identifier.
        /// </summary>
        /// <value>
        /// The tester identifier.
        /// </value>
        public string TesterId { get; set; }

        /// <summary>
        /// Gets or sets the board number.
        /// </summary>
        /// <value>
        /// The board number.
        /// </value>
        public string BoardNumber { get; set; }

        /// <summary>
        /// Gets or sets the meter test steps.
        /// </summary>
        /// <value>
        /// The meter test steps.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Needed for WCF")]
        public IList<MeterTestStep> MeterTestSteps { get; set; }
    }
}
