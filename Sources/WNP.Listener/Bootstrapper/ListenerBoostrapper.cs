using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace AMSLLC.Listener.Bootstrapper
{
    public class ListenerBoostrapper : ServiceControl
    {
        private readonly string _protocol;
        private readonly string _hostName;
        private readonly int _port;
        private readonly bool _isDebug;

        private IDisposable _owinHost;

        public ListenerBoostrapper(string protocol, string hostName, int port, bool isDebug)
        {
            this._protocol = protocol;
            this._hostName = hostName;
            this._port = port;
            this._isDebug = isDebug;
        }

        public bool Start(HostControl hostControl)
        {
            string baseAddress = $"{this._protocol}://{this._hostName}:{this._port}";
            var startOptions = new StartOptions(baseAddress) { ServerFactory = "Microsoft.Owin.Host.HttpListener" };

            this._owinHost = WebApp.Start<Startup>(startOptions);

            if (!this._isDebug)
                return true;

            var chromeCanaryPath = @"C:\Users\Fen\AppData\Local\Google\Chrome SxS\Application\chrome.exe";
            if (File.Exists(chromeCanaryPath))
                Process.Start(new ProcessStartInfo(chromeCanaryPath, baseAddress));

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this._owinHost.Dispose();
            return true;
        }
    }
}