//-----------------------------------------------------------------------
// <copyright file="EventsRegister.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Core;

    /// <summary>
    /// Implements observer design pattern for domain events.
    /// Allows to register handlers for specified events, so that handlers would be notified when event occurs and could process it.
    /// </summary>
    public static class EventsRegister
    {
        /// <summary>
        /// The event handlers collection.
        /// </summary>
        private static readonly Dictionary<Type, List<Action<IEvent>>> Handlers = new Dictionary<Type, List<Action<IEvent>>>();
        private static readonly Dictionary<Type, List<Func<IEvent, Task>>> AsyncHandlers = new Dictionary<Type, List<Func<IEvent, Task>>>();

        /// <summary>
        /// Registers the specified event handler.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="handler">The handler.</param>
        public static void Register<T>(Action<T> handler) where T : IEvent
        {
            if (!Handlers.ContainsKey(typeof(T)))
            {
                Handlers.Add(typeof(T), new List<Action<IEvent>>());
            }

            Handlers[typeof(T)].Add(x => handler((T)x));
        }

        /// <summary>
        /// Registers the specified event handler.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <param name="handler">The handler.</param>
        public static void RegisterAsync<T>(Func<T, Task> handler) where T : IEvent
        {
            if (!AsyncHandlers.ContainsKey(typeof(T)))
            {
                AsyncHandlers.Add(typeof(T), new List<Func<IEvent, Task>>());
            }

            AsyncHandlers[typeof(T)].Add(x => handler((T)x));
        }

        /// <summary>
        /// Raises the event.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <exception cref="System.ArgumentNullException">eventData; Event data must be specified in order to raise the event.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This implementation is similar but much simplier than MS Observer pattern.")]
        public static void Raise(IEvent eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException("eventData", "Event must be specified in order to raise it.");
            }

            if (Handlers.ContainsKey(eventData.GetType()))
            {
                foreach (var eventHandler in Handlers[eventData.GetType()])
                {
                    eventHandler(eventData);
                }
            }
        }

        /// <summary>
        /// Raises the events as set of tasks.
        /// </summary>
        /// <param name="events">The events.</param>
        /// <returns>Task[].</returns>
        public static Task[] AsParallel(ReadOnlyCollection<IEvent> events)
        {
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }

            Task[] tasks = new Task[events.Count];

            for (int i = 0; i < events.Count; i++)
            {
                var evt = events[i];
                if (!AsyncHandlers.ContainsKey(evt.GetType()))
                {
                    continue;
                }

                foreach (var eventHandler in AsyncHandlers[evt.GetType()])
                {
                    tasks[i] = eventHandler(evt);
                }
            }

            return tasks;
        }
    }
}
