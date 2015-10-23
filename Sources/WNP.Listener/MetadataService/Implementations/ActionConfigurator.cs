// <copyright file="ActionConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using ODataService.Actions;

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

            var boundActionContainers =
                actionContainers.Where(type => typeof(IBoundActionsContainer).IsAssignableFrom(type));

            var unboundActionContainers =
                actionContainers.Where(type => typeof(IUnboundActionsContainer).IsAssignableFrom(type));

            var compositionRoot = ApplicationIntegration.DependencyResolver;

            boundActionContainers.Map(
                type => this.boundActionContainers.Add(((IBoundActionsContainer)compositionRoot.ResolveType(type)).GetTableName(), type));

            unboundActionContainers.Map(type => this.unboundActionContainers.Add(type.Name, type));
        }

        /// <inheritdoc/>
        public bool IsEntityActionsContainerAvailable(string tableName) =>
            this.boundActionContainers.ContainsKey(tableName);

        /// <inheritdoc/>
        public Type GetUnboundActionContainer(string containerName)
        {
            if (this.unboundActionContainers.ContainsKey(containerName))
            {
                return this.unboundActionContainers[containerName];
            }

            throw new ArgumentException("Invalid container name");
        }

        /// <inheritdoc/>
        public Type GetEntityActionContainer(string tableName)
        {
            if (this.boundActionContainers.ContainsKey(tableName))
            {
                return this.boundActionContainers[tableName];
            }

            throw new ArgumentException("Invalid table name");
        }
    }
}