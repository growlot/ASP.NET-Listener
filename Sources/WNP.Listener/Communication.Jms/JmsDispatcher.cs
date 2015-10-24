// //-----------------------------------------------------------------------
// <copyright file="JmsDispatcher.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using WebLogic.Messaging;

    /// <summary>
    /// Jms data dispatcher
    /// </summary>
    public class JmsDispatcher : ICommunicationHandler
    {
        private static string cfName = "weblogic.jms.ConnectionFactory";

        /// <summary>
        /// Handles the specified request data.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="connectionConfiguration">The connection configuration.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">eventData must be of type {0}.FormatWith(typeof(TransactionDataReady).FullName)</exception>
        public Task Handle(object requestData, IConnectionConfiguration connectionConfiguration)
        {
            var request = requestData as TransactionDataReady;
            var cfg = connectionConfiguration as JmsConnectionConfiguration;

            if (request == null)
            {
                throw new ArgumentException("{0} must be of type {1}".FormatWith(nameof(requestData), typeof(TransactionDataReady).FullName));
            }

            if (cfg == null)
            {
                throw new ArgumentException("{0} must be of type {1}".FormatWith(nameof(connectionConfiguration), typeof(JmsConnectionConfiguration).FullName));
            }

            return Task.Run(() =>
            {
                // create properties dictionary
                IDictionary<string, object> paramMap = new Dictionary<string, object>();

                // add necessary properties
                paramMap[Constants.Context.PROVIDER_URL] = "t3://{0}:{1}".FormatWith(cfg.Host, cfg.Port);
                paramMap[Constants.Context.SECURITY_PRINCIPAL] = cfg.UserName;
                paramMap[Constants.Context.SECURITY_CREDENTIALS] = cfg.Password;

                // get the initial context
                IContext context = ContextFactory.CreateContext(paramMap);

                // lookup the connection factory
                IConnectionFactory cf = context.LookupConnectionFactory(cfName);

                // lookup the queue
                IQueue queue = (IQueue)context.LookupDestination(cfg.QueueName);

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
                ITextMessage textMessage = session.CreateTextMessage(JsonConvert.SerializeObject(request.Data));

                // send the message
                producer.Send(textMessage);

                // CLEAN UP
                connection.Close();

                context.CloseAll();
            });
        }
    }
}