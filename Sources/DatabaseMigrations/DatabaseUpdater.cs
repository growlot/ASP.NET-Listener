//-----------------------------------------------------------------------
// <copyright file="DatabaseUpdater.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System.Configuration;
    using System.Globalization;
    using System.Reflection;
    using AMSLLC.Listener.Common;
    using FluentMigrator;
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;
    using FluentMigrator.Runner.Processors.Oracle;
    using FluentMigrator.Runner.Processors.SqlServer;
    using log4net;

    /// <summary>
    /// Updates or destroys the database
    /// </summary>
    public class DatabaseUpdater
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The migration runner
        /// </summary>
        private MigrationRunner runner;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseUpdater"/> class.
        /// </summary>
        public DatabaseUpdater()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseUpdater" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Throws exception if database dialect defined in nHibernate configuration is not recognized/supported in FluentMigrator</exception>
        public DatabaseUpdater(IPersistenceManager persistenceManager)
        {
            IMigrationProcessor processor = null;

            // var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            TextWriterAnnouncer announcer = new TextWriterAnnouncer(s => Logger.Info(s));

            Assembly assembly = typeof(DatabaseUpdater).Assembly;

            RunnerContext migrationContext = new RunnerContext(announcer)
            {
                Namespace = "AMSLLC.Listener.DatabaseMigrations",

                // ApplicationContext = "AlliantLoad"
                ApplicationContext = ConfigurationManager.AppSettings["Customer"]
            };

            MigrationOptions options = new MigrationOptions { PreviewOnly = false, Timeout = 60, ProviderSwitches = string.Empty };
            string database;
            string connectionString;

            if (persistenceManager == null)
            {
                using (IPersistenceManager persistenceManagerTemp = new PersistenceManager())
                {
                    connectionString = persistenceManagerTemp.ConnectionString;
                    database = persistenceManagerTemp.Database;
                }
            }
            else
            {
                connectionString = persistenceManager.ConnectionString;
                database = persistenceManager.Database;
            }

            switch (database)
            {
                case "MsSql2005":
                    SqlServer2005ProcessorFactory factoryMsSql2005 = new SqlServer2005ProcessorFactory();
                    processor = factoryMsSql2005.Create(connectionString, announcer, options);
                    break;
                case "MsSql2008":
                    SqlServer2008ProcessorFactory factoryMsSql2008 = new SqlServer2008ProcessorFactory();
                    processor = factoryMsSql2008.Create(connectionString, announcer, options);
                    break;
                case "MsSql2012":
                    SqlServer2012ProcessorFactory factoryMsSql2012 = new SqlServer2012ProcessorFactory();
                    processor = factoryMsSql2012.Create(connectionString, announcer, options);
                    break;
                case "Oracle10g":
                    OracleProcessorFactory factoryOracle = new OracleProcessorFactory();
                    processor = factoryOracle.Create(connectionString, announcer, options);
                    break;
                case "AMSLLC.Listener.Common.NHibernateOracle12c":
                    Oracle12cProcessorFactory factoryOracle12 = new Oracle12cProcessorFactory();
                    processor = factoryOracle12.Create(connectionString, announcer, options);
                    break;
                default:
                    string message = string.Format(CultureInfo.CurrentCulture, "Can not perform migrations for unknown database {0}", database);
                    throw new ConfigurationErrorsException(message);
            }

            this.runner = new MigrationRunner(assembly, migrationContext, processor);
        }

        /// <summary>
        /// Updates database to latest version.
        /// </summary>
        public void UpdateDatabase()
        {
            this.runner.MigrateUp(true);
        }

        /// <summary>
        /// Destroys the database by undoing all migrations.
        /// </summary>
        public void DestroyDatabase()
        {
            this.runner.MigrateDown(0, true);
        }

        /// <summary>
        /// Implements Migration Processor Options interface
        /// </summary>
        private class MigrationOptions : IMigrationProcessorOptions
        {
            /// <summary>
            /// Gets or sets a value indicating whether migration will run only preview.
            /// </summary>
            /// <value>
            ///   <c>true</c> if [preview only]; otherwise, <c>false</c>.
            /// </value>
            public bool PreviewOnly { get; set; }

            /// <summary>
            /// Gets or sets the provider switches.
            /// </summary>
            /// <value>
            /// The provider switches.
            /// </value>
            public string ProviderSwitches { get; set; }

            /// <summary>
            /// Gets or sets the timeout for migration process.
            /// </summary>
            /// <value>
            /// The timeout.
            /// </value>
            public int Timeout { get; set; }
        }
    }
}
