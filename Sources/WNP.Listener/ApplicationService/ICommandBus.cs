// <copyright file="ICommandBus.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for domain event bus.
    /// </summary>
    /// <remarks>
    /// A mechanism for publishing commands and subscribing handlers to these commands.
    /// The only behavior is publishing commands and subscribing command handlers to commands;
    /// the only state is what's required to track which handler have subscribed to which command.
    /// </remarks>
    public interface ICommandBus
    {
        /// <summary>
        /// Subscribes the handler to specific application command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="handler">The handler.</param>
        void Subscribe<TCommand>(Func<TCommand, Task> handler)
            where TCommand : ICommand;

        /// <summary>
        /// Publishes the specified application command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="applicationCommand">The application command.</param>
        /// <returns>The list of unifinished tasks.</returns>
        Task PublishAsync<TCommand>(TCommand applicationCommand)
            where TCommand : ICommand;
    }
}
