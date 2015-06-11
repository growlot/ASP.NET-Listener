//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Host
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "temp")]
    public class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The composition container
        /// </summary>
        private CompositionContainer container;

        /// <summary>
        /// The service host
        /// </summary>
#pragma warning disable 0649
        [Import]
        private CustomServiceHost serviceHost;
#pragma warning restore 0649

        /// <summary>
        /// Prevents a default instance of the <see cref="Program"/> class from being created.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "testing")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This method is used only for debugging of program and not presented to user")]
        private Program()
        {
            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
                catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory + "\\Plugins"));

                this.container = new CompositionContainer(catalog);

                try
                {
                    this.container.ComposeParts(this);
                }
                catch (Exception exception)
                {
                    Logger.Error("Composition exception occured.", exception);
                    Console.WriteLine(exception.ToString());
                }

                try
                {
                    if (!Environment.UserInteractive)
                    {
                        ServiceBase[] servicesToRun;
                        servicesToRun = new ServiceBase[] 
                            { 
                                this.serviceHost
                            };
                        ServiceBase.Run(servicesToRun);
                    }
                    else
                    {
                        this.serviceHost.OpenAll();
                        Console.WriteLine("**");
                        Console.WriteLine("** Web service started successfully. Press any key to stop.");
                        Console.WriteLine("**");
                        Console.ReadKey(true);

                        this.serviceHost.CloseAll();
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

        /// <summary>
        /// The main entry point for the application.
        /// Runs service as a console application or as windows service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "program", Justification = "Need to initialize the composition of the program")]
        public static void Main()
        {
            XmlConfigurator.Configure();
            Program program = new Program();
        }
    }
}
