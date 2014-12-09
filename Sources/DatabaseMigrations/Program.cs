//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using log4net;
    using log4net.Config;

    /// <summary>
    /// Runs Database migrations
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Justification = "Don't need to provide multilanguage support")]
        public static void Main()
        {
            XmlConfigurator.Configure();

            try
            {
                DatabaseUpdater databaseUpdater = new DatabaseUpdater();
                                
                //// databaseUpdater.Downgrade(21);
                //// databaseUpdater.DestroyDatabase();
                databaseUpdater.UpdateDatabase();

                Console.WriteLine("**");
                Console.WriteLine("** Database was successfully upgraded. Press any key to close.");
                Console.WriteLine("**");
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Logger.Error("Unknown error occured during database upgrade.", exception);

                Console.WriteLine("**");
                Console.WriteLine("** Database upgrade FAILED. Press any key to close.");
                Console.WriteLine("**");
                Console.ReadKey();
            }
        }
    }
}
