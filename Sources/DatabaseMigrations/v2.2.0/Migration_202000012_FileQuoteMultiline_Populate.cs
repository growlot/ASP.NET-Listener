// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000012_FileQuoteMultiline_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
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
            this.Insert.IntoTable("FileQuoteMultiline")
                .Row(new { FileQuoteMultilineId = "0", Description = "AllowForBoth" });
            this.Insert.IntoTable("FileQuoteMultiline")
                .Row(new { FileQuoteMultilineId = "1", Description = "AllowForRead" });
            this.Insert.IntoTable("FileQuoteMultiline")
                .Row(new { FileQuoteMultilineId = "2", Description = "AllowForWrite" });
            this.Insert.IntoTable("FileQuoteMultiline")
                .Row(new { FileQuoteMultilineId = "3", Description = "NotAllow" });
        }
    }
}