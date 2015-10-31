// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000060_FieldConfigurationEntry_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000060)]
    public class Migration_202000060_FieldConfigurationEntry_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FieldConfigurationEntry")
                .WithColumn("FieldConfigurationEntryId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("FieldConfigurationId").AsInt32().NotNullable()
                .WithColumn("FieldName").AsString(100).NotNullable()
                .WithColumn("ValueMapId").AsInt32().Nullable()
                .WithColumn("OutgoingSequence").AsInt16().Nullable()
                .WithColumn("IncomingSequence").AsInt16().Nullable()
                .WithColumn("IncludeInSummary").AsBoolean().NotNullable()
                .WithColumn("MapToName").AsString(100).Nullable();

            this.Create.ForeignKey("FK_FielConfEntr_FielConf")
                .FromTable("FieldConfigurationEntry").ForeignColumn("FieldConfigurationId")
                .ToTable("FieldConfiguration").PrimaryColumn("FieldConfigurationId");

            this.Create.ForeignKey("FK_FielConfEntr_ValuMap")
                .FromTable("FieldConfigurationEntry").ForeignColumn("ValueMapId")
                .ToTable("ValueMap").PrimaryColumn("ValueMapId");

            this.Create.UniqueConstraint("IX_FielConfEntr_FielName")
                .OnTable("FieldConfigurationEntry").Columns("FieldConfigurationId", "FieldName");
        }
    }
}