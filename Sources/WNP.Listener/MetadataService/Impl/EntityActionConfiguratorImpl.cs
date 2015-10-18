using System;
using System.Collections.Generic;
using System.Linq;
using AMSLLC.Core;
using AMSLLC.Listener.ODataService.Actions;

namespace AMSLLC.Listener.MetadataService.Impl
{
    public class EntityActionConfiguratorImpl : IEntityActionConfigurator
    {
        private readonly Dictionary<string, Type> _actionContainers = new Dictionary<string, Type>();

        public EntityActionConfiguratorImpl()
        {
            var actionContainers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface)
                    .Where(type => typeof (IActionsContainer).IsAssignableFrom(type));

            var compositionRoot = ApplicationIntegration.DependencyResolver;
            actionContainers.Map(
                type => _actionContainers.Add(((IActionsContainer) compositionRoot.ResolveType(type)).GetTableName(), type));
        }

        public bool IsEntityActionsContainerAvailable(string tableName) =>
            _actionContainers.ContainsKey(tableName);

        public Type GetEntityActionContainer(string tableName)
        {
            if (_actionContainers.ContainsKey(tableName))
                return _actionContainers[tableName];
            
            throw new ArgumentException("Invalid table name");
        }
    }
}