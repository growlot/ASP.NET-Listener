//-----------------------------------------------------------------------
// <copyright file="Device.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing Device
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        public Device()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="id">The device identifier.</param>
        public Device(int id)
        {
            this.Id = id;
            this.Comments = new List<Comment>();
            this.RelatedFiles = new List<RelatedFile>();
            this.Tests = new List<DeviceTest>();
        }

        /// <summary>
        /// Gets or sets the device identifier.
        /// </summary>
        /// <value>
        /// The device identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets company who owns the device.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public Company Company { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public EquipmentType EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets site comment info
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets site multimedia info
        /// </summary>
        /// <value>
        /// The related files.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<RelatedFile> RelatedFiles { get; set; }

        /// <summary>
        /// Gets or sets the test volts.
        /// </summary>
        /// <value>
        /// The test volts.
        /// </value>
        public decimal TestVolts { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>
        /// The form.
        /// </value>
        public string Form { get; set; }

        /// <summary>
        /// Gets or sets the test amps.
        /// </summary>
        /// <value>
        /// The test amps.
        /// </value>
        public decimal TestAmps { get; set; }

        /// <summary>
        /// Gets or sets the KH.
        /// </summary>
        /// <value>
        /// The KH.
        /// </value>
        public string KH { get; set; }

        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        public char Base { get; set; }

        /// <summary>
        /// Gets or sets the tests.
        /// </summary>
        /// <value>
        /// The tests.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Needed for WCF")]
        public IList<DeviceTest> Tests { get; set; }

        /// <summary>
        /// Gets or sets  the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the model number.
        /// </summary>
        /// <value>
        /// The model number.
        /// </value>
        public string ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the test program.
        /// </summary>
        /// <value>
        /// The test program.
        /// </value>
        public string TestProgram { get; set; }

        /// <summary>
        /// Gets or sets  the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets  the equipment status.
        /// </summary>
        /// <value>
        /// The equipment status.
        /// </value>
        public string EquipmentStatus { get; set; }

        /// <summary>
        /// Gets or sets the equipment status date.
        /// </summary>
        /// <value>
        /// The equipment status date.
        /// </value>
        public DateTime? EquipmentStatusDate { get; set; }

        /// <summary>
        /// Gets or sets  the shop status.
        /// </summary>
        /// <value>
        /// The shop status.
        /// </value>
        public string ShopStatus { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        /// <value>
        /// The vehicle.
        /// </value>
        public Vehicle Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the user who checked-out the device.
        /// </summary>
        /// <value>
        /// The user who received the device.
        /// </value>
        public User ReceivedBy { get; set; }
    }
}
