﻿// //-----------------------------------------------------------------------
// // <copyright file="ApplicationEventManager.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AMSLLC.Core;
    using AMSLLC.Listener.Domain;

    public class ApplicationEventManager
    {
        private static readonly Lazy<ApplicationEventManager> instanceLazy = new Lazy<ApplicationEventManager>(() =>
        {
            var returnValue = new ApplicationEventManager();
            returnValue.Initialize();
            return returnValue;
        });

        private readonly Dictionary<Type, List<IDomainHandler>> _eventHandlers =
            new Dictionary<Type, List<IDomainHandler>>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationEventManager" /> class from being created.
        /// </summary>
        private ApplicationEventManager()
        {
        }

        public static ApplicationEventManager Instance => instanceLazy.Value;

        public void Ensure(params Type[] type)
        {
            //Doing nothing right now. Lazy loader of the instance would perform handlers load
            //Type parameter at this point just helping to ensure that certain assemblies are loaded
        }

        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <returns>System.Threading.Tasks.Task.</returns>
        public Task Handle(object eventData)
        {
            FailFast.EnsureNotNull(eventData, nameof(eventData));
            var dataType = eventData.GetType();
            if (this._eventHandlers.ContainsKey(dataType))
            {
                var tasks = _eventHandlers[dataType].Select(eventHandler => eventHandler.Handle(eventData));
                return Task.WhenAll(tasks);
            }
            return Task.CompletedTask;
        }

        private void Initialize()
        {
            var types =
                TypesImplementingInterface(typeof(IDomainHandler))
                    .Where(t => t.GetCustomAttributes<DomainEventHandlerAttribute>().Any());
            foreach (var type in types)
            {
                var eventType = type.GetCustomAttributes<DomainEventHandlerAttribute>();
                foreach (var domainEventHandlerAttribute in eventType)
                {
                    RegisterEvent(domainEventHandlerAttribute.EventType, (IDomainHandler)Activator.CreateInstance(type));
                }
            }
        }

        public void RegisterEvent(Type eventType, IDomainHandler handler)
        {
            if (!this._eventHandlers.ContainsKey(eventType))
            {
                this._eventHandlers.Add(eventType, new List<IDomainHandler>());
            }
            this._eventHandlers[eventType].Add(handler);
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