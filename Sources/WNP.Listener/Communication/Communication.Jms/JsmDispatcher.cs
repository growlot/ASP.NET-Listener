// //-----------------------------------------------------------------------
// // <copyright file="JsmDispatcher.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms
{
    using System;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent;

    /// <summary>
    /// Jsm data dispatcher
    /// </summary>
    [DomainEventHandler(typeof(JmsDataReady))]
    public class JsmDispatcher : IDomainHandler
    {
        /// <summary>
        /// Handles the specified event data.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task Handle(object eventData)
        {
            throw new NotImplementedException();
        }
    }
}