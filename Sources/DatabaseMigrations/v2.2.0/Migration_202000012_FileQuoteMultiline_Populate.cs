//-----------------------------------------------------------------------
// <copyright file="Migration_202000012_FileQuoteMultiline_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000012)]
    public class Migration_202000012_FileQuoteMultiline_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("FileQuoteMultiline").Row(new { FileQuoteMultilineId = "0", Description = "AllowForBoth" });
            Insert.IntoTable("FileQuoteMultiline").Row(new { FileQuoteMultilineId = "1", Description = "AllowForRead" });
            Insert.IntoTable("FileQuoteMultiline").Row(new { FileQuoteMultilineId = "2", Description = "AllowForWrite" });
            Insert.IntoTable("FileQuoteMultiline").Row(new { FileQuoteMultilineId = "3", Description = "NotAllow" });
        }
    }
}