// <copyright file="WnpAsyncDbContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNPAsync
{
    using System.Data;
    using System.Data.Common;
    using Poco;
    using Serilog;

    /// <summary>
    /// WnpAsync db context
    /// </summary>
    public class WnpAsyncDbContext : PocoDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WnpAsyncDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="providerName">Name of the provider.</param>
        public WnpAsyncDbContext(string connectionString,
            string providerName)
            : base(connectionString, providerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WnpAsyncDbContext" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="provider">The provider.</param>
        public WnpAsyncDbContext(string connectionString,
                    DbProviderFactory provider)
                    : base(connectionString, provider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WnpAsyncDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public WnpAsyncDbContext(string connectionStringName)
            : base(connectionStringName)
        {
        }

        /// <summary>
        /// Called when [executing command].
        /// </summary>
        /// <param name="cmd">The command.</param>
        public override void OnExecutingCommand(IDbCommand cmd)
        {
            Log.Information(cmd.CommandText);
            base.OnExecutingCommand(cmd);
        }
    }
}