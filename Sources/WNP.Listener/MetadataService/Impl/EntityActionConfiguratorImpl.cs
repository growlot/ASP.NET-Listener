using System;
using System.Collections.Generic;
using AMSLLC.Listener.Persistence.Metadata;
using ODataService.Actions;

namespace AMSLLC.Listener.MetadataService.Impl
{
    public class EntityActionConfiguratorImpl : IEntityActionConfigurator
    {
        private readonly Dictionary<string, Type> _mappings;

        public EntityActionConfiguratorImpl()
        {
            _mappings = new Dictionary<string, Type>()
            {
                {DBMetadata.EqpMeter.RealTableName, typeof(ElectricMeterActionContainer) }
            };
        }

        public bool IsEntityActionsContainerAvailable(string tableName) =>
            _mappings.ContainsKey(tableName);

        public Type GetEntityActionContainer(string tableName)
        {
            if (_mappings.ContainsKey(tableName))
                return _mappings[tableName];
            
            throw new ArgumentException("Invalid table name");
        }
    }
}