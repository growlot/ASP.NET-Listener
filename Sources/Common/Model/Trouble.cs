//-----------------------------------------------------------------------
// <copyright file="Trouble.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing Trouble entity
    /// </summary>
    public class Trouble
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the site of this circuit.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public TroubleCode TroubleCode { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public Comment Comment { get; set; }
    }
}