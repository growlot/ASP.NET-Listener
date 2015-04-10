//-----------------------------------------------------------------------
// <copyright file="Comment.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing comment 
    /// </summary>
    public class Comment : BaseComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Comment(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the source of comment.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the trouble 1.
        /// </summary>
        /// <value>
        /// The trouble 1.
        /// </value>
        public string Trouble1 { get; set; }

        /// <summary>
        /// Gets or sets the trouble 2.
        /// </summary>
        /// <value>
        /// The trouble 2.
        /// </value>
        public string Trouble2 { get; set; }

        /// <summary>
        /// Gets or sets the trouble 3.
        /// </summary>
        /// <value>
        /// The trouble 3.
        /// </value>
        public string Trouble3 { get; set; }

        /// <summary>
        /// Gets or sets the trouble 4.
        /// </summary>
        /// <value>
        /// The trouble 4.
        /// </value>
        public string Trouble4 { get; set; }

        /// <summary>
        /// Gets or sets the trouble 5.
        /// </summary>
        /// <value>
        /// The trouble 5.
        /// </value>
        public string Trouble5 { get; set; }
    }
}
