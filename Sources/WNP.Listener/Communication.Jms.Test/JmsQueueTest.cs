// //-----------------------------------------------------------------------
// <copyright file="JmsQueueTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Communication.Jms.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using Domain.Listener.Transaction;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using WebLogic.Messaging;

    [TestClass]
    public class JmsQueueTest
    {
        private const string Host = "localhost";

        private const int Port = 7001;

        private const string QueueName = "jms/AMSIntegration";

        private const string Username = "ams";

        private const string Password = "Password1";

        private static string cfName = "weblogic.jms.ConnectionFactory";

        [TestMethod]
        public async Task TestPutMessage()
        {
            try
            {
                JmsDispatcher dispatcher = new JmsDispatcher();
                var transactionId = Guid.NewGuid().ToString();
                var testMessage = new TestMessage { TransactionId = transactionId, Value = new Random().Next(9999) };
                var eventData = new TransactionDataReady { Data = testMessage };
                var connectionConfiguration = new JmsConnectionConfiguration
                {
                    Host = Host,
                    Password = Password,
                    UserName = Username,
                    QueueName = QueueName,
                    Port = Port
                };
                await dispatcher.Handle(eventData, connectionConfiguration);
                this.ReadMessage(JsonConvert.SerializeObject(testMessage), connectionConfiguration);
            }
            catch (MessageException exc)
            {
                Assert.Inconclusive("Jms communication failed: {0}".FormatWith(exc.Message));
            }
        }

        private void ReadMessage(string messageBody, JmsConnectionConfiguration config)
        {
            // create properties dictionary
            IDictionary<string, Object> paramMap = new Dictionary<string, Object>();

            // add necessary properties
            paramMap[Constants.Context.PROVIDER_URL] = "t3://{0}:{1}".FormatWith(config.Host, config.Port);
            paramMap[Constants.Context.SECURITY_PRINCIPAL] = config.UserName;
            paramMap[Constants.Context.SECURITY_CREDENTIALS] = config.Password;

            // get the initial context
            IContext context = ContextFactory.CreateContext(paramMap);

            // lookup the connection factory
            IConnectionFactory cf = context.LookupConnectionFactory(cfName);

            // lookup the queue
            IQueue queue = (IQueue)context.LookupDestination(config.QueueName);

            // create a connection
            IConnection connection = cf.CreateConnection();

            // start the connection
            connection.Start();

            // create a session
            ISession session = connection.CreateSession(Constants.SessionMode.AUTO_ACKNOWLEDGE);

            var consumer = session.CreateConsumer(queue);

            var found = false;
            ITextMessage message;
            while ((message = (ITextMessage)consumer.ReceiveNoWait()) != null)
            {
                found = string.Compare(message.Text, messageBody, StringComparison.InvariantCulture) == 0;
                if (found)
                {
                    break;
                }
            }

            Assert.IsTrue(found, "Expected message was not found");

            consumer.Close();

            // CLEAN UP
            connection.Close();

            context.CloseAll();
        }

        private class TestMessage
        {
            public string TransactionId { get; set; }

            public int Value { get; set; }
        }
    }
}