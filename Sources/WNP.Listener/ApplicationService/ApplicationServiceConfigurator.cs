// <copyright file="ApplicationServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using CommandHandlers;
    using Commands;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;

    /// <summary>
    /// Configures application services by registering command handlers
    /// </summary>
    public class ApplicationServiceConfigurator
    {
        private ICommandBus commandBus;
        private IDomainEventBus domainEventBus;
        private ITransactionService transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationServiceConfigurator" /> class.
        /// </summary>
        /// <param name="transactionService">The transaction service.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        /// <param name="commandBus">The command bus.</param>
        public ApplicationServiceConfigurator(ITransactionService transactionService, IDomainEventBus domainEventBus, ICommandBus commandBus)
        {
            this.transactionService = transactionService;
            this.domainEventBus = domainEventBus;
            this.commandBus = commandBus;
        }

        /// <summary>
        /// Registers command handlers for all commands.
        /// </summary>
        public void RegisterCommandHandlers()
        {
            this.commandBus.SubscribeAsync<CreateSiteCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<CreateSiteCommandHandler>().Handle(command));
        }

        /// <summary>
        /// Registers the handlers for all Saga's.
        /// </summary>
        public void RegisterSagaHandlers()
        {
            this.domainEventBus.SubscribeAsync<TransactionSkipped>(
                domainEvent => this.transactionService.Skipped(new SkipTransactionCommand { RecordKey = domainEvent.RecordKey }));
        }
    }
}
