// <copyright file="FailTransactionCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Repository.WNP;

    /// <summary>
    /// Fail Transaction command handler
    /// </summary>
    public class FailTransactionCommandHandler : CommandHandlerBase, ICommandHandler<FailTransactionCommand>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FailTransactionCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public FailTransactionCommandHandler(
            IWNPUnitOfWork unitOfWork,
            IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FailTransactionCommandHandler" /> class.
        /// </summary>
        /// <param name="domainEventBus">The domain event bus.</param>
        public FailTransactionCommandHandler(IDomainEventBus domainEventBus)
            : base(domainEventBus)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(
            FailTransactionCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}