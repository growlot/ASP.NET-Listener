﻿// <copyright file="JmsDispatcher.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication.Jms
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Domain.Listener;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Repository.Listener;
    using Serilog;
    using Utilities;
    using WebLogic.Messaging;

    /// <summary>
    /// Jms data dispatcher
    /// </summary>
    public class JmsDispatcher : ICommunicationHandler
    {
        private const string GenericMessageType = "GenericMessage";
        private static string cfName = "weblogic.jms.ConnectionFactory";

        private readonly ITransactionDataRepository transactionDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JmsDispatcher"/> class.
        /// </summary>
        /// <param name="transactionDataRepository">The transaction data repository.</param>
        public JmsDispatcher(ITransactionDataRepository transactionDataRepository)
        {
            this.transactionDataRepository = transactionDataRepository;
        }

        /// <summary>
        /// Creates the type of the message.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="messageTypeTemplate">The message type template.</param>
        /// <returns>System.String.</returns>
        public static string CreateMessageType(object data, string messageTypeTemplate)
        {
            if (messageTypeTemplate == null)
            {
                return GenericMessageType;
            }

            string returnValue = messageTypeTemplate;
            var regex = new Regex("{.*?}");
            var matches = regex.Matches(messageTypeTemplate);
            foreach (Match match in matches)
            {
                DynamicUtilities.ProcessProperty(data, match.Value.Substring(1, match.Value.Length - 2), null, (info, o, arg3) =>
                {
                    returnValue = returnValue.Replace(match.Value, info.GetValue(o)?.ToString());
                });
            }

            Log.Information(StringUtilities.Invariant($"JMS: Message type: {returnValue}"));
            return returnValue;
        }

        /// <summary>
        /// Handles the specified request data.
        /// </summary>
        /// <param name="requestData">The request data.</param>
        /// <param name="connectionConfiguration">The connection configuration.</param>
        /// <param name="protocolConfiguration">The protocol configuration.</param>
        /// <returns>Task.</returns>
        /// <exception cref="System.ArgumentException">eventData must be of type {0}.FormatWith(typeof(TransactionDataReady).FullName)</exception>
        public async Task Handle(object requestData, IConnectionConfiguration connectionConfiguration, IProtocolConfiguration protocolConfiguration)
        {
            var request = requestData as TransactionDataReady;
            var cfg = connectionConfiguration as JmsConnectionConfiguration;

            var jmsAdapterConfiguration = (ProtocolConfiguration)protocolConfiguration;

            if (request == null)
            {
                throw new ArgumentException("{0} must be of type {1}".FormatWith(nameof(requestData), typeof(TransactionDataReady).FullName));
            }

            if (cfg == null)
            {
                throw new ArgumentException("{0} must be of type {1}".FormatWith(nameof(connectionConfiguration), typeof(JmsConnectionConfiguration).FullName));
            }

            await this.transactionDataRepository.SaveDataAsync(request.RecordKey, request.Data);
            this.PutMessage(cfg, request, jmsAdapterConfiguration);
        }

        /// <summary>
        /// Puts the message.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="request">The request.</param>
        /// <param name="jmsAdapterConfiguration">The JMS adapter configuration.</param>
        public virtual void PutMessage(JmsConnectionConfiguration configuration, TransactionDataReady request, ProtocolConfiguration jmsAdapterConfiguration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Log.Information("JMS: Sending message to {0}:{1}", configuration.Host, configuration.Port);

            try
            {
                // create properties dictionary
                IDictionary<string, object> paramMap = new Dictionary<string, object>();

                // add necessary properties
                paramMap[Constants.Context.PROVIDER_URL] = "t3://{0}:{1}".FormatWith(
                    configuration.Host,
                    configuration.Port);
                paramMap[Constants.Context.SECURITY_PRINCIPAL] = configuration.UserName;
                paramMap[Constants.Context.SECURITY_CREDENTIALS] = configuration.Password;

                Log.Debug("JMS: ParamMap built");

                // get the initial context
                IContext context = ContextFactory.CreateContext(paramMap);

                Log.Debug("JMS: Context Created");

                // lookup the connection factory
                IConnectionFactory cf = context.LookupConnectionFactory(cfName);

                // lookup the queue
                IQueue queue = (IQueue)context.LookupDestination(configuration.QueueName);

                // create a connection
                IConnection connection = cf.CreateConnection();

                Log.Debug("JMS: Connection created");

                // start the connection
                connection.Start();

                Log.Debug("JMS: Connection started");

                // create a session
                ISession session = connection.CreateSession(Constants.SessionMode.AUTO_ACKNOWLEDGE);

                Log.Debug("JMS: Session started");

                // create a message producer
                IMessageProducer producer = session.CreateProducer(queue);

                Log.Debug("JMS: Producer created");

                producer.DeliveryMode = Constants.DeliveryMode.PERSISTENT;

                // create a text message
                ITextMessage textMessage = CreateMessage(session, request, jmsAdapterConfiguration);

                Log.Debug("JMS: Connection created");

                // send the message
                producer.Send(textMessage);

                Log.Information("JMS: Message sent");

                // CLEAN UP
                connection.Close();

                context.CloseAll();

                Log.Debug("JMS: Cleaned");
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Exception occured while sending message");
                throw;
            }
        }

        private static ITextMessage CreateMessage(ISession session, TransactionDataReady data, ProtocolConfiguration configuration)
        {
            Log.Information("JMS: Creating message");
            var returnValue = session.CreateTextMessage(JsonConvert.SerializeObject(data.Data));
            returnValue.JMSType = CreateMessageType(data.Data, configuration.MessageTypeTemplate);
            return returnValue;
        }
    }
}