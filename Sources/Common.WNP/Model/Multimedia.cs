//-----------------------------------------------------------------------
// <copyright file="Multimedia.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing multimedia 
    /// </summary>
    public class Multimedia : BaseMultimedia
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Multimedia"/> class.
        /// </summary>
        public Multimedia()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Multimedia"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Multimedia(int id)
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
    }
}