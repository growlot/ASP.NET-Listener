// <copyright file="ListenerClientFlowTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Test
{
    using System;
    using System.Net;
    using Exception;
    using Message;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ListenerClientFlowTest
    {
        private const string basicResponse = "{\"value\":\"TestKey\"}";

        [TestMethod]
        public void TestListenerClientSuccess()
        {
            var lc = new Mock<ListenerClient>("http://localhost") ;
            lc.Setup(
                s => s.OpenTransaction(It.IsAny<BaseListenerRequestMessage>()))
                .Returns(basicResponse);
            //var lc = new ListenerClient("http://localhost", proxyMock.Object);

            lc.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.IsTrue(true, "Should have reached this point without exceptions");
        }

        [TestMethod]
        [ExpectedException(typeof(FailedToOpenTransactionException), "Should have failed with FailedToOpenTransactionException")]
        public void TestListenerClientHttpResponseErrorFailed()
        {
            var lc = new Mock<ListenerClient>("http://localhost");
            lc.Setup(
                s => s.OpenTransaction(It.IsAny<BaseListenerRequestMessage>())).Throws(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));
            //var lc = new ListenerClient("http://localhost", proxyMock.Object);

            lc.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.Fail("Shouldn't have reached this");
        }

        [TestMethod]
        public void TestListenerClientProcessingFailed()
        {
            var lc = new Mock<ListenerClient>("http://localhost");
            lc.Setup(
                s => s.OpenTransaction(It.IsAny<BaseListenerRequestMessage>()))
                .Returns(basicResponse);
            //var mq = new Mock<ListenerClient>("http://localhost", proxyMock.Object) { CallBase = true };

            lc.Setup(s => s.ProcessTransaction(It.IsAny<string>()))
                .Throws(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));

            try
            {
                lc.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());
            }
            catch (FailedToProcessTransactionException)
            {
                //this is expected
            }

            lc.Verify(f => f.OpenTransaction(It.IsAny<BaseListenerRequestMessage>()), Times.Once);
            lc.Verify(f => f.SucceedTransaction(It.IsAny<string>()), Times.Never);
            lc.Verify(f => f.FailTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void TestListenerClientSucceedingFailed()
        {
            var lc = new Mock<ListenerClient>("http://localhost");
            lc.Setup(
                s => s.OpenTransaction(It.IsAny<BaseListenerRequestMessage>()))
                .Returns(basicResponse);
            // var mq = new Mock<ListenerClient>("http://localhost", proxyMock.Object) { CallBase = true };

            lc.Setup(s => s.SucceedTransaction(It.IsAny<string>()))
                .Throws(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));

            try
            {
                lc.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());
            }
            catch (FailedToSucceedTransactionException)
            {
                //This is expected
            }

            lc.Verify(f => f.OpenTransaction(It.IsAny<BaseListenerRequestMessage>()), Times.Once);
            lc.Verify(f => f.SucceedTransaction(It.IsAny<string>()), Times.Once);
            lc.Verify(f => f.FailTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

    }
}
