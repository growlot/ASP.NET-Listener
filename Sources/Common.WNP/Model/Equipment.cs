//-----------------------------------------------------------------------
// <copyright file="Equipment.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing fields common for all devices 
    /// </summary>
    public class Equipment : IEquipment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

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
        /// Gets or sets  the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>
        /// The serial number.
        /// </value>
        public string SerialNumber { get; set; }

        /// <summary>
        /// Gets or sets the model number.
        /// </summary>
        /// <value>
        /// The model number.
        /// </value>
        public string ModelNumber { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the new batch.
        /// </summary>
        /// <value>
        /// The new batch.
        /// </value>
        public NewBatch NewBatch { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 1.
        /// </summary>
        /// <value>
        /// The meter custom field 1.
        /// </value>
        public string CustomField1 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 2.
        /// </summary>
        /// <value>
        /// The meter custom field 2.
        /// </value>
        public string CustomField2 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 3.
        /// </summary>
        /// <value>
        /// The meter custom field 3.
        /// </value>
        public string CustomField3 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 4.
        /// </summary>
        /// <value>
        /// The meter custom field 4.
        /// </value>
        public string CustomField4 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 5.
        /// </summary>
        /// <value>
        /// The meter custom field 5.
        /// </value>
        public string CustomField5 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 6.
        /// </summary>
        /// <value>
        /// The meter custom field 6.
        /// </value>
        public string CustomField6 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 7.
        /// </summary>
        /// <value>
        /// The meter custom field 7.
        /// </value>
        public string CustomField7 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 8.
        /// </summary>
        /// <value>
        /// The meter custom field 8.
        /// </value>
        public string CustomField8 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 9.
        /// </summary>
        /// <value>
        /// The meter custom field 9.
        /// </value>
        public string CustomField9 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 10.
        /// </summary>
        /// <value>
        /// The meter custom field 10.
        /// </value>
        public string CustomField10 { get; set; }
    }
}
