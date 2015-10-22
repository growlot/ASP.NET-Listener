// <copyright file="WNPDbContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence
{
    using System;
    using System.Data;
    using System.Data.Common;
    using Serilog;
    using StackExchange.Profiling;
    using StackExchange.Profiling.Data;

    /// <summary>
    /// WNP database context based on PetaPoco
    /// </summary>
    public class WNPDBContext : Database
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WNPDBContext"/> class.
        /// </summary>
        /// <param name="connectionStringName">The name of the connection</param>
        public WNPDBContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        /// <inheritdoc/>
        public override bool OnException(Exception x)
        {
            return base.OnException(x);
        }

        /// <inheritdoc/>
        public override IDbConnection OnConnectionOpened(IDbConnection conn)
        {
            if (MiniProfiler.Current != null)
            {
                return new ProfiledDbConnection((DbConnection)conn, MiniProfiler.Current);
            }

            return base.OnConnectionOpened(conn);
        }

        /// <inheritdoc/>
        public override void OnConnectionClosing(IDbConnection conn)
        {
            base.OnConnectionClosing(conn);
        }

        /// <inheritdoc/>
        public override void OnExecutingCommand(IDbCommand cmd)
        {
            base.OnExecutingCommand(cmd);
        }

        /// <inheritdoc/>
        public override void OnExecutedCommand(IDbCommand cmd)
        {
            base.OnExecutedCommand(cmd);
        }

        /// <inheritdoc/>
        public override void OnBeginTransaction()
        {
            Log.Verbose("Transaction Begin");
            base.OnBeginTransaction();
        }

        /// <inheritdoc/>
        public override void OnEndTransaction()
        {
            Log.Verbose("Transaction End");
            base.OnEndTransaction();
        }
    }
}