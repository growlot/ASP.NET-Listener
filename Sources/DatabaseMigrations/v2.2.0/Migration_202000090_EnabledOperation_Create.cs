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
                .WithColumn("OperationId").AsInt32().NotNullable();

            this.Create.ForeignKey("FK_EnabledOperation_Application")
                .FromTable("EnabledOperation").ForeignColumn("ApplicationId")
                .ToTable("Application").PrimaryColumn("ApplicationId");

            this.Create.ForeignKey("FK_EnabledOperation_Company")
                .FromTable("EnabledOperation").ForeignColumn("CompanyId")
                .ToTable("Company").PrimaryColumn("CompanyId");

            this.Create.ForeignKey("FK_EnabledOperation_Operation")
                .FromTable("EnabledOperation").ForeignColumn("OperationId")
                .ToTable("Operation").PrimaryColumn("OperationId");

            this.Create.UniqueConstraint("UX_EnabledOperation")
                .OnTable("EnabledOperation").Columns("ApplicationId", "CompanyId", "OperationId");
        }
    }
}