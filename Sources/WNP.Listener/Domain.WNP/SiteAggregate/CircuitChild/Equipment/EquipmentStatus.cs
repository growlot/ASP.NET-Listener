//-----------------------------------------------------------------------
// <copyright file="EquipmentStatus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate.CircuitChild.Equipment
{
    /// <summary>
    /// Equipment status.
    /// </summary>
    public sealed class EquipmentStatus : ValueObject<EquipmentStatus>
    {
        private readonly string status;

        /// <summary>
        /// Initializes a new instance of the <see cref="EquipmentStatus"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public EquipmentStatus(string status)
        {
            this.status = status;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status
        {
            get
            {
                return this.status;
            }
        }

        /// <summary>
        /// Determines whether equipment is retired.
        /// </summary>
        /// <returns>True if equipment is retired, false otherwise.</returns>
        public bool IsRetired()
        {
            return this.status == "R";
        }
    }
}
