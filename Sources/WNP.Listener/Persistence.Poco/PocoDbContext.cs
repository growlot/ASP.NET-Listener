// <copyright file="PocoDbContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Poco
{
    using System.Data;
    using System.Data.Common;
    using AsyncPoco;
    using Serilog;

    /// <summary>
    /// POCO db context
    /// </summary>
    public class PocoDbContext : Database
    {
        //public ListenerDbContext(IDbConnection connection) : base(connection)
        //{
        //}

        public PocoDbContext(
            string connectionString,
            string providerName)
            : base(connectionString, providerName)
        {
        }

        public PocoDbContext(
            string connectionString,
            DbProviderFactory provider)
            : base(connectionString, provider)
        {
        }

        public PocoDbContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public override void OnExecutingCommand(
            IDbCommand cmd)
        {
            Log.Information(cmd.CommandText);
            base.OnExecutingCommand(cmd);
        }
    }
}