//-----------------------------------------------------------------------
// <copyright file="IEquipment.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    /// <summary>
    /// Interface representing basic equipment
    /// </summary>
    public interface IEquipment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the equipment number.
        /// </summary>
        /// <value>
        /// The equipment number.
        /// </value>
        string EquipmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the new batch.
        /// </summary>
        /// <value>
        /// The new batch.
        /// </value>
        NewBatch NewBatch { get; set; }
    }
}
