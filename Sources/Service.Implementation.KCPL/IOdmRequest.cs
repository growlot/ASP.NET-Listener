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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "listener", Justification = "Variable provided by conract.")]
        int listenerTransactionId { get; set; }
    }
}
