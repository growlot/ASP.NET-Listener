// <copyright file="ApplicationServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using Commands;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;

    /// <summary>
    /// Configures application services by registering command handlers
    /// </summary>
    public class ApplicationServiceConfigurator
    {
        private IDomainEventBus domainEventBus;
        private ITransactionService transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationServiceConfigurator" /> class.
        /// </summary>
        /// <param name="transactionService">The transaction service.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public ApplicationServiceConfigurator(ITransactionService transactionService, IDomainEventBus domainEventBus)
        {
            this.transactionService = transactionService;
            this.domainEventBus = domainEventBus;
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        public void Configure()
        {
            this.domainEventBus.SubscribeAsync<TransactionSkipped>(
                msg => this.transactionService.Skipped(new SkipTransactionCommand { RecordKey = msg.RecordKey }));
        }
    }
}
