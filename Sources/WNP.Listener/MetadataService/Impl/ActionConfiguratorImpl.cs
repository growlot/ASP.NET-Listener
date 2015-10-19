using System;
using System.Collections.Generic;
using System.Linq;
using AMSLLC.Core;
using AMSLLC.Listener.ODataService.Actions;

namespace AMSLLC.Listener.MetadataService.Impl
{
    public class ActionConfiguratorImpl : IActionConfigurator
    {
        private readonly Dictionary<string, Type> _boundActionContainers = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> _unboundActionContainers = new Dictionary<string, Type>();

        public ActionConfiguratorImpl()
        {
            var actionContainers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface)
                    .ToList();

            var boundActionContainers =
                actionContainers.Where(type => typeof (IBoundActionsContainer).IsAssignableFrom(type));

            var unboundActionContainers =
                actionContainers.Where(type => typeof (IUnboundActionsContainer).IsAssignableFrom(type));

            var compositionRoot = ApplicationIntegration.DependencyResolver;

            boundActionContainers.Map(
                type => _boundActionContainers.Add(((IBoundActionsContainer) compositionRoot.ResolveType(type)).GetTableName(), type));

            unboundActionContainers.Map(type => _unboundActionContainers.Add(type.Name, type));
        }

        public bool IsEntityActionsContainerAvailable(string tableName) =>
            _boundActionContainers.ContainsKey(tableName);

        public Type GetUnboundActionContainer(string containerName)
        {
            if (_unboundActionContainers.ContainsKey(containerName))
                return _unboundActionContainers[containerName];

            throw new ArgumentException("Invalid container name");
        }

        public Type GetEntityActionContainer(string tableName)
        {
            if (_boundActionContainers.ContainsKey(tableName))
                return _boundActionContainers[tableName];
            
            throw new ArgumentException("Invalid table name");
        }
    }
}