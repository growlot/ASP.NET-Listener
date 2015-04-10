//-----------------------------------------------------------------------
// <copyright file="BaseComment.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing common fields for equipment and site comments 
    /// </summary>
    public class BaseComment
    {
        /// <summary>
        /// Gets or sets the comment identifier.
        /// </summary>
        /// <value>
        /// The comment identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the index of the comment.
        /// </summary>
        /// <value>
        /// The index of the comment.
        /// </value>
        public int CommentIndex { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string CommentText { get; set; }

        /// <summary>
        /// Gets or sets the type of the comment.
        /// </summary>
        /// <value>
        /// The type of the comment.
        /// </value>
        public char CommentType { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created this comment.
        /// </summary>
        /// <value>
        /// The user who created this comment.
        /// </value>
        public string CreateUser { get; set; }
    }
}
