using AMSLLC.Listener.Domain.Listener.Application;

namespace Repository
{
    /// <summary>
    /// Application configuration repository interface
    /// </summary>
    public interface IApplicationConfigurationRepository : IRepository
    {
        /// <summary>
        /// Gets the memento for the application, described by identifier
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <returns>ApplicationConfigurationMemento.</returns>
        ApplicationConfigurationMemento Get(int applicationId);

        /// <summary>
        /// Saves the specified domain model.
        /// </summary>
        /// <param name="model">The domain model.</param>
        void Save(ApplicationConfiguration model);
    }
}
