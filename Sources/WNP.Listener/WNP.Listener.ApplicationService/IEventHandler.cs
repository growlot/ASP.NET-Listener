// //-----------------------------------------------------------------------
// // <copyright file="IEventHandler.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService
{
    using System.Threading.Tasks;

    public interface IEventHandler
    {
        Task Handle(object eventData);
    }
}