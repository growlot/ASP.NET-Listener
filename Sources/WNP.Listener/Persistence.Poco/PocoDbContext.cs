// <copyright file="PocoDbContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Poco
{
    using System.Data;
    using System.Data.Common;
    using Serilog;

    /// <summary>
    /// Common db context for all databases accessed with POCO.
    /// </summary>
    public class PocoDbContext : Database
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PocoDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The DB connection string</param>
        /// <param name="providerName">The name of the DB provider to use</param>
        /// <remarks>
        /// PetaPoco will automatically close and dispose any connections it creates.
        /// </remarks>
        public PocoDbContext(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to use</param>
        /// <param name="provider">The DbProviderFactory to use for instantiating DbConnection's</param>
        public PocoDbContext(string connectionString,  DbProviderFactory provider)
            : base(connectionString, provider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringName">The name of the connection</param>
        public PocoDbContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        /// <inheritdoc/>
        public override DbConnection OnConnectionOpened(DbConnection conn)
        {
            return base.OnConnectionOpened(conn);
        }

        /// <inheritdoc/>
        public override void OnExecutingCommand(
            IDbCommand cmd)
        {
            Log.Debug(cmd.CommandText);
            base.OnExecutingCommand(cmd);
        }
    }
}