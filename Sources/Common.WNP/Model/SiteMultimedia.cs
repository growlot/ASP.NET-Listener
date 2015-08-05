//-----------------------------------------------------------------------
// <copyright file="SiteMultimedia.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing site multimedia
    /// </summary>
    public class SiteMultimedia : BaseMultimedia
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMultimedia"/> class.
        /// </summary>
        public SiteMultimedia()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMultimedia"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public SiteMultimedia(int id)
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