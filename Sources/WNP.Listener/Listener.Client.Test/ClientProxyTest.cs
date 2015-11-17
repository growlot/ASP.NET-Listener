using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Client.Test
{
    using Exception;
    using Message;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ClientProxyTest
    {
        [TestMethod]
        [ExpectedException(typeof(FailedToOpenTransactionException))]
        public void TestListenerClientSuccess()
        {
            var proxy = new ListenerProxy(new DummyClientAdapter());
           
            var lc = new ListenerClient("http://localhost", proxy);

            lc.ProcessDeviceUpdate(new DeviceUpdateMessage());

            Assert.Fail("Should not have reached this point without exceptions");
        }
    }
}
