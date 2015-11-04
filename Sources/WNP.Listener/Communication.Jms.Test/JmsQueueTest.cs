// <copyright file="JmsQueueTest.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Communication.Jms.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Listener.Transaction;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Repository;
    using WebLogic.Messaging;

    /// <summary>
    /// Tests JMS queue
    /// </summary>
    [TestClass]
    public class JmsQueueTest
    {
        private const string Host = "localhost";

        private const int Port = 7001;

        private const string QueueName = "jms/AMSIntegration";

        private const string Username = "ams";

        private const string Password = "Password1";

        private static string cfName = "weblogic.jms.ConnectionFactory";

        /// <summary>
        /// Tests message sending to JMS queue.
        /// </summary>
        /// <returns>The empty task</returns>
        [TestMethod]
        public async Task TestPutMessageAsync()
        {
            try
            {
                var transactionMessageDataRepMock = new Mock<ITransactionDataRepository>();
                JmsDispatcher dispatcher = new JmsDispatcher(transactionMessageDataRepMock.Object);
                var transactionId = Guid.NewGuid().ToString();
                var testMessage = new TestMessage { TransactionId = transactionId, Value = new Random().Next(9999) };
                var eventData = new TransactionDataReady { Data = testMessage };
                var connectionConfiguration = new JmsConnectionConfiguration { Host = Host, Password = Password, UserName = Username, QueueName = QueueName, Port = Port };

                var protocolConfiguration = new ProtocolConfiguration();

                await dispatcher.Handle(eventData, connectionConfiguration, protocolConfiguration);
                ReadMessage(JsonConvert.SerializeObject(testMessage), connectionConfiguration);
            }
            catch (MessageException exc)
            {
                Assert.Inconclusive("Jms communication failed: {0}".FormatWith(exc.Message));
            }
        }

        /// <summary>
        /// Tests the message type builder.
        /// </summary>
        [TestMethod]
        public void TestMessageTypeBuilder()
        {
            const string template = "Type{EntityCategory}:{OperationKey}.{EntityCategory}?";
            const string expected = "TypeElectricMeters:Install.ElectricMeters?";
            var data = new DataMessage { EntityCategory = "ElectricMeters", OperationKey = "Install", SomeOtherProperty = 99 };
            var actual = JmsDispatcher.CreateMessageType(data, template);
            Assert.AreEqual(expected, actual);
        }

        private static void ReadMessage(string messageBody, JmsConnectionConfiguration config)
        {
            // create properties dictionary
            IDictionary<string, object> paramMap = new Dictionary<string, object>();

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
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive, property is accessed during serialization to Json string")]
            public string TransactionId { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive, property is accessed during serialization to Json string")]
            public int Value { get; set; }
        }

        private class DataMessage
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive, property is accessed during serialization to Json string")]
            public string EntityCategory { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive, property is accessed during serialization to Json string")]
            public string OperationKey { get; set; }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "False positive, property is accessed during serialization to Json string")]
            public int SomeOtherProperty { get; set; }
        }
    }
}