// //-----------------------------------------------------------------------
// // <copyright file="ApplicationConfigurationService.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.Impl
{
    using Domain.Listener.Application;
    using Domain.Listener.Lookups;
    using Repository;

    /// <summary>
    /// Application configuration service
    /// </summary>
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        /// <summary>
        /// Enable the device.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="deviceType">Type of the device.</param>
        public void EnableDevice(int applicationId, DeviceTypeLookup deviceType)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var listenerRepository = scope.RepositoryBuilder.Create<IApplicationConfigurationRepository>();
                var model = scope.DomainBuilder.Create<ApplicationConfiguration>(listenerRepository.Get(applicationId));
                model.EnableDeviceType((int) deviceType);
                listenerRepository.Save(model);
            }
        }
    }
}