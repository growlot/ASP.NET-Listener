//-----------------------------------------------------------------------
// <copyright file="DeviceTypeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Lookups
{
    /// <summary>
    /// List of possible device types
    /// </summary>
    public enum DeviceTypeLookup
    {
        /// <summary>
        /// The undefined value
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The electric meter
        /// </summary>
        ElectricMeter = 1,

        /// <summary>
        /// The current transformer
        /// </summary>
        CurrentTransformer = 2,

        /// <summary>
        /// The potential transformer
        /// </summary>
        PotentialTransformer = 3
    }
}
