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
        /// Gets the workstation.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The workstation memento</returns>
        Task<IMemento> GetWorkstation(string name);

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The location memento</returns>
        Task<IMemento> GetLocation(string name);

        /// <summary>
        /// Gets the state of the equipment.
        /// </summary>
        /// <param name="equipmentType">Type of the equipment.</param>
        /// <param name="equipmentNumber">The equipment number.</param>
        /// <returns>The equipment state memento</returns>
        Task<IMemento> GetEquipmentState(string equipmentType, string equipmentNumber);
    }
}
