//-----------------------------------------------------------------------
// <copyright file="Migration_202000006_FileConverter_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000006)]
    public class Migration_202000006_FileConverter_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FileConverter")
                .WithColumn("FileConverterId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Argument1").AsString(50).Nullable()
                .WithColumn("Argument2").AsString(50).Nullable()
                .WithColumn("Argument3").AsString(50).Nullable()
                .WithColumn("FileConverterKindId").AsInt32().NotNullable();

            this.Create.ForeignKey("FK_FileConv_ConvType")
                .FromTable("FileConverter").ForeignColumn("FileConverterKindId")
                .ToTable("FileConverterKind").PrimaryColumn("FileConverterKindId");
        }
    }
}