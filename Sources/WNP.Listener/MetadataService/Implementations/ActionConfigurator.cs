// <copyright file="ActionConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Attributes;

    /// <summary>
    /// Implements <see cref="IActionConfigurator"/>
    /// </summary>
    public class ActionConfigurator : IActionConfigurator
    {
        private readonly Dictionary<string, Type> boundActionContainers = new Dictionary<string, Type>();
        private readonly Dictionary<string, Type> unboundActionContainers = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionConfigurator"/> class.
        /// </summary>
        public ActionConfigurator()
        {
            var actionContainers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface)
                    .ToList();

            var boundActionContainers = actionContainers
                .Where(type => !type.IsAbstract)
                .Where(type => typeof(IBoundActionsContainer).IsAssignableFrom(type));

            var unboundActionContainers = actionContainers
                .Where(type => !type.IsAbstract)
                .Where(type => typeof(IUnboundActionsContainer).IsAssignableFrom(type));

            boundActionContainers.Map(
                type => this.boundActionContainers.Add(((IBoundActionsContainer)FormatterServices.GetUninitializedObject(type)).GetEntityTableName(), type));

            unboundActionContainers.Map(type =>
            {
                var containerName = type.Name;
                var actionPrefixAttribute =
                    type.GetCustomAttributes(typeof(ActionPrefixAttribute)).FirstOrDefault() as ActionPrefixAttribute;

                if (actionPrefixAttribute != null)
                {
                    containerName = actionPrefixAttribute.Prefix;
                }

                this.unboundActionContainers.Add(containerName, type);
            });
        }

        /// <inheritdoc/>
        public Type GetUnboundActionContainer(string containerName)
        {
            if (this.unboundActionContainers.ContainsKey(containerName))
            {
                return this.unboundActionContainers[containerName];
            }

            return null;
        }

        /// <inheritdoc/>
        public Type GetEntityActionContainer(string tableName)
        {
            if (this.boundActionContainers.ContainsKey(tableName))
            {
                return this.boundActionContainers[tableName];
            }

            return null;
        }
    }
}