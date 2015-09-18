// //-----------------------------------------------------------------------
// // <copyright file="ApplicationEventManager.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Utilities;

    public class ApplicationEventManager
    {
        private static readonly Lazy<ApplicationEventManager> instanceLazy = new Lazy<ApplicationEventManager>(() =>
        {
            var returnValue = new ApplicationEventManager();
            returnValue.Initialize();
            return returnValue;
        });

        private readonly Dictionary<Type, List<IEventHandler>> _eventHandlers =
            new Dictionary<Type, List<IEventHandler>>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationEventManager" /> class from being created.
        /// </summary>
        private ApplicationEventManager()
        {
        }

        public static ApplicationEventManager Instance => instanceLazy.Value;

        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public Task Handle(object eventData)
        {
            FailFast.EnsureNotNull(eventData, nameof(eventData));
            var dataType = eventData.GetType();
            if (_eventHandlers.ContainsKey(dataType))
            {
                var tasks = _eventHandlers[dataType].Select(eventHandler => eventHandler.Handle(eventData));
                return Task.WhenAll(tasks);
            }
            return Task.CompletedTask;
        }

        private void Initialize()
        {
            var types =
                TypesImplementingInterface(typeof (IEventHandler))
                    .Where(t => t.GetCustomAttributes<DomainEventHandlerAttribute>().Any());
            foreach (var type in types)
            {
                var eventType = type.GetCustomAttributes<DomainEventHandlerAttribute>();
                foreach (var domainEventHandlerAttribute in eventType)
                {
                    if (!_eventHandlers.ContainsKey(domainEventHandlerAttribute.Type))
                    {
                        _eventHandlers.Add(domainEventHandlerAttribute.Type, new List<IEventHandler>());
                    }
                    _eventHandlers[domainEventHandlerAttribute.Type].Add((IEventHandler) Activator.CreateInstance(type));
                }
            }
        }

        private static bool DoesTypeSupportInterface(Type type, Type inter)
        {
            if (inter.IsAssignableFrom(type))
                return true;
            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == inter))
                return true;
            return false;
        }

        private static IEnumerable<Type> TypesImplementingInterface(Type desiredType)
        {
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => DoesTypeSupportInterface(type, desiredType));
        }
    }
}