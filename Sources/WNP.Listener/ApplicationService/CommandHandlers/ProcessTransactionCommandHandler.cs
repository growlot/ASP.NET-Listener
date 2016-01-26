// <copyright file="ProcessTransactionCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Repository.WNP;

    /// <summary>
    ///     Process Transaction command handler
    /// </summary>
    public class ProcessTransactionCommandHandler : CommandHandlerBase, ICommandHandler<ProcessTransactionCommand>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcessTransactionCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public ProcessTransactionCommandHandler(
            IWNPUnitOfWork unitOfWork,
            IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>The empty task.</returns>
        public Task HandleAsync(
            ProcessTransactionCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}