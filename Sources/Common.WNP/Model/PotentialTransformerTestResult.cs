//-----------------------------------------------------------------------
// <copyright file="PotentialTransformerTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing electric meter test results 
    /// </summary>
    public class PotentialTransformerTestResult : TransformerTestResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialTransformerTestResult"/> class.
        /// </summary>
        public PotentialTransformerTestResult()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialTransformerTestResult"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PotentialTransformerTestResult(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the test voltage.
        /// </summary>
        /// <value>
        /// The test voltage.
        /// </value>
        public decimal TestVoltage { get; set; }

        /// <summary>
        /// Gets or sets the secondary voltage.
        /// </summary>
        /// <value>
        /// The secondary voltage.
        /// </value>
        public decimal SecondaryVoltage { get; set; }

        /// <summary>
        /// Gets or sets the burden.
        /// </summary>
        /// <value>
        /// The burden.
        /// </value>
        public string Burden { get; set; }
    }
}
