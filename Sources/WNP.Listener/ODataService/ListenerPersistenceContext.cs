// <copyright file="ListenerPersistenceContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Collections.Generic;
    using Persistence.Listener;

    /// <summary>
    /// Listener persistence context
    /// </summary>
    public class ListenerPersistenceContext : IListenerPersistenceContext
    {
        private readonly ListenerODataContext ctx;
        private readonly Dictionary<Type, object> repositoryRegistry = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerPersistenceContext"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public ListenerPersistenceContext(ListenerODataContext ctx)
        {
            this.ctx = ctx;
            this.repositoryRegistry.Add(
                typeof(TransactionRegistryEntity),
                new DbRepository<TransactionRegistryEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(TransactionMessageDatumEntity),
                new DbRepository<TransactionMessageDatumEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(TransactionRegistryViewEntity),
                new DbRepository<TransactionRegistryViewEntity>(this.ctx));
            this.repositoryRegistry.Add(typeof(EndpointEntity), new DbRepository<EndpointEntity>(this.ctx));
            this.repositoryRegistry.Add(typeof(ProtocolTypeEntity), new DbRepository<ProtocolTypeEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(EndpointTriggerTypeEntity),
                new DbRepository<EndpointTriggerTypeEntity>(this.ctx));
            this.repositoryRegistry.Add(typeof(ValueMapEntity), new DbRepository<ValueMapEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(ValueMapEntryEntity),
                new DbRepository<ValueMapEntryEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(FieldConfigurationEntity),
                new DbRepository<FieldConfigurationEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(FieldConfigurationEntryEntity),
                new DbRepository<FieldConfigurationEntryEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(EntityCategoryOperationEntity),
                new DbRepository<EntityCategoryOperationEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(EnabledOperationEntity),
                new DbRepository<EnabledOperationEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(EntityCategoryEntity),
                new DbRepository<EntityCategoryEntity>(this.ctx));
            this.repositoryRegistry.Add(typeof(OperationEntity), new DbRepository<OperationEntity>(this.ctx));
            this.repositoryRegistry.Add(
                typeof(OperationEndpointEntity),
                new DbRepository<OperationEndpointEntity>(this.ctx));
        }

        /// <summary>
        /// Gets the transaction registry repository.
        /// </summary>
        /// <value>The transaction registry.</value>
        public IDbRepository<TransactionRegistryEntity> TransactionRegistry
                    => this.GetRepository<TransactionRegistryEntity>();

        /// <summary>
        /// Gets the transaction message data repository.
        /// </summary>
        /// <value>The transaction message data.</value>
        public IDbRepository<TransactionMessageDatumEntity> TransactionMessageData
                    => this.GetRepository<TransactionMessageDatumEntity>();

        /// <summary>
        /// Gets the transaction registry details repository.
        /// </summary>
        /// <value>The transaction registry details.</value>
        public IDbRepository<TransactionRegistryViewEntity> TransactionRegistryDetails
                    => this.GetRepository<TransactionRegistryViewEntity>();

        /// <summary>
        /// Gets the endpoint repository.
        /// </summary>
        /// <value>The endpoint.</value>
        public IDbRepository<EndpointEntity> Endpoint => this.GetRepository<EndpointEntity>();

        /// <summary>
        /// Gets the type of the protocol repository.
        /// </summary>
        /// <value>The type of the protocol.</value>
        public IDbRepository<ProtocolTypeEntity> ProtocolType => this.GetRepository<ProtocolTypeEntity>();

        /// <summary>
        /// Gets the type of the endpoint trigger repository.
        /// </summary>
        /// <value>The type of the endpoint trigger.</value>
        public IDbRepository<EndpointTriggerTypeEntity> EndpointTriggerType
                    => this.GetRepository<EndpointTriggerTypeEntity>();

        /// <summary>
        /// Gets the value map repository.
        /// </summary>
        /// <value>The value map.</value>
        public IDbRepository<ValueMapEntity> ValueMap => this.GetRepository<ValueMapEntity>();

        /// <summary>
        /// Gets the value map entry repository.
        /// </summary>
        /// <value>The value map entry.</value>
        public IDbRepository<ValueMapEntryEntity> ValueMapEntry => this.GetRepository<ValueMapEntryEntity>();

        /// <summary>
        /// Gets the field configuration repository.
        /// </summary>
        /// <value>The field configuration.</value>
        public IDbRepository<FieldConfigurationEntity> FieldConfiguration
                    => this.GetRepository<FieldConfigurationEntity>();

        /// <summary>
        /// Gets the field configuration entry repository.
        /// </summary>
        /// <value>The field configuration entry.</value>
        public IDbRepository<FieldConfigurationEntryEntity> FieldConfigurationEntry
                    => this.GetRepository<FieldConfigurationEntryEntity>();

        /// <summary>
        /// Gets the entity category operation repository.
        /// </summary>
        /// <value>The entity category operation.</value>
        public IDbRepository<EntityCategoryOperationEntity> EntityCategoryOperation
                    => this.GetRepository<EntityCategoryOperationEntity>();

        /// <summary>
        /// Gets the enabled operation repository.
        /// </summary>
        /// <value>The enabled operation.</value>
        public IDbRepository<EnabledOperationEntity> EnabledOperation => this.GetRepository<EnabledOperationEntity>()
                    ;

        /// <summary>
        /// Gets the entity category repository.
        /// </summary>
        /// <value>The entity category.</value>
        public IDbRepository<EntityCategoryEntity> EntityCategory => this.GetRepository<EntityCategoryEntity>();

        /// <summary>
        /// Gets the operation repository.
        /// </summary>
        /// <value>The operation.</value>
        public IDbRepository<OperationEntity> Operation => this.GetRepository<OperationEntity>();

        /// <summary>
        /// Gets the operation endpoint repository.
        /// </summary>
        /// <value>The operation endpoint.</value>
        public IDbRepository<OperationEndpointEntity> OperationEndpoint
                    => this.GetRepository<OperationEndpointEntity>();

        private IDbRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this.repositoryRegistry.ContainsKey(typeof(TEntity)))
            {
                return (IDbRepository<TEntity>)this.repositoryRegistry[typeof(TEntity)];
            }

            return null;
        }
    }
}