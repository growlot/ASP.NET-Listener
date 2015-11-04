﻿// <copyright file="ApplicationServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CommandHandlers;
    using Commands;
    using Communication;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Persistence.WNP.DomainEventHandlers;

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
        /// Registers command handlers for commands.
        /// </summary>
        public void RegisterCommandHandlers()
        {
            this.commandBus.Subscribe<CreateSiteCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<CreateSiteCommandHandler>().HandleAsync(command));
            this.commandBus.Subscribe<UpdateSiteBillingAccountCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<UpdateSiteBillingAccountCommandHandler>().HandleAsync(command));
            this.commandBus.Subscribe<UpdateSiteAddressCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<UpdateSiteAddressCommandHandler>().HandleAsync(command));
        }

        /// <summary>
        /// Registers the saga handlers for domain events.
        /// </summary>
        public void RegisterSagaHandlers()
        {
            this.domainEventBus.SubscribeAsync<TransactionSkipped>(
                domainEvent => this.transactionService.Skipped(new SkipTransactionCommand { RecordKey = domainEvent.RecordKey }));

            this.domainEventBus.SubscribeAsync<TransactionsCanceled>(
                domainEvent => this.transactionService.Cancel(new CancelTransactionsCommand(domainEvent.RecordKeys)));

            this.domainEventBus.SubscribeAsync<TransactionDataReady>(
                domainEvent =>
                {
                    return this.transactionService.Processing(
                        new ProcessingTransactionCommand
                        {
                            RecordKey = domainEvent.RecordKey
                        }).ContinueWith(
                            t => t.IsCompleted
                                ? Task.WhenAll(
                                    domainEvent.Endpoint.Select(
                                        ep =>
                                        {
                                            var dispatcher =
                                                ApplicationIntegration.DependencyResolver
                                                    .ResolveNamed<ICommunicationHandler>(
                                                        "communication-{0}".FormatWith(ep.Protocol));
                                            return dispatcher.Handle(
                                                domainEvent,
                                                ep.ConnectionConfiguration,
                                                ep.ProtocolConfiguration);
                                        }))
                                : t);
                });
        }

        /// <summary>
        /// Registers the persistence handlers for domain events.
        /// </summary>
        public void RegisterPersistenceHandlers()
        {
            this.domainEventBus.SubscribeAsync<SiteCreatedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<SiteCreatedEventHandler>().HandleAsync(domainEvent));
            this.domainEventBus.SubscribeAsync<SiteAddressUpdated>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<SiteAddressUpdatedEventHandler>().HandleAsync(domainEvent));
            this.domainEventBus.SubscribeAsync<SiteBillingAccountUpdated>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<SiteBillingAccountUpdatedEventHandler>().HandleAsync(domainEvent));
        }
    }
}
