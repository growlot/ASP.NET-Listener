// //-----------------------------------------------------------------------
// // <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
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
        /// Initializes a new instance of the <see cref="TransactionExecution"/> class.
        /// </summary>
        /// <param name="hashValidator">The hash validator.</param>
        public TransactionExecution(IUniqueHashValidator hashValidator)
        {
            this.validatorRegistry.Add(typeof(IUniqueHashValidator), hashValidator);
        }

        /// <summary>
        /// Gets the hash validator.
        /// </summary>
        /// <value>The hash validator.</value>
        protected IUniqueHashValidator HashValidator => this.validatorRegistry[typeof(IUniqueHashValidator)] as IUniqueHashValidator;

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
        /// Gets or sets the domain builder.
        /// </summary>
        /// <value>The domain builder.</value>
        public virtual IDomainBuilder DomainBuilder { get; set; }

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
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="header">The header.</param>
        /// <returns>Task.</returns>
        public virtual Task[] Process(object data, Dictionary<string, object> header)
        {
            var returnValue = new Task[this.EndpointConfigurations.Count];
            var processor =
                ApplicationIntegration.DependencyResolver.ResolveType<IEndpointDataProcessor>();

            for (int i = 0; i < this.EndpointConfigurations.Count; i++)
            {
                var cfg = this.EndpointConfigurations[i];
                var preparedData = processor.Process(data, cfg);
                this.TransactionHash = preparedData.Hash;
                returnValue[i] = this.HashValidator.ValidateAsync(this.EnabledOperationId, preparedData.Hash).ContinueWith(t =>
                {
                    if (!this.HashValidator.Valid && cfg.Trigger == EndpointTriggerType.Changed)
                    {
                        var tasks = EventsRegister.RaiseAsync(new TransactionSkipped(this.RecordKey));
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
                        Data = new TransactionMessage { Data = preparedData.Data, Header = header, RecordKey = this.RecordKey },
                    };
                    EventsRegister.Raise(eventData);

                    return dispatcher.Handle(eventData, cfg.ConnectionConfiguration);
                });
            }

            return returnValue;
        }
    }
}