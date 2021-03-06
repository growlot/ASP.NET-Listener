﻿// <copyright file="ApplicationServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ApplicationService;
    using ApplicationService.CommandHandlers;
    using ApplicationService.Commands;
    using Core;
    using Domain;
    using Domain.Listener;
    using Domain.Listener.Transaction;
    using Domain.WNP.ElectricMeterAggregate;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Domain.WNP.SiteAggregate.CircuitChild.Equipment;
    using Domain.WNP.WorkstationAggregate;
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
            this.commandBus.Subscribe<UpdateSiteDetailsCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<UpdateSiteDetailsCommandHandler>().HandleAsync(command));

            this.commandBus.Subscribe<CreateCircuitCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<CreateCircuitCommandHandler>().HandleAsync(command));
            this.commandBus.Subscribe<UpdateCircuitDetailsCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<UpdateCircuitDetailsCommandHandler>().HandleAsync(command));

            this.commandBus.Subscribe<InstallMeterCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<InstallMeterCommandHandler>().HandleAsync(command));
            this.commandBus.Subscribe<UninstallMeterCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<UninstallMeterCommandHandler>().HandleAsync(command));

            this.commandBus.Subscribe<ExecuteBusinessRuleCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<ExecuteBusinessRuleCommandHandler>().HandleAsync(command));

            this.commandBus.Subscribe<AddElectricMeterReadingCommand>(
                command => ApplicationIntegration.DependencyResolver.ResolveType<AddElectricMeterReadingCommandHandler>().HandleAsync(command));
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
                async domainEvent =>
                {
                    try
                    {
                        await this.transactionService.Processing(
                            new ProcessingTransactionCommand
                            {
                                RecordKey = domainEvent.RecordKey
                            });

                        await Task.WhenAll(
                            domainEvent.Endpoint.Select(
                                ep =>
                                {
                                    var dispatcher =
                                        ApplicationIntegration.DependencyResolver.ResolveNamed<ICommunicationHandler>(
                                            "communication-{0}".FormatWith(ep.Protocol));
                                    return dispatcher.Handle(
                                        domainEvent,
                                        ep.ConnectionConfiguration,
                                        ep.ProtocolConfiguration);
                                }));
                        if (domainEvent.AutoSucceed)
                        {
                            await this.transactionService.Success(
                                new SucceedTransactionCommand
                                {
                                    RecordKey = domainEvent.RecordKey
                                });
                        }
                    }
                    catch (Exception exc)
                    {
                        await this.transactionService.Failed(
                            new FailTransactionCommand
                            {
                                RecordKey = domainEvent.RecordKey,
                                Message = exc.Message,
                                Details = exc.StackTrace
                            });
                        throw;
                    }
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
            this.domainEventBus.SubscribeAsync<SiteDetailsUpdated>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<SiteDetailsUpdatedEventHandler>().HandleAsync(domainEvent));

            this.domainEventBus.SubscribeAsync<CircuitCreatedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<CircuitCreatedEventHandler>().HandleAsync(domainEvent));
            this.domainEventBus.SubscribeAsync<CircuitDetailsUpdatedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<CircuitDetailsUpdatedEventHandler>().HandleAsync(domainEvent));

            this.domainEventBus.SubscribeAsync<EquipmentInstalledInCircuitEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<EquipmentInstalledInCircuitEventHandler>().HandleAsync(domainEvent));
            this.domainEventBus.SubscribeAsync<EquipmentUninstalledFromCircuitEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<EquipmentUninstalledFromCircuitEventHandler>().HandleAsync(domainEvent));
            this.domainEventBus.SubscribeAsync<MeterBillingInformationUpdatedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<MeterBillingInformationUpdatedEventHandler>().HandleAsync(domainEvent));

            this.domainEventBus.SubscribeAsync<EquipmentStateChangedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<EquipmentStateChangedEventHandler>().HandleAsync(domainEvent));

            this.domainEventBus.SubscribeAsync<ElectricMeterReadingAddedEvent>(
                domainEvent => ApplicationIntegration.DependencyResolver.ResolveType<ElectricMeterReadingAddedEventHandler>().HandleAsync(domainEvent));
        }
    }
}
