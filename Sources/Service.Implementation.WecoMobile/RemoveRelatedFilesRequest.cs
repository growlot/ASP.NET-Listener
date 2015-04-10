//-----------------------------------------------------------------------
// <copyright file="RemoveRelatedFilesRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System.Collections.Generic;

    /// <summary>
    /// Related files message for web service
    /// </summary>
    public class RemoveRelatedFilesRequest
    {
        /// <summary>
        /// Gets or sets the related files.
        /// </summary>
        /// <value>
        /// The related files.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This is needed for WCF access")]
        public ICollection<RemoveRelatedFileRequest> RelatedFiles { get; set; }
    }
}