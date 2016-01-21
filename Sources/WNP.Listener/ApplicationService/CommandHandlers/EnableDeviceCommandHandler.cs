// //-----------------------------------------------------------------------
// <copyright file="EnableDeviceCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System;
    using System.Threading.Tasks;
    using Commands;
    using Domain.Listener.Application;
    using Implementations;
    using Repository.Listener;

    /// <summary>
    /// Application configuration service
    /// </summary>
    public class EnableDeviceCommandHandler : ICommandHandler<EnableDeviceCommand>
    {
        /// <inheritdoc/>
        public Task HandleAsync(EnableDeviceCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Can not process command if it is not specified.");
            }

            using (var scope = ApplicationServiceScope.Create())
            {
                var listenerRepository = scope.RepositoryBuilder.Create<IApplicationConfigurationRepository>();
                var model = scope.DomainBuilder.Create<ApplicationConfiguration>(listenerRepository.GetApplicationConfiguration(command.ApplicationId));
                model.EnableDeviceType((int)command.DeviceType);
                listenerRepository.SaveApplicationConfiguration(model);
            }

            return Task.CompletedTask;
        }
    }
}