using System;
using System.Configuration;
using Topshelf;

namespace AMSLLC.Listener.Bootstrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            var protocol = ConfigurationManager.AppSettings["DefaultProtocol"];
            var hostName = ConfigurationManager.AppSettings["DefaultHostname"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultPort"]);
            var isDebug = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugEnabled"]);

            HostFactory.Run(x =>
            {
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
