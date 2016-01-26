// //-----------------------------------------------------------------------
// <copyright file="EndpointTriggerType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener
{
    /// <summary>
    /// Defines when transaction should be triggered.
    /// </summary>
    public enum EndpointTriggerType
    {
        /// <summary>
        /// The undefined, shoul not be used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The transaction is always executed
        /// </summary>
        Always = 1,

        /// <summary>
        /// The transaction is only executed if hash has changed
        /// </summary>
        Changed = 2,

        /// <summary>
        /// The transaction is only executed if hash hasn't cahnged
        /// </summary>
        Unchanged = 3
    }
}