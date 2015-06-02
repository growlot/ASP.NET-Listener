//-----------------------------------------------------------------------
// <copyright file="Migration_202000016_FileFixedMode_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000016)]
    public class Migration_202000016_FileFixedMode_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "0", Description = "ExactLength" });
            Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "1", Description = "AllowMoreChars" });
            Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "2", Description = "AllowLessChars" });
            Insert.IntoTable("FileFixedMode").Row(new { FileFixedModeId = "3", Description = "AllowVariableLength" });
        }
    }
}