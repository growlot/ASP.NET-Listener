//-----------------------------------------------------------------------
// <copyright file="IOdmRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    /// <summary>
    /// Interface for all ODM requests.
    /// </summary>
    public interface IOdmRequest
    {
        /// <summary>
        /// Gets or sets the listener transaction identifier.
        /// </summary>
        /// <value>
        /// The listener transaction identifier.
        /// </value>
#pragma warning disable SA1300 // already released code
        int listenerTransactionId { get; set; }
#pragma warning restore SA1300
    }
}
