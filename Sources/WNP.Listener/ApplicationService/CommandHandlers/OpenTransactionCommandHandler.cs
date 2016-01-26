// <copyright file="OpenTransactionCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Persistence.Listener;
    using Repository.WNP;

    /// <summary>
    /// Open Transaction command handler
    /// </summary>
    public class OpenTransactionCommandHandler : ListenerCommandHandlerBase, ICommandHandler<OpenTransactionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTransactionCommandHandler" /> class.
        /// </summary>
        /// <param name="domainEventBus">The domain event bus.</param>
        public OpenTransactionCommandHandler(
            IDomainEventBus domainEventBus)
            : base(domainEventBus)
        {
        }

        // [PersistenceContext]
        // private IListenerPersistenceContext PersistenceContext { get; set; }

        /// <inheritdoc/>
        public Task HandleAsync(
            OpenTransactionCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}