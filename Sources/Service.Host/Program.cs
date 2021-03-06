﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Host
{
    using System;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.IO;
    using System.ServiceProcess;
    using log4net;
    using log4net.Config;

    /// <summary>
    /// Main entry class for the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The service host
        /// </summary>
#pragma warning disable 0649
        [Import]
        private CustomServiceHost serviceHost;
#pragma warning restore 0649

        /// <summary>
        /// Prevents a default instance of the <see cref="Program" /> class from being created.
        /// <see href="https://msdn.microsoft.com/en-us/library/dd460648(v=vs.100).aspx">MEF reference</see>
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to catch all exceptions before pgoram crashes, so they would be logged")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This method is used only for debugging of program and not presented to user")]
        private Program()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            using (AggregateCatalog catalog = new AggregateCatalog())
            {
                catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
                catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory + "\\Plugins"));

                using (CompositionContainer container = new CompositionContainer(catalog))
                {
                    try
                    {
                        container.ComposeParts(this);
                    }
                    catch (CompositionException exception)
                    {
                        Logger.Error("Composition exception occured.", exception);
                    }

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
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// Runs service as a console application or as windows service.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to catch all exceptions before pgoram crashes, so they would be logged")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "This method is used only for debugging of program and not presented to user")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "AMSLLC.Listener.Service.Host.Program", Justification = "Call to constructor actually runs the whole program.")]
        public static void Main()
        {
            try
            {
                XmlConfigurator.Configure();
                new Program();
            }
            catch (Exception exception)
            {
                Logger.Error("Unknown error occured.", exception);
                if (Environment.UserInteractive)
                {
                    Console.WriteLine("**");
                    Console.WriteLine("** Web service failed to start. Press any key to exit.");
                    Console.WriteLine("**");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
