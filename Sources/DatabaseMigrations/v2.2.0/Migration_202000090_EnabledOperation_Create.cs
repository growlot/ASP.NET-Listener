// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000090_EnabledOperation_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000090)]
    public class Migration_202000090_EnabledOperation_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EnabledOperation")
                .WithColumn("EnabledOperationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("OperationId").AsInt32().NotNullable()
                .WithColumn("FieldConfigurationId").AsInt32().Nullable().ForeignKey("FK_EnabOper_FielConf", "FieldConfiguration", "FieldConfigurationId");

            this.Create.ForeignKey("FK_EnabOper_Appl")
                .FromTable("EnabledOperation").ForeignColumn("ApplicationId")
                .ToTable("Application").PrimaryColumn("ApplicationId");

            this.Create.ForeignKey("FK_EnabOper_Comp")
                .FromTable("EnabledOperation").ForeignColumn("CompanyId")
                .ToTable("Company").PrimaryColumn("CompanyId");

            this.Create.ForeignKey("FK_EnabOper_Oper")
                .FromTable("EnabledOperation").ForeignColumn("OperationId")
                .ToTable("Operation").PrimaryColumn("OperationId");

            this.Create.UniqueConstraint("UX_EnabOper")
                .OnTable("EnabledOperation").Columns("ApplicationId", "CompanyId", "OperationId");
        }
    }
}