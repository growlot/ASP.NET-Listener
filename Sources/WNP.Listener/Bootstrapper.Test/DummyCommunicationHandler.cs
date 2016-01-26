// <copyright file="DummyCommunicationHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Threading.Tasks;
    using Domain.Listener;

    public class DummyCommunicationHandler : ICommunicationHandler
    {
        public Task Handle(
            object requestData,
            IConnectionConfiguration connectionConfiguration,
            IProtocolConfiguration protocolConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}