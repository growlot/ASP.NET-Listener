//-----------------------------------------------------------------------
// <copyright file="Reading.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing reading 
    /// </summary>
    public class Reading
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Reading"/> class.
        /// </summary>
        public Reading()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reading"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Reading(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the reading identifier.
        /// </summary>
        /// <value>
        /// The reading identifier.
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
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the index of the reading.
        /// </summary>
        /// <value>
        /// The index of the reading.
        /// </value>
        public int ReadIndex { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime ReadDate { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string ReadingValue { get; set; }

        /// <summary>
        /// Gets or sets the read label.
        /// </summary>
        /// <value>
        /// The read label.
        /// </value>
        public string ReadLabel { get; set; }
    }
}
