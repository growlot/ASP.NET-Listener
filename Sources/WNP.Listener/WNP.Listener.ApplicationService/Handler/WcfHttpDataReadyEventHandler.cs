// //-----------------------------------------------------------------------
// // <copyright file="WcfHttpDataReadyEventHandler.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService.Handler
{
    using System;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent;

    [DomainEventHandler(typeof (WcfHttpDataReady))]
    public class WcfHttpDataReadyEventHandler : IEventHandler
    {
        /// <summary>
        /// Handles the specified event.
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