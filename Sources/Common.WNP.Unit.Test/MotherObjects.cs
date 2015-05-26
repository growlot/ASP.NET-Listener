//-----------------------------------------------------------------------
// <copyright file="MotherObjects.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Unit.Test
{
    using System;
    using AMSLLC.Listener.Common.WNP.Model;

    /// <summary>
    /// Default test objects
    /// </summary>
    public static class MotherObjects
    {
        /// <summary>
        /// Constructs the default meter object.
        /// </summary>
        /// <returns>The meter.</returns>
        public static Meter DefaultMeter()
        {
            Meter meter = new Meter()
            {
                Id = 1,
                MeterCode = "AA0",
                EquipmentNumber = "23456789",
                CustomField13 = "Y",
                CreateDate = new DateTime(2014, 1, 20),
                Owner = DefaultOwner()
            };

            return meter;
        }

        /// <summary>
        /// Constructs the default owner object.
        /// </summary>
        /// <returns>The owner.</returns>
        public static Owner DefaultOwner()
        {
            Owner owner = new Owner()
            {
                Id = 1,
                Description = "OwnerName"
            };

            return owner;
        }
    }
}
