// //-----------------------------------------------------------------------
// <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Communication;
    using Core;

    /// <summary>
    /// Transaction execution
    /// </summary>
    public class TransactionExecution : Entity<int>, IWithDomainBuilder, IAggregateRoot
    {
        private readonly DomainValidatorDictionary validatorRegistry = new DomainValidatorDictionary();

        /// <summary>
        /// The domain event bus
        /// </summary>
        private IDomainEventBus domainEventBus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionExecution" /> class.
        /// </summary>
        /// <param name="hashValidator">The hash validator.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public TransactionExecution(IUniqueHashValidator hashValidator, IDomainEventBus domainEventBus)
        {
            this.validatorRegistry.Add(typeof(IUniqueHashValidator), hashValidator);
            this.domainEventBus = domainEventBus;
        }

        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string RecordKey { get; private set; }

        /// <summary>
        /// Gets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EnabledOperationId { get; private set; }

        /// <summary>
        /// Gets or sets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        public string TransactionHash { get; set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public ReadOnlyCollection<IntegrationEndpointConfiguration> EndpointConfigurations { get; private set; } =
            new ReadOnlyCollection<IntegrationEndpointConfiguration>(new IntegrationEndpointConfiguration[0]);

        /// <summary>
        /// Gets the field configurations.
        /// </summary>
        /// <value>The field configurations.</value>
        public ReadOnlyCollection<FieldConfiguration> FieldConfigurations { get; private set; } =
                    new ReadOnlyCollection<FieldConfiguration>(new FieldConfiguration[0]);

        /// <summary>
        /// Gets or sets the domain builder.
        /// </summary>
        /// <value>The domain builder.</value>
        public virtual IDomainBuilder DomainBuilder { get; set; }

        /// <summary>
        /// Gets the hash validator.
        /// </summary>
        /// <value>The hash validator.</value>
        protected IUniqueHashValidator HashValidator => this.validatorRegistry[typeof(IUniqueHashValidator)] as IUniqueHashValidator;

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Task.</returns>
        public virtual Task[] Process(object data)
        {
            var returnValue = new Task[this.EndpointConfigurations.Count];
            var processor =
                ApplicationIntegration.DependencyResolver.ResolveType<IEndpointDataProcessor>();

            var preparedData = processor.Process(data, this.FieldConfigurations);
            for (int i = 0; i < this.EndpointConfigurations.Count; i++)
            {
                var cfg = this.EndpointConfigurations[i];
                this.TransactionHash = preparedData.Hash;
                returnValue[i] = this.HashValidator.ValidateAsync(this.EnabledOperationId, preparedData.Hash).ContinueWith(t =>
                {
                    if (!this.HashValidator.Valid && cfg.Trigger == EndpointTriggerType.Changed)
                    {
                        var tasks = this.domainEventBus.PublishAsync(new TransactionSkipped(this.RecordKey));
                        if (tasks.Any())
                        {
                            return Task.Factory.ContinueWhenAll(tasks, (tt) => tt);
                        }
                        return Task.Factory.StartNew(() => { });
                    }

                    var dispatcher =
                    ApplicationIntegration.DependencyResolver.ResolveNamed<ICommunicationHandler>(
                        "communication-{0}".FormatWith(cfg.Protocol));

                    var eventData = new TransactionDataReady
                    {
                        Data = preparedData.Data,
                        RecordKey = this.RecordKey
                    };
                    this.domainEventBus.Publish(eventData);

                    return dispatcher.Handle(eventData, cfg.ConnectionConfiguration);
                });
            }

            return returnValue;
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionExecutionMemento)memento;
            this.RecordKey = myMemento.RecordKey;
            this.Id = myMemento.TransactionId;
            this.EnabledOperationId = myMemento.EnabledOperationId;
            this.EndpointConfigurations =
                new ReadOnlyCollection<IntegrationEndpointConfiguration>(myMemento.EndpointConfigurations.Select(
                    cfgMemento => this.DomainBuilder.Create<IntegrationEndpointConfiguration>(cfgMemento)).ToList());

            this.FieldConfigurations =
                new ReadOnlyCollection<FieldConfiguration>(new List<FieldConfiguration>(myMemento.FieldConfigurations.Select(
                    s =>
                    {
                        var itm = new FieldConfiguration();
                        ((IOriginator)itm).SetMemento(s);
                        return itm;
                    })));
        }
    }
}