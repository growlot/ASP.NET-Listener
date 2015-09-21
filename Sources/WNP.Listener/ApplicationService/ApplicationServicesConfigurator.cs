// //-----------------------------------------------------------------------
// // <copyright file="ApplicationServicesConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent;

    public static class ApplicationServicesConfigurator
    {
        public static void Configure()
        {
            EventsRegister.Register<JmsDataReady>(data => ApplicationEventManager.Instance.Handle(data));
        }
    }
}