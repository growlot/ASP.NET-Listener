//-----------------------------------------------------------------------
// <copyright file="IEquipmentTestResult.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Interface representing basic equipment test result
    /// </summary>
    public interface IEquipmentTestResult
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the test date.
        /// </summary>
        /// <value>
        /// The test date.
        /// </value>
        DateTime TestDate { get; set; }

        /// <summary>
        /// Gets or sets the test result step number.
        /// </summary>
        /// <value>
        /// The test result step number.
        /// </value>
        int StepNumber { get; set; }
    }
}
