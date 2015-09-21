// //-----------------------------------------------------------------------
// // <copyright file="TransactionExecution.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Core;

    /// <summary>
    /// Transaction execution
    /// </summary>
    public class TransactionExecution : Entity<int>, IAggregateRoot, IOriginator
    {
        /// <summary>
        /// Gets the source application identifier.
        /// </summary>
        /// <value>The source application identifier.</value>
        public string SourceApplicationId { get; private set; }

        /// <summary>
        /// Gets the destination application identifier.
        /// </summary>
        /// <value>The destination application identifier.</value>
        public string DestinationApplicationId { get; private set; }

        /// <summary>
        /// Gets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; private set; }

        /// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public ReadOnlyCollection<IIntegrationEndpointConfiguration> EndpointConfigurations { get; private set; } =
            new ReadOnlyCollection<IIntegrationEndpointConfiguration>(new IIntegrationEndpointConfiguration[0]);

        /*/// <summary>
        /// Gets the endpoint configurations.
        /// </summary>
        /// <value>The endpoint configurations.</value>
        public List<ListenerEndpointConfiguration> EndpointConfigurations { get; } = new List<ListenerEndpointConfiguration>();*/

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
            this.SourceApplicationId = myMemento.SourceApplicationId;
            this.DestinationApplicationId = myMemento.DestinationApplicationId;
            this.OperationKey = myMemento.DestinationOperationKey;
            this.EndpointConfigurations =
                new ReadOnlyCollection<IIntegrationEndpointConfiguration>(myMemento.EndpointConfigurations);
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>Task.</returns>
        public virtual Task[] Process(string transactionId)
        {
            List<IEvent> events = new List<IEvent>();
            for (int i = 0; i < this.EndpointConfigurations.Count; i++)
            {
                var cfg = this.EndpointConfigurations[i];
                var processor =
                    ApplicationIntegration.DependencyResolver.ResolveNamed<IEndpointProcessor>(
                        "endpoint-{0}".FormatWith(cfg.Name));
                var data = processor.Process(this.SourceApplicationId, this.DestinationApplicationId, this.OperationKey,
                    transactionId, cfg);
                events.Add(data);
            }

            return EventsRegister.AsParallel(new ReadOnlyCollection<IEvent>(events));
        }
    }
}