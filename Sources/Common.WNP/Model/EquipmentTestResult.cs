//-----------------------------------------------------------------------
// <copyright file="EquipmentTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing test results common for all devices
    /// </summary>
    public class EquipmentTestResult : IEquipmentTestResult
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
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        public DateTime TestDate { get; set; }

        /// <summary>
        /// Gets or sets the custom field1.
        /// </summary>
        /// <value>
        /// The custom field1.
        /// </value>
        public string CustomField1 { get; set; }

        /// <summary>
        /// Gets or sets the custom field2.
        /// </summary>
        /// <value>
        /// The custom field2.
        /// </value>
        public string CustomField2 { get; set; }

        /// <summary>
        /// Gets or sets the custom field3.
        /// </summary>
        /// <value>
        /// The custom field3.
        /// </value>
        public string CustomField3 { get; set; }

        /// <summary>
        /// Gets or sets the custom field4.
        /// </summary>
        /// <value>
        /// The custom field4.
        /// </value>
        public string CustomField4 { get; set; }

        /// <summary>
        /// Gets or sets the custom field5.
        /// </summary>
        /// <value>
        /// The custom field5.
        /// </value>
        public string CustomField5 { get; set; }

        /// <summary>
        /// Gets or sets the custom field6.
        /// </summary>
        /// <value>
        /// The custom field6.
        /// </value>
        public string CustomField6 { get; set; }

        /// <summary>
        /// Gets or sets the custom field7.
        /// </summary>
        /// <value>
        /// The custom field7.
        /// </value>
        public string CustomField7 { get; set; }

        /// <summary>
        /// Gets or sets the end of test date.
        /// </summary>
        /// <value>
        /// The end of test date.
        /// </value>
        public DateTime TestDateStop { get; set; }
    }
}
