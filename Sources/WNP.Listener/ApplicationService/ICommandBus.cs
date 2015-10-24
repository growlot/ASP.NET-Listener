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
    public interface ICommandBus
    {
        /// <summary>
        /// Subscribes the handler to specific application command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="handler">The handler.</param>
        void Subscribe<TCommand>(Action<TCommand> handler)
                    where TCommand : ICommand;

        /// <summary>
        /// Subscribes the asynchronous handler to specific application command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="handler">The handler.</param>
        void SubscribeAsync<TCommand>(Func<TCommand, Task> handler)
            where TCommand : ICommand;

        /// <summary>
        /// Publishes the specified application command.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="applicationCommand">The application command.</param>
        void Publish<TCommand>(TCommand applicationCommand)
            where TCommand : ICommand;

        /// <summary>
        /// Publishes the specified application command asynchronously.
        /// </summary>
        /// <typeparam name="TCommand">The type of the application command.</typeparam>
        /// <param name="applicationCommand">The application command.</param>
        /// <returns>The empty task.</returns>
        Task[] PublishAsync<TCommand>(TCommand applicationCommand)
            where TCommand : ICommand;
    }
}
