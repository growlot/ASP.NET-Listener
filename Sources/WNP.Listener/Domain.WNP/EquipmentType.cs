// <copyright file="EquipmentType.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Value object for equipment types.
    /// </summary>
    public class EquipmentType : ValueObject<EquipmentType>
    {
        private static ICollection<string> supportedEquipentTypes = new List<string>(new[] { "EM", "CT", "PT" });
        private readonly string equipmentTypeCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentType"/> class.
        /// </summary>
        /// <param name="equipmentTypeCode">The equipment type code.</param>
        public EquipmentType(string equipmentTypeCode)
        {
            if (!supportedEquipentTypes.Contains(equipmentTypeCode))
            {
                throw new ArgumentOutOfRangeException(nameof(equipmentTypeCode), "Equipment type code is not recognized.");
            }

            this.equipmentTypeCode = equipmentTypeCode;
        }

        /// <summary>
        /// The electric meter
        /// </summary>
        public static EquipmentType ElectricMeter
        {
            get { return new EquipmentType("EM"); }
        }

        /// <summary>
        /// The current transformer
        /// </summary>
        public static EquipmentType CurrentTransformer
        {
            get { return new EquipmentType("CT"); }
        }

        /// <summary>
        /// The potential transformer
        /// </summary>
        public static EquipmentType PotentialTransformer
        {
            get { return new EquipmentType("PT"); }
        }

        /// <summary>
        /// Gets the equipment type code.
        /// </summary>
        /// <value>
        /// The equipment type code.
        /// </value>
        public string Code
        {
            get
            {
                return this.equipmentTypeCode;
            }
        }
    }
}
