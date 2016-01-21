// <copyright file="IApplicationConfigurationRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.Listener
{
    using AMSLLC.Listener.Domain.Listener.Application;

    /// <summary>
    /// Application configuration repository interface
    /// </summary>
    public interface IApplicationConfigurationRepository : IRepository
    {
        /// <summary>
        /// Gets the memento for the application, described by identifier
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns>The application configuration memento.</returns>
        ApplicationConfigurationMemento GetApplicationConfiguration(int applicationId);

        /// <summary>
        /// Saves the specified domain model.
        /// </summary>
        /// <param name="model">The domain model.</param>
        void SaveApplicationConfiguration(ApplicationConfiguration model);
    }
}
