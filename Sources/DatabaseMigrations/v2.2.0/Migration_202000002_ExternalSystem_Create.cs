﻿// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000002_ExternalSystem_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000002)]
    public class Migration_202000002_ExternalSystem_Create : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("ExternalSystem")
                .Row(new { Name = "WecoMobile", Description = "Weco Mobile application" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("ExternalSystem").Row(new { Name = "WecoMobile" });
        }
    }
}