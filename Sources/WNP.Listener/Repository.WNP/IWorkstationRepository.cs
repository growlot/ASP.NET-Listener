// <copyright file="IWorkstationRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.WNP
{
    using System.Threading.Tasks;
    using Domain;

    /// <summary>
    /// Repository to access workstation aggregate root
    /// </summary>
    public interface IWorkstationRepository : IRepository
    {
        /// <summary>
        /// Gets the workstation asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The workstation memento</returns>
        Task<IMemento> GetWorkstationAsync(string name);

        /// <summary>
        /// Gets the location asynchronously.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The location memento</returns>
        Task<IMemento> GetLocationAsync(string name);

        /// <summary>
        /// Gets the state of the equipment asynchronously.
        /// </summary>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The equipment state memento</returns>
        Task<IMemento> GetEquipmentStateAsync(string equipmentType, string equipmentNumber);
    }
}
