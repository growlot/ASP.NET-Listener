// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000005_FileConverterKind_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000005)]
    public class Migration_202000005_FileConverterKind_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "0", Description = "None" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "1", Description = "Date" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "2", Description = "Boolean" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "3", Description = "Byte" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "4", Description = "Int16" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "5", Description = "Int32" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "6", Description = "Int64" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "7", Description = "Decimal" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "8", Description = "Double" });
            this.Insert.IntoTable("FileConverterKind")
                .Row(new { FileConverterKindId = "9", Description = "PercentDouble" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "10", Description = "Single" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "11", Description = "SByte" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "12", Description = "UInt16" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "13", Description = "UInt32" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "14", Description = "UInt64" });
            this.Insert.IntoTable("FileConverterKind")
                .Row(new { FileConverterKindId = "15", Description = "DateMultiFormat" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "16", Description = "Char" });
            this.Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "17", Description = "Guid" });
        }
    }
}