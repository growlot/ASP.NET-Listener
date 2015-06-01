//-----------------------------------------------------------------------
// <copyright file="Migration_202000008_FileTrimMode_Populate.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000008)]
    public class Migration_202000008_FileTrimMode_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("FileTrimMode").Row(new { FileTrimModeId = "0", Description = "None" });
            Insert.IntoTable("FileTrimMode").Row(new { FileTrimModeId = "1", Description = "Both" });
            Insert.IntoTable("FileTrimMode").Row(new { FileTrimModeId = "2", Description = "Left" });
            Insert.IntoTable("FileTrimMode").Row(new { FileTrimModeId = "3", Description = "Right" });
        }
    }
}