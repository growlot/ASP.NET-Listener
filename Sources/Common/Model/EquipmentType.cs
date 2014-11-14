//-----------------------------------------------------------------------
// <copyright file="EquipmentType.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing EquipmentType 
    /// </summary>
    public class EquipmentType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentType"/> class.
        /// </summary>
        public EquipmentType()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentType"/> class.
        /// </summary>
        /// <param name="id">The equipment type identifier.</param>
        public EquipmentType(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the equipment type identifier.
        /// </summary>
        /// <value>
        /// The equipment type identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the service.
        /// </summary>
        /// <value>
        /// The type of the service.
        /// </value>
        public ServiceType ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the external code.
        /// </summary>
        /// <value>
        /// The external code.
        /// </value>
        public string ExternalCode { get; set; }

        /// <summary>
        /// Gets or sets the internal code.
        /// </summary>
        /// <value>
        /// The internal code.
        /// </value>
        public string InternalCode { get; set; }

        /// <summary>
        /// Gets or sets the equipment type description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}