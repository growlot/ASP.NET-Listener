//-----------------------------------------------------------------------
// <copyright file="NewBatch.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing new batch
    /// </summary>
    [Serializable]
    public class NewBatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewBatch"/> class.
        /// </summary>
        public NewBatch()
            : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewBatch"/> class.
        /// </summary>
        /// <param name="number">The new bath number.</param>
        public NewBatch(string number)
        {
            this.Number = number;
        }

        /// <summary>
        /// Gets or sets the new batch number.
        /// </summary>
        /// <value>
        /// The new batch number.
        /// </value>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the new batch status.
        /// </summary>
        /// <value>
        /// The new batch status.
        /// </value>
        public char Status { get; set; }

        /// <summary>
        /// Gets or sets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}