using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using AMSLLC.Core;
using AMSLLC.Listener.ODataService.Actions;
using AMSLLC.Listener.ODataService.Actions.Attributes;

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

            var boundActionContainers = actionContainers
                .Where(type => !type.IsAbstract)
                .Where(type => typeof (IBoundActionsContainer).IsAssignableFrom(type));

            var unboundActionContainers = actionContainers
                .Where(type => !type.IsAbstract)
                .Where(type => typeof (IUnboundActionsContainer).IsAssignableFrom(type));
            
            boundActionContainers.Map(
                type => this._boundActionContainers.Add(((IBoundActionsContainer)FormatterServices.GetUninitializedObject(type)).GetEntityTableName(), type));

            unboundActionContainers.Map(type =>
            {
                var containerName = type.Name;
                var actionPrefixAttribute =
                    type.GetCustomAttributes(typeof (ActionPrefixAttribute)).FirstOrDefault() as ActionPrefixAttribute;

                if (actionPrefixAttribute != null)
                    containerName = actionPrefixAttribute.Prefix;

                this._unboundActionContainers.Add(containerName, type);
            });
        }

        public bool IsEntityActionsContainerAvailable(string tableName) =>
            this._boundActionContainers.ContainsKey(tableName);

        public Type GetUnboundActionContainer(string containerName)
        {
            if (this._unboundActionContainers.ContainsKey(containerName))
                return this._unboundActionContainers[containerName];

            throw new ArgumentException("Invalid container name");
        }

        public Type GetEntityActionContainer(string tableName)
        {
            if (this._boundActionContainers.ContainsKey(tableName))
                return this._boundActionContainers[tableName];

            throw new ArgumentException("Invalid table name");
        }
    }
}