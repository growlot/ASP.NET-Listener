// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000075_Application_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000075)]
    public class Migration_202000075_Application_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var record = new
            {
                ApplicationId = 1,
                Name = "WNP",
                Key = "dde3ff6d-e368-4427-b75e-6ec47183f88e"
            };

            this.IfSqlServer().Insert.IntoTable("Application").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("Application")
                .Row(record);
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("Application").AllRows();
        }
    }
}