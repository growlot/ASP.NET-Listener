// //-----------------------------------------------------------------------
// // <copyright file="ListenerDbContext.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Data;
    using System.Data.Common;
    using System.Threading.Tasks;
    using AsyncPoco;
    using Serilog;

    public class ListenerDbContext : Database
    {
        //public ListenerDbContext(IDbConnection connection) : base(connection)
        //{
        //}

        public ListenerDbContext(string connectionString, string providerName) : base(connectionString, providerName)
        {
        }

        public ListenerDbContext(string connectionString, DbProviderFactory provider) : base(connectionString, provider)
        {
        }

        public ListenerDbContext(string connectionStringName) : base(connectionStringName)
        {
        }

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            Log.Information(cmd.CommandText);
            base.OnExecutingCommand(cmd);
        }
    }
}