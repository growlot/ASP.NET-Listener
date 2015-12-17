// <copyright file="Program.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;

    using Serilog;
    using Topshelf;

    internal class Program
    {
        private static void Main(string[] args)
        {
            // force initialize logger settings to handle exception during startup
            Log.Logger = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();

            // set unhandled exceptions to go to the log
            AppDomain.CurrentDomain.FirstChanceException +=
                (sender, eventArgs) => Log.Error(eventArgs.Exception, "First chance exception occured");

            var protocol = ConfigurationManager.AppSettings["DefaultProtocol"];
            var hostName = ConfigurationManager.AppSettings["DefaultHostname"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPort"]);
            var isDebug = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugEnabled"]);

            HostFactory.Run(x =>
            {
                x.UseSerilog();

                x.AddCommandLineDefinition("protocol", s => protocol = s);
                x.AddCommandLineDefinition("hostname", s => hostName = s);
                x.AddCommandLineDefinition("port", s => port = Convert.ToInt32(s));
                x.AddCommandLineDefinition("debug", s => isDebug = Convert.ToBoolean(s));

                x.ApplyCommandLine();

                x.Service<ListenerBoostrapper>(s =>
                {
                    s.ConstructUsing(_ => new ListenerBoostrapper(protocol, hostName, port, isDebug));

                    s.WhenStarted((boostrapper, control) => boostrapper.Start(control));
                    s.WhenStopped((boostrapper, control) => boostrapper.Stop(control));
                });

                x.RunAsNetworkService();

                x.SetDescription("AMSLLC Listener Service");
                x.SetDisplayName("AMSLLC Listener Service");
                x.SetServiceName("AMSLLCListener");

                x.StartAutomatically();
            });
        }
    }
}
