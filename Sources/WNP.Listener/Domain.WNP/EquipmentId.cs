// <copyright file="EquipmentId.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP
{
    using System;

    /// <summary>
    /// Class that represents equipment identifier.
    /// </summary>
    public class EquipmentId : ValueObject<EquipmentId>
    {
        private readonly EquipmentType equipmentType;
        private readonly string equipmentNumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentId" /> class.
        /// </summary>
        /// <param name="equipmentTypeCode">The equipment type code.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        public EquipmentId(string equipmentTypeCode, string equipmentNumber)
        {
            if (string.IsNullOrWhiteSpace(equipmentNumber))
            {
                throw new ArgumentException("Equipment number must be specified to construct equipment identifier.", nameof(equipmentNumber));
            }

            this.equipmentType = new EquipmentType(equipmentTypeCode);
            this.equipmentNumber = equipmentNumber;
        }

        /// <summary>
        /// Gets the type of the equipment.
        /// </summary>
        /// <value>
        /// The type of the equipment.
        /// </value>
        public string EquipmentType
        {
            get
            {
                return this.equipmentType.Code;
            }
        }

        /// <summary>
        /// Gets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        public string EquipmentNumber
        {
            get
            {
                return this.equipmentNumber;
            }
        }
    }
}
