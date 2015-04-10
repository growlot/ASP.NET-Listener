//-----------------------------------------------------------------------
// <copyright file="UpdateCommentRequest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using AMSLLC.Listener.Common.Model;

    /// <summary>
    /// Comment message format
    /// </summary>
    public class UpdateCommentRequest
    {
        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int? SiteId { get; set; }

        /// <summary>
        /// Gets or sets the comment index.
        /// </summary>
        /// <value>
        /// The comment index.
        /// </value>
        public int CommentIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether comment is popup.
        /// </summary>
        /// <value>
        ///   <c>true</c> if popup; otherwise, <c>false</c>.
        /// </value>
        public bool Popup { get; set; }
    }
}