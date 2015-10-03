// //-----------------------------------------------------------------------
// // <copyright file="Utilities.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Builders.IfDatabase;
    using FluentMigrator.Builders.Insert;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// DB migration utilities
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Check if targeted DB is SQL Server
        /// </summary>
        /// <param name="migrationBase">The migration base.</param>
        /// <returns>IIfDatabaseExpressionRoot.</returns>
        public static IIfDatabaseExpressionRoot IfSqlServer(this MigrationBase migrationBase)
        {
            return migrationBase.IfDatabase("sqlserver");
        }

        /// <summary>
        /// Check if targeted DB is Oracle
        /// </summary>
        /// <param name="migrationBase">The migration base.</param>
        /// <returns>IIfDatabaseExpressionRoot.</returns>
        public static IIfDatabaseExpressionRoot IfOracle(this MigrationBase migrationBase)
        {
            return migrationBase.IfDatabase("oracle");
        }
    }
}