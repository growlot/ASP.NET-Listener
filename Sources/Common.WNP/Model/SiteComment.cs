//-----------------------------------------------------------------------
// <copyright file="SiteComment.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing site comment
    /// </summary>
    public class SiteComment : BaseComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteComment"/> class.
        /// </summary>
        public SiteComment()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteComment"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public SiteComment(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public Site Site { get; set; }
    }
}
