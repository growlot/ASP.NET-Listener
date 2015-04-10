//-----------------------------------------------------------------------
// <copyright file="RemoveCommentsRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System.Collections.Generic;

    /// <summary>
    /// Comments message for web service
    /// </summary>
    public class RemoveCommentsRequest
    {
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is needed for WCF access")]
        public ICollection<RemoveCommentRequest> Comments { get; set; }
    }
}