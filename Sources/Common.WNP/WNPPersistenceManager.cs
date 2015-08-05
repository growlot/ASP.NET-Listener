//-----------------------------------------------------------------------
// <copyright file="WNPPersistenceManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Reflection;
    using AMSLLC.Listener.Common;

    /// <summary>
    /// Persistence manager for WNP.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "False positive as described in http://stackoverflow.com/questions/8925925/code-analysis-ca1063-fires-when-deriving-from-idisposable-and-providing-implemen")]
    public class WNPPersistenceManager : PersistenceManager, IPersistenceManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WNPPersistenceManager"/> class.
        /// </summary>
        public WNPPersistenceManager()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPPersistenceManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public WNPPersistenceManager(string connectionString)
            : this(connectionString, typeof(WNPPersistenceManager).Assembly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPPersistenceManager"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="assembly">The assembly with additional mappings.</param>
        private WNPPersistenceManager(string connectionString, Assembly assembly)
            : base(connectionString, assembly)
        {
        }
    }
}
