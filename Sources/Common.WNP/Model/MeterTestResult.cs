﻿//-----------------------------------------------------------------------
// <copyright file="MeterTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing electric meter test results 
    /// </summary>
    public class MeterTestResult : EquipmentTestResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestResult"/> class.
        /// </summary>
        public MeterTestResult()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeterTestResult"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public MeterTestResult(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public char Element { get; set; }

        /// <summary>
        /// Gets or sets the type of the test.
        /// </summary>
        /// <value>
        /// The type of the test.
        /// </value>
        public string TestType { get; set; }

        /// <summary>
        /// Gets or sets the test standard.
        /// </summary>
        /// <value>
        /// The test standard.
        /// </value>
        public string TestStandard { get; set; }

        /// <summary>
        /// Gets or sets as found.
        /// </summary>
        /// <value>
        /// As found.
        /// </value>
        public float AsFound { get; set; }

        /// <summary>
        /// Gets or sets as left.
        /// </summary>
        /// <value>
        /// As left.
        /// </value>
        public float AsLeft { get; set; }
        
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
        /// Gets or sets the test reason.
        /// </summary>
        /// <value>
        /// The test reason.
        /// </value>
        public string TestReason { get; set; }

        /// <summary>
        /// Gets or sets the station identifier.
        /// </summary>
        /// <value>
        /// The station identifier.
        /// </value>
        public string StationId { get; set; }
    }
}
