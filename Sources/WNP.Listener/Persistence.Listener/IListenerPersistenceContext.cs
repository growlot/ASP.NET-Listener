namespace AMSLLC.Listener.Persistence.Listener
{
    public interface IListenerPersistenceContext
    {
        
        IDbRepository<TransactionRegistryEntity> TransactionRegistry { get; }

        
        IDbRepository<TransactionMessageDatumEntity> TransactionMessageData { get; }

        
        IDbRepository<TransactionRegistryViewEntity> TransactionRegistryDetails { get; }

        
        IDbRepository<EndpointEntity> Endpoint { get; }

        
        IDbRepository<ProtocolTypeEntity> ProtocolType { get; }

        
        IDbRepository<EndpointTriggerTypeEntity> EndpointTriggerType { get; }

        
        IDbRepository<ValueMapEntity> ValueMap { get; }

        
        IDbRepository<ValueMapEntryEntity> ValueMapEntry { get; }

        
        IDbRepository<FieldConfigurationEntity> FieldConfiguration { get; }

        
        IDbRepository<FieldConfigurationEntryEntity> FieldConfigurationEntry { get; }

        
        IDbRepository<EntityCategoryOperationEntity> EntityCategoryOperation { get; }

        
        IDbRepository<EnabledOperationEntity> EnabledOperation { get; }

        
        IDbRepository<EntityCategoryEntity> EntityCategory { get; }

        
        IDbRepository<OperationEntity> Operation { get; }

        
        IDbRepository<OperationEndpointEntity> OperationEndpoint { get; }
    }
}
