﻿// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000014_FileAlignMode_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000014)]
    public class Migration_202000014_FileAlignMode_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("FileAlignMode").Row(new { FileAlignModeId = "0", Description = "Left" });
            this.Insert.IntoTable("FileAlignMode").Row(new { FileAlignModeId = "1", Description = "Center" });
            this.Insert.IntoTable("FileAlignMode").Row(new { FileAlignModeId = "2", Description = "Right" });
        }
    }
}