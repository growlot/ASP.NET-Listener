// //-----------------------------------------------------------------------
// <copyright file="JmsConnectionConfigurationBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms
{
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;

    /// <summary>
    /// Implements <see cref="IConnectionConfigurationBuilder"/> for JMS
    /// </summary>
    public class JmsConnectionConfigurationBuilder : IConnectionConfigurationBuilder
    {
        /// <summary>
        /// Create JMS connection configuration using provided memento
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <returns>IConnectionConfiguration.</returns>
        public IConnectionConfiguration Create(IMemento memento)
        {
            var myMemento = (IntegrationEndpointConfigurationMemento)memento;
            return JsonConvert.DeserializeObject<JmsConnectionConfiguration>(myMemento.ConnectionDetails);
        }
    }
}