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
            var proxyMock = new Mock<IListenerProxy>();
            proxyMock.Setup(
                s => s.OpenAsync(It.IsAny<Uri>(), It.IsAny<object>()))
                .ReturnsAsync(basicResponse);
            var lc = new ListenerClient("http://localhost", proxyMock.Object);

            lc.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.IsTrue(true, "Should have reached this point without exceptions");
        }

        [TestMethod]
        [ExpectedException(typeof(FailedToOpenTransactionException), "Should have failed with FailedToOpenTransactionException")]
        public void TestListenerClientHttpResponseErrorFailed()
        {
            var proxyMock = new Mock<IListenerProxy>();
            proxyMock.Setup(
                s => s.OpenAsync(It.IsAny<Uri>(), It.IsAny<object>())).ThrowsAsync(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));
            var lc = new ListenerClient("http://localhost", proxyMock.Object);

            lc.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.Fail("Shouldn't have reached this");
        }

        [TestMethod]
        public void TestListenerClientProcessingFailed()
        {
            var proxyMock = new Mock<IListenerProxy>();
            proxyMock.Setup(
                s => s.OpenAsync(It.IsAny<Uri>(), It.IsAny<object>()))
                .ReturnsAsync(basicResponse);
            var mq = new Mock<ListenerClient>("http://localhost", proxyMock.Object) { CallBase = true };

            mq.Setup(s => s.ProcessTransaction(It.IsAny<string>()))
                .Throws(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));

            try
            {
                mq.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());
            }
            catch (FailedToProcessTransactionException)
            {
                //this is expected
            }

            mq.Verify(f => f.OpenTransaction(It.IsAny<Uri>(), It.IsAny<object>()), Times.Once);
            mq.Verify(f => f.SucceedTransaction(It.IsAny<string>()), Times.Never);
            mq.Verify(f => f.FailTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void TestListenerClientSucceedingFailed()
        {
            var proxyMock = new Mock<IListenerProxy>();
            proxyMock.Setup(
                s => s.OpenAsync(It.IsAny<Uri>(), It.IsAny<object>()))
                .ReturnsAsync(basicResponse);
            var mq = new Mock<ListenerClient>("http://localhost", proxyMock.Object) { CallBase = true };

            mq.Setup(s => s.SucceedTransaction(It.IsAny<string>()))
                .Throws(new ListenerRequestFailedException(HttpStatusCode.InternalServerError));

            try
            {
                mq.Object.ProcessDeviceUpdate(new DeviceUpdateMessage());
            }
            catch (FailedToSucceedTransactionException)
            {
                //This is expected
            }

            mq.Verify(f => f.OpenTransaction(It.IsAny<Uri>(), It.IsAny<object>()), Times.Once);
            mq.Verify(f => f.SucceedTransaction(It.IsAny<string>()), Times.Once);
            mq.Verify(f => f.FailTransaction(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
