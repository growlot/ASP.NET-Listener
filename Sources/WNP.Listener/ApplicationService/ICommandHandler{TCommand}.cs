//-----------------------------------------------------------------------
// <copyright file="ICommandHandler{TCommand}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for all application command handlers.
    /// </summary>
    /// <remarks>
    /// Command Handler is a domain service that turns a command into events by calling methods on an aggregate
    /// root and publishes the resulting events.
    /// Stateless with just behavior; just a task based method and sort of procedural.
    /// </remarks>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<TCommand>
            where TCommand : ICommand
        {
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The empty task.</returns>
        Task HandleAsync(TCommand command);
    }
}
