// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000010_FileQuoteMode_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000010)]
    public class Migration_202000010_FileQuoteMode_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("FileQuoteMode").Row(new { FileQuoteModeId = "0", Description = "AlwaysQuoted" });
            this.Insert.IntoTable("FileQuoteMode").Row(new { FileQuoteModeId = "1", Description = "OptionalForRead" });
            this.Insert.IntoTable("FileQuoteMode").Row(new { FileQuoteModeId = "2", Description = "OptionalForWrite" });
            this.Insert.IntoTable("FileQuoteMode").Row(new { FileQuoteModeId = "3", Description = "OptionalForBoth" });
        }
    }
}