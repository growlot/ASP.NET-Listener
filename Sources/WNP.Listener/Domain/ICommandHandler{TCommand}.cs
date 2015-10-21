//-----------------------------------------------------------------------
// <copyright file="ICommandHandler{TCommand}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for all domain command handlers.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<TCommand>
            where TCommand : ICommand
        {
        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The empty task.</returns>
        Task Handle(TCommand command);
    }
}
