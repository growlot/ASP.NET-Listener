//-----------------------------------------------------------------------
// <copyright file="CompanyConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Company
{
    /// <summary>
    /// Represents a company in listener transaction bounded context
    /// </summary>
    public sealed class CompanyConfiguration : Entity<int>, IOriginator
    {
        ////public void AddDevice(string equipmentNumber, int deviceTypeId)
        ////{
        ////    var device = new Device(this.Id, equipmentNumber, deviceTypeId);
        ////}

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
            var companyConfigurationMemento = (CompanyConfigurationMemento)memento;
            this.Id = companyConfigurationMemento.Id;
        }
    }
}
