// //-----------------------------------------------------------------------
// <copyright file="JmsConnectionConfigurationBuilder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms
{
    using Domain;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;

    public class JmsConnectionConfigurationBuilder : IConnectionConfigurationBuilder
    {
        /// <summary>
        /// Create Jms connection configuration using provided memento
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