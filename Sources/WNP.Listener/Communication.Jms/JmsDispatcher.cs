// //-----------------------------------------------------------------------
// // <copyright file="JmsDispatcher.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.Listener.Transaction.DomainEvent;
    using WebLogic.Messaging;

    /// <summary>
    /// Jms data dispatcher
    /// </summary>
    [DomainEventHandler(typeof(JmsDataReady))]
    public class JmsDispatcher : IDomainHandler
    {
        private static string cfName = "weblogic.jms.ConnectionFactory";

        /// <summary>
        /// Handles the specified event data.
        /// </summary>
        /// <param name="eventData">The event data.</param>
        /// <returns>Task.</returns>
        public Task Handle(object eventData)
        {
            var requestData = eventData as JmsDataReady;

            if (requestData == null)
            {
                throw new ArgumentException("eventData must be of type {0}".FormatWith(typeof(JmsDataReady).FullName));
            }

            // create properties dictionary
            IDictionary<string, Object> paramMap = new Dictionary<string, Object>();

            // add necessary properties
            paramMap[Constants.Context.PROVIDER_URL] = "t3://{0}:{1}".FormatWith(requestData.Endpoint.Host,
                requestData.Endpoint.Port);
            paramMap[Constants.Context.SECURITY_PRINCIPAL] = requestData.Endpoint.UserName;
            paramMap[Constants.Context.SECURITY_CREDENTIALS] = requestData.Endpoint.Password;

            // get the initial context
            IContext context = ContextFactory.CreateContext(paramMap);

            // lookup the connection factory
            IConnectionFactory cf = context.LookupConnectionFactory(cfName);

            // lookup the queue
            IQueue queue = (IQueue)context.LookupDestination(requestData.Endpoint.QueueName);

            // create a connection
            IConnection connection = cf.CreateConnection();

            // start the connection
            connection.Start();

            // create a session
            ISession session = connection.CreateSession(Constants.SessionMode.AUTO_ACKNOWLEDGE);

            // create a message producer
            IMessageProducer producer = session.CreateProducer(queue);

            producer.DeliveryMode = Constants.DeliveryMode.PERSISTENT;

            // create a text message
            ITextMessage textMessage = session.CreateTextMessage(requestData.Message);

            // send the message
            producer.Send(textMessage);

            // CLEAN UP
            connection.Close();

            context.CloseAll();

            return Task.CompletedTask;
        }
    }
}