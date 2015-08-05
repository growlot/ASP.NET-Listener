//-----------------------------------------------------------------------
// <copyright file="CurrentTransformerTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing electric meter test results
    /// </summary>
    public class CurrentTransformerTestResult : TransformerTestResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTransformerTestResult"/> class.
        /// </summary>
        public CurrentTransformerTestResult()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTransformerTestResult"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public CurrentTransformerTestResult(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the test current (Amps).
        /// </summary>
        /// <value>
        /// The test current (Amps).
        /// </value>
        public decimal TestCurrent { get; set; }

        /// <summary>
        /// Gets or sets the burden.
        /// </summary>
        /// <value>
        /// The burden.
        /// </value>
        public decimal Burden { get; set; }

        /// <summary>
        /// Gets or sets the secondary current.
        /// </summary>
        /// <value>
        /// The secondary current.
        /// </value>
        public decimal SecondaryCurrent { get; set; }
    }
}
