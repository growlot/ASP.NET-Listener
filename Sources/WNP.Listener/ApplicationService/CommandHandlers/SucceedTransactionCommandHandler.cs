// <copyright file="SucceedTransactionCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using Domain;
    using Repository.WNP;

    /// <summary>
    /// Succeed Transaction command handler
    /// </summary>
    public class SucceedTransactionCommandHandler : CommandHandlerBase
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SucceedTransactionCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public SucceedTransactionCommandHandler(
            IWNPUnitOfWork unitOfWork,
            IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }
    }
}