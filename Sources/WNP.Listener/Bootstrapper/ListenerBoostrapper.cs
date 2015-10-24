// <copyright file="ListenerBoostrapper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Microsoft.Owin.Hosting;
    using Topshelf;

    /// <summary>
    /// Implments <see cref="ServiceControl"/>
    /// </summary>
    public class ListenerBoostrapper : ServiceControl
    {
        private readonly string protocol;
        private readonly string hostName;
        private readonly int port;
        private readonly bool isDebug;

        private IDisposable owinHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerBoostrapper"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="port">The port.</param>
        /// <param name="isDebug">if set to <c>true</c> [is debug].</param>
        public ListenerBoostrapper(string protocol, string hostName, int port, bool isDebug)
        {
            this.protocol = protocol;
            this.hostName = hostName;
            this.port = port;
            this.isDebug = isDebug;
        }

        /// <inheritdoc/>
        public bool Start(HostControl hostControl)
        {
            string baseAddress = $"{this.protocol}://{this.hostName}:{this.port}";
            var startOptions = new StartOptions(baseAddress) { ServerFactory = "Microsoft.Owin.Host.HttpListener" };

            this.owinHost = WebApp.Start<Startup>(startOptions);

            if (!this.isDebug)
            {
                return true;
            }

            var chromeCanaryPath = @"C:\Users\Fen\AppData\Local\Google\Chrome SxS\Application\chrome.exe";
            if (File.Exists(chromeCanaryPath))
            {
                Process.Start(new ProcessStartInfo(chromeCanaryPath, baseAddress));
            }

            return true;
        }

        /// <inheritdoc/>
        public bool Stop(HostControl hostControl)
        {
            this.owinHost.Dispose();
            return true;
        }
    }
}