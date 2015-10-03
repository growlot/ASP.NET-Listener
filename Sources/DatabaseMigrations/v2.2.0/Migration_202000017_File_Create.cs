﻿// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000017_File_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000017)]
    public class Migration_202000017_File_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("File")
                .WithColumn("FileId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ExternalSystemId").AsInt32().NotNullable()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("FileFixedModeId").AsInt32().Nullable()
                .WithColumn("Delimiter").AsString(2).Nullable()
                .WithColumn("System").AsBoolean().NotNullable();

            this.Create.ForeignKey("FK_File_FixeMode")
                .FromTable("File").ForeignColumn("FileFixedModeId")
                .ToTable("FileFixedMode").PrimaryColumn("FileFixedModeId");

            this.Create.ForeignKey("FK_File_ExteSyst")
                .FromTable("File").ForeignColumn("ExternalSystemId")
                .ToTable("ExternalSystem").PrimaryColumn("ExternalSystemId");
        }
    }
}