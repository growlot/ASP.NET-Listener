//-----------------------------------------------------------------------
// <copyright file="MeterBarcode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Data model class representing electric meter barcode 
    /// </summary>
    [Serializable]
    public class MeterBarcode : Barcode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterBarcode"/> class.
        /// </summary>
        public MeterBarcode()
        {
            this.BarcodeId = new BarcodeIdentifier();
        }

        /// <summary>
        /// Gets or sets the wire.
        /// </summary>
        /// <value>
        /// The wire.
        /// </value>
        public int Wire { get; set; }

        /// <summary>
        /// Gets or sets the amp.
        /// </summary>
        /// <value>
        /// The amp.
        /// </value>
        public decimal Amp { get; set; }

        /// <summary>
        /// Gets or sets the volt.
        /// </summary>
        /// <value>
        /// The volt.
        /// </value>
        public decimal Volt { get; set; }

        /// <summary>
        /// Gets or sets the KH.
        /// </summary>
        /// <value>
        /// The KH.
        /// </value>
        public string KH { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>
        /// The phase.
        /// </value>
        public int Phase { get; set; }

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
        /// Gets or sets the register ratio.
        /// </summary>
        /// <value>
        /// The register ratio.
        /// </value>
        public string RegisterRatio { get; set; }

        /// <summary>
        /// Gets or sets the test revision.
        /// </summary>
        /// <value>
        /// The test revision.
        /// </value>
        public int TestRevision { get; set; }

        /// <summary>
        /// Gets or sets the standard mode.
        /// </summary>
        /// <value>
        /// The standard mode.
        /// </value>
        public string StandardMode { get; set; }

        /// <summary>
        /// Gets or sets the dwell time.
        /// </summary>
        /// <value>
        /// The dwell time.
        /// </value>
        public int DwellTime { get; set; }

        /// <summary>
        /// Gets or sets the optics.
        /// </summary>
        /// <value>
        /// The optics.
        /// </value>
        public string Optics { get; set; }

        /// <summary>
        /// Gets or sets the test time.
        /// </summary>
        /// <value>
        /// The test time.
        /// </value>
        public int TestTime { get; set; }

        /// <summary>
        /// Gets or sets the test progress measure.
        /// </summary>
        /// <value>
        /// The test progress measure.
        /// </value>
        public char TestProgressMeasure { get; set; }

        /// <summary>
        /// Gets or sets the test service.
        /// </summary>
        /// <value>
        /// The test service.
        /// </value>
        public string TestService { get; set; }

        /// <summary>
        /// Gets or sets the test sequence.
        /// </summary>
        /// <value>
        /// The test sequence.
        /// </value>
        public string TestSequence { get; set; }

        /// <summary>
        /// Gets or sets the test limit as found.
        /// </summary>
        /// <value>
        /// The test limit as found.
        /// </value>
        public int TestLimitAsFound { get; set; }

        /// <summary>
        /// Gets or sets the test limit as left.
        /// </summary>
        /// <value>
        /// The test limit as left.
        /// </value>
        public int TestLimitAsLeft { get; set; }
    }
}
