//-----------------------------------------------------------------------
// <copyright file="Meter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing electric meter device 
    /// </summary>
    public class Meter : Equipment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        public Meter()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meter"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Meter(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the meter code.
        /// </summary>
        /// <value>
        /// The meter code.
        /// </value>
        public string MeterCode { get; set; }

        /// <summary>
        /// Gets or sets the firmware revision 1.
        /// </summary>
        /// <value>
        /// The firmware revision 1.
        /// </value>
        public string FirmwareRevision1 { get; set; }

        /// <summary>
        /// Gets or sets the firmware revision 2.
        /// </summary>
        /// <value>
        /// The firmware revision 2.
        /// </value>
        public string FirmwareRevision2 { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// The phase.
        /// </value>
        public char Phase { get; set; }

        /// <summary>
        /// Gets or sets the wire.
        /// </summary>
        /// <value>
        /// The wire.
        /// </value>
        public char Wire { get; set; }

        /// <summary>
        /// Gets or sets the register ratio.
        /// </summary>
        /// <value>
        /// The register ratio.
        /// </value>
        public string RegisterRatio { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>
        /// The form.
        /// </value>
        public string Form { get; set; }

        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        public char Base { get; set; }

        /// <summary>
        /// Gets or sets the KH.
        /// </summary>
        /// <value>
        /// The KH.
        /// </value>
        public string KH { get; set; }

        /// <summary>
        /// Gets or sets the test amps.
        /// </summary>
        /// <value>
        /// The test amps.
        /// </value>
        public float TestAmps { get; set; }

        /// <summary>
        /// Gets or sets the test volts.
        /// </summary>
        /// <value>
        /// The test volts.
        /// </value>
        public float TestVolts { get; set; }

        /// <summary>
        /// Gets or sets the ami id1.
        /// </summary>
        /// <value>
        /// The ami id1.
        /// </value>
        public string AmiId1 { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public string ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 11.
        /// </summary>
        /// <value>
        /// The meter custom field 11.
        /// </value>
        public string CustomField11 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 12.
        /// </summary>
        /// <value>
        /// The meter custom field 12.
        /// </value>
        public string CustomField12 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 13.
        /// </summary>
        /// <value>
        /// The meter custom field 13.
        /// </value>
        public string CustomField13 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 14.
        /// </summary>
        /// <value>
        /// The meter custom field 14.
        /// </value>
        public string CustomField14 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 15.
        /// </summary>
        /// <value>
        /// The meter custom field 15.
        /// </value>
        public string CustomField15 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 16.
        /// </summary>
        /// <value>
        /// The meter custom field 16.
        /// </value>
        public string CustomField16 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 17.
        /// </summary>
        /// <value>
        /// The meter custom field 17.
        /// </value>
        public string CustomField17 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 18.
        /// </summary>
        /// <value>
        /// The meter custom field 18.
        /// </value>
        public string CustomField18 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 19.
        /// </summary>
        /// <value>
        /// The meter custom field 19.
        /// </value>
        public string CustomField19 { get; set; }

        /// <summary>
        /// Gets or sets the meter custom field 20.
        /// </summary>
        /// <value>
        /// The meter custom field 20.
        /// </value>
        public string CustomField20 { get; set; }
    }
}
