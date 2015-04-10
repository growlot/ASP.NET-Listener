//-----------------------------------------------------------------------
// <copyright file="Comment.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing Comment entity
    /// </summary>
    public class Comment
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
            this.Troubles = new List<Trouble>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the index of the comment.
        /// </summary>
        /// <value>
        /// The index of the comment.
        /// </value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the comment text.
        /// </summary>
        /// <value>The comment text.</value>
        public string Text { get; set; }

        /// <summary>
        /// Gets troubles registered while entering comment
        /// </summary>
        /// <value>
        /// The troubles.
        /// </value>
        public IList<Trouble> Troubles { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Comment"/> is popup.
        /// </summary>
        /// <value>
        ///   <c>true</c> if popup; otherwise, <c>false</c>.
        /// </value>
        public bool Popup { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>The create date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the create by.
        /// </summary>
        /// <value>The create by.</value>
        public string CreatedBy { get; set; }
    }
}