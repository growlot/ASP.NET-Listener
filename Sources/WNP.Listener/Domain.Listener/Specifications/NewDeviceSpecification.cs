//-----------------------------------------------------------------------
// <copyright file="NewDeviceSpecification.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Model
{
    using Model;

    /// <summary>
    /// Specification that checks if device is new, not ppresent in the repository.
    /// </summary>
    public sealed class NewDeviceSpecification : ISpecification<Device, int>
    {
        /// <summary>
        /// The transaction log repository
        /// </summary>
        private readonly ITransactionLogRepository transactionLogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewDeviceSpecification"/> class.
        /// </summary>
        /// <param name="transactionLogRepository">The transaction log repository.</param>
        public NewDeviceSpecification(ITransactionLogRepository transactionLogRepository)
        {
            this.transactionLogRepository = transactionLogRepository;
        }

        /// <summary>
        /// Determines whether specified device is already present in repository.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <returns>True if device does not exist in repository, false otherwise.</returns>
        public bool IsSatisfiedBy(Device device)
        {
            Device foundDevice = this.transactionLogRepository.FindDeviceByEquipmentNumber(device.EquipmentNumber, device.DeviceTypeId);
            return foundDevice == null;
        }
    }
}
