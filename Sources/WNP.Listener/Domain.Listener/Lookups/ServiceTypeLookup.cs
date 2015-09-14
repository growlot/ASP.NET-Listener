//-----------------------------------------------------------------------
// <copyright file="ServiceTypeLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Lookups
{
    /// <summary>
    /// List of possible service types
    /// </summary>
    public enum ServiceTypeLookup
    {
        /// <summary>
        /// The undefined
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The electric
        /// </summary>
        Electric = 1,

        /// <summary>
        /// The gas
        /// </summary>
        Gas = 2
    }
}
