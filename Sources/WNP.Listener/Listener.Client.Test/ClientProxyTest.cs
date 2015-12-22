// <copyright file="ClientProxyTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client.Test
{
    using Exception;
    using Message;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ClientProxyTest
    {
        [TestMethod]
        [ExpectedException(typeof(FailedToOpenTransactionException))]
        public void TestListenerClientSuccess()
        {
            var proxy = new ListenerProxy(new DummyClientAdapter(), new ListenerRequestHeaderDictionary());

            var lc = new ListenerClient("http://localhost", proxy);

            lc.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.Fail("Should not have reached this point without exceptions");
        }
    }
}
