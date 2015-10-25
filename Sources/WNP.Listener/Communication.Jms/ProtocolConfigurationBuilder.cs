// <copyright file="ProtocolConfigurationBuilder.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication.Jms
{
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;

    /// <summary>
    /// JMS protocol configuration builder
    /// </summary>
    public class ProtocolConfigurationBuilder : IProtocolConfigurationBuilder
    {
        /// <summary>
        /// Creates the protocol configuration using information provided.
        /// </summary>
        /// <param name="myMemento">Data</param>
        /// <returns>IProtocolConfiguration.</returns>
        public IProtocolConfiguration Create(IMemento myMemento)
        {
            var cfgMemento = (IntegrationEndpointConfigurationMemento)myMemento;
            return cfgMemento.AdapterConfiguration == null ? null : JsonConvert.DeserializeObject<ProtocolConfiguration>(cfgMemento.AdapterConfiguration);
        }
    }
}