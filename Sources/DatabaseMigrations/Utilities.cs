// //-----------------------------------------------------------------------
// // <copyright file="Utilities.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using FluentMigrator;
    using FluentMigrator.Builders.IfDatabase;

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
            if (migrationBase == null)
            {
                throw new ArgumentNullException("migrationBase", "Can not check if database is MS SQL, becasue migration is not provided.");
            }

            return migrationBase.IfDatabase("sqlserver");
        }

        /// <summary>
        /// Check if targeted DB is Oracle
        /// </summary>
        /// <param name="migrationBase">The migration base.</param>
        /// <returns>IIfDatabaseExpressionRoot.</returns>
        public static IIfDatabaseExpressionRoot IfOracle(this MigrationBase migrationBase)
        {
            if (migrationBase == null)
            {
                throw new ArgumentNullException("migrationBase", "Can not check if database is Oracle, becasue migration is not provided.");
            }

            return migrationBase.IfDatabase("oracle", "Oracle12c");
        }
    }
}