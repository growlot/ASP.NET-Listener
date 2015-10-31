// <copyright file="ApplicationServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommandHandlers;
    using Commands;
    using Communication;
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

            this.domainEventBus.SubscribeAsync<TransactionDataReady>(domainEvent =>
            {
                var dispatcher = ApplicationIntegration.DependencyResolver.ResolveNamed<ICommunicationHandler>("communication-{0}".FormatWith(domainEvent.Endpoint.Protocol));
                return this.transactionService.Processing(
                    new ProcessingTransactionCommand
                    {
                        RecordKey = domainEvent.RecordKey
                    })
                    .ContinueWith(
                        t =>
                            t.IsCompleted
                                ? dispatcher.Handle(
                                    domainEvent,
                                    domainEvent.Endpoint.ConnectionConfiguration,
                                    domainEvent.Endpoint.ProtocolConfiguration)
                                : t);
            });
        }
    }
}
