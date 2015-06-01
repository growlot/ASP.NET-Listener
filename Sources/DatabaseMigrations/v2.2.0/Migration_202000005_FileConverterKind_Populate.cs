//-----------------------------------------------------------------------
// <copyright file="Migration_202000005_FileConverterKind_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000005)]
    public class Migration_202000005_FileConverterKind_Populate : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "0", Description = "None" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "1", Description = "Date" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "2", Description = "Boolean" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "3", Description = "Byte" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "4", Description = "Int16" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "5", Description = "Int32" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "6", Description = "Int64" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "7", Description = "Decimal" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "8", Description = "Double" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "9", Description = "PercentDouble" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "10", Description = "Single" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "11", Description = "SByte" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "12", Description = "UInt16" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "13", Description = "UInt32" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "14", Description = "UInt64" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "15", Description = "DateMultiFormat" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "16", Description = "Char" });
            Insert.IntoTable("FileConverterKind").Row(new { FileConverterKindId = "17", Description = "Guid" });
        }
    }
}