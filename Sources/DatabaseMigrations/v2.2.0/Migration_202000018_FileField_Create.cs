// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000018_FileField_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000018)]
    public class Migration_202000018_FileField_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FileField")
                .WithColumn("FileFieldId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FileId").AsInt32().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Index").AsInt32().NotNullable()
                .WithColumn("FieldType").AsString(20).NotNullable()
                .WithColumn("FileConverterId").AsInt32().Nullable()
                .WithColumn("NullValue").AsString(50).Nullable()
                .WithColumn("TrimChars").AsString(10).Nullable()
                .WithColumn("FileTrimModeId").AsInt32().Nullable()
                .WithColumn("Description").AsString(250).Nullable()
                .WithColumn("AllignChar").AsString(1).Nullable()
                .WithColumn("FileAlignModeId").AsInt32().Nullable()
                .WithColumn("Length").AsInt32().Nullable()
                .WithColumn("IsQuoted").AsBoolean().Nullable()
                .WithColumn("QuoteChar").AsString(1).Nullable()
                .WithColumn("FileQuoteModeId").AsInt32().Nullable()
                .WithColumn("FileQuoteMultilineId").AsInt32().Nullable();

            this.Create.ForeignKey("FK_FileFiel_FileConv")
                .FromTable("FileField").ForeignColumn("FileConverterId")
                .ToTable("FileConverter").PrimaryColumn("FileConverterId");

            this.Create.ForeignKey("FK_FileFiel_TrimMode")
                .FromTable("FileField").ForeignColumn("FileTrimModeId")
                .ToTable("FileTrimMode").PrimaryColumn("FileTrimModeId");

            this.Create.ForeignKey("FK_FileFiel_File")
                .FromTable("FileField").ForeignColumn("FileId")
                .ToTable("File").PrimaryColumn("FileId");

            this.Create.ForeignKey("FK_FileFiel_AligMode")
                .FromTable("FileField").ForeignColumn("FileAlignModeId")
                .ToTable("FileAlignMode").PrimaryColumn("FileAlignModeId");

            this.Create.ForeignKey("FK_FileFiel_QuotMode")
                .FromTable("FileField").ForeignColumn("FileQuoteModeId")
                .ToTable("FileQuoteMode").PrimaryColumn("FileQuoteModeId");

            this.Create.ForeignKey("FK_FileFiel_QuotMult")
                .FromTable("FileField").ForeignColumn("FileQuoteMultilineId")
                .ToTable("FileQuoteMultiline").PrimaryColumn("FileQuoteMultilineId");
        }
    }
}