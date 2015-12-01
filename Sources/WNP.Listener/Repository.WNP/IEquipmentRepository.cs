// <copyright file="IEquipmentRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP
{
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access different equipment types.
    /// </summary>
    public interface IEquipmentRepository : IRepository
    {
        /// <summary>
        /// Gets the electric meter.
        /// </summary>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The electric meter memento</returns>
        Task<IMemento> GetElectricMeter(string equipmentNumber);
    }
}
