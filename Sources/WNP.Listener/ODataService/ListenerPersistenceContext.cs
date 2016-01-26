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
        private readonly ListenerODataContext _ctx;
        private readonly Dictionary<Type, object> _repositoryRegistry = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerPersistenceContext"/> class.
        /// </summary>
        /// <param name="ctx">The CTX.</param>
        public ListenerPersistenceContext(ListenerODataContext ctx)
        {
            this._ctx = ctx;
            this._repositoryRegistry.Add(
                typeof(TransactionRegistryEntity),
                new DbRepository<TransactionRegistryEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(TransactionMessageDatumEntity),
                new DbRepository<TransactionMessageDatumEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(TransactionRegistryViewEntity),
                new DbRepository<TransactionRegistryViewEntity>(this._ctx));
            this._repositoryRegistry.Add(typeof(EndpointEntity), new DbRepository<EndpointEntity>(this._ctx));
            this._repositoryRegistry.Add(typeof(ProtocolTypeEntity), new DbRepository<ProtocolTypeEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(EndpointTriggerTypeEntity),
                new DbRepository<EndpointTriggerTypeEntity>(this._ctx));
            this._repositoryRegistry.Add(typeof(ValueMapEntity), new DbRepository<ValueMapEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(ValueMapEntryEntity),
                new DbRepository<ValueMapEntryEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(FieldConfigurationEntity),
                new DbRepository<FieldConfigurationEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(FieldConfigurationEntryEntity),
                new DbRepository<FieldConfigurationEntryEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(EntityCategoryOperationEntity),
                new DbRepository<EntityCategoryOperationEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(EnabledOperationEntity),
                new DbRepository<EnabledOperationEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(EntityCategoryEntity),
                new DbRepository<EntityCategoryEntity>(this._ctx));
            this._repositoryRegistry.Add(typeof(OperationEntity), new DbRepository<OperationEntity>(this._ctx));
            this._repositoryRegistry.Add(
                typeof(OperationEndpointEntity),
                new DbRepository<OperationEndpointEntity>(this._ctx));
        }

        public IDbRepository<TransactionRegistryEntity> TransactionRegistry
            => this.GetRepository<TransactionRegistryEntity>();

        public IDbRepository<TransactionMessageDatumEntity> TransactionMessageData
            => this.GetRepository<TransactionMessageDatumEntity>();

        public IDbRepository<TransactionRegistryViewEntity> TransactionRegistryDetails
            => this.GetRepository<TransactionRegistryViewEntity>();

        public IDbRepository<EndpointEntity> Endpoint => this.GetRepository<EndpointEntity>();

        public IDbRepository<ProtocolTypeEntity> ProtocolType => this.GetRepository<ProtocolTypeEntity>();

        public IDbRepository<EndpointTriggerTypeEntity> EndpointTriggerType
            => this.GetRepository<EndpointTriggerTypeEntity>();

        public IDbRepository<ValueMapEntity> ValueMap => this.GetRepository<ValueMapEntity>();

        public IDbRepository<ValueMapEntryEntity> ValueMapEntry => this.GetRepository<ValueMapEntryEntity>();

        public IDbRepository<FieldConfigurationEntity> FieldConfiguration
            => this.GetRepository<FieldConfigurationEntity>();

        public IDbRepository<FieldConfigurationEntryEntity> FieldConfigurationEntry
            => this.GetRepository<FieldConfigurationEntryEntity>();

        public IDbRepository<EntityCategoryOperationEntity> EntityCategoryOperation
            => this.GetRepository<EntityCategoryOperationEntity>();

        public IDbRepository<EnabledOperationEntity> EnabledOperation => this.GetRepository<EnabledOperationEntity>()
            ;

        public IDbRepository<EntityCategoryEntity> EntityCategory => this.GetRepository<EntityCategoryEntity>();

        public IDbRepository<OperationEntity> Operation => this.GetRepository<OperationEntity>();

        public IDbRepository<OperationEndpointEntity> OperationEndpoint
            => this.GetRepository<OperationEndpointEntity>();

        private IDbRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (this._repositoryRegistry.ContainsKey(typeof(TEntity)))
            {
                return (IDbRepository<TEntity>)this._repositoryRegistry[typeof(TEntity)];
            }

            return null;
        }
    }
}