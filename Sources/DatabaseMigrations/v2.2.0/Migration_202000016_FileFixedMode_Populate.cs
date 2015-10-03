﻿// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000016_FileFixedMode_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000016)]
    public class Migration_202000016_FileFixedMode_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "0", Description = "ExactLength" });
            this.Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "1", Description = "AllowMoreChars" });
            this.Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "2", Description = "AllowLessChars" });
            this.Insert.IntoTable("FileFixedMode")
                .Row(new { FileFixedModeId = "3", Description = "AllowVariableLength" });
        }
    }
}