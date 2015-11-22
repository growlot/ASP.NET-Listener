﻿namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Data;
    using System.Data.Common;
    using Poco;
    using Serilog;

    public class ListenerDbContext : PocoDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="providerName">Name of the provider.</param>
        public ListenerDbContext(string connectionString,
            string providerName)
            : base(connectionString, providerName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerDbContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="provider">The provider.</param>
        public ListenerDbContext(string connectionString,
            DbProviderFactory provider)
            : base(connectionString, provider)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        public ListenerDbContext(string connectionStringName)
            : base(connectionStringName)
        {
        }
    }
}
