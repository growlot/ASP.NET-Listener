// //-----------------------------------------------------------------------
// // <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Communication;
    using Core;

    /// <summary>
    /// Transaction execution
    /// </summary>
    public class TransactionExecution : Entity<int>, IWithDomainBuilder, IAggregateRoot, IOriginator
    {
        /// <summary>
        /// Gets the transaction identifier.
        /// </summary>
        /// <value>The transaction identifier.</value>
        public string TransactionId { get; private set; }

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
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected void SetMemento(IMemento memento)
        {
            var myMemento = (TransactionExecutionMemento)memento;
            this.TransactionId = myMemento.TransactionId;
            this.EndpointConfigurations =
                new ReadOnlyCollection<IntegrationEndpointConfiguration>(myMemento.EndpointConfigurations.Select(
                    cfgMemento => this.DomainBuilder.Create<IntegrationEndpointConfiguration>(cfgMemento)).ToList());
        }

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
            for (int i = 0; i < this.EndpointConfigurations.Count; i++)
            {
                var cfg = this.EndpointConfigurations[i];
                var preparedData = processor.Process(data, cfg);
                var dispatcher =
                    ApplicationIntegration.DependencyResolver.ResolveNamed<ICommunicationHandler>(
                        "communication-{0}".FormatWith(cfg.Protocol));

                var eventData = new TransactionDataReady
                {
                    Data = preparedData.Data
                };
                EventsRegister.Raise(eventData);
                returnValue[i] = dispatcher.Handle(eventData, cfg.ConnectionConfiguration);
            }

            return returnValue;
        }

        /// <summary>
        /// Open the transaction.
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task<string> Open()
        {
            return Task.Factory.StartNew(() => string.Empty);
        }

        /// <summary>
        /// Mark transaction as successful
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task Success()
        {
            return Task.Factory.StartNew(() => { });
        }

        /// <summary>
        /// Mark transaction as failed
        /// </summary>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public virtual Task Failed()
        {
            return Task.Factory.StartNew(() => { });
        }
    }
}