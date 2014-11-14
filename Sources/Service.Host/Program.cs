//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Host
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    using Listener.Common;
    using log4net;
    using log4net.Config;

    /// <summary>
    /// Main entry class for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
               
        /// <summary>
        /// The main entry point for the application.
        /// Runs service as a console application or as windows service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This method is used only for debugging of program and not presented to user")]
        public static void Main()
        {
            XmlConfigurator.Configure();

            try
            {
                if (!Environment.UserInteractive)
                {
                    ServiceBase[] servicesToRun;
                    servicesToRun = new ServiceBase[] 
                { 
                    new CustomServiceHost() 
                };
                    ServiceBase.Run(servicesToRun);
                }
                else
                {
                    using (CustomServiceHost consoleApp = new CustomServiceHost())
                    {
                        // running as console app
                        consoleApp.OpenAll();
                        Console.WriteLine("**");
                        Console.WriteLine("** Web service started successfully. Press any key to stop.");
                        Console.WriteLine("**");
                        Console.ReadKey(true);

                        consoleApp.CloseAll();
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error("Unknown error occured.", exception);
                Console.WriteLine("**");
                Console.WriteLine("** Web service failed to start. Press any key to exit.");
                Console.WriteLine("**");
                Console.ReadKey(true);
            }
        }
    }
}
