// <copyright file="UpdateEntityCategoryOperationCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Repository.WNP;

    /// <summary>
    /// [Update Entity Category Operation] command handler.
    /// </summary>
    public class UpdateEntityCategoryOperationCommandHandler : CommandHandlerBase,
        ICommandHandler<UpdateEntityCategoryOperationCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEntityCategoryOperationCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UpdateEntityCategoryOperationCommandHandler(
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
            UpdateEntityCategoryOperationCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}