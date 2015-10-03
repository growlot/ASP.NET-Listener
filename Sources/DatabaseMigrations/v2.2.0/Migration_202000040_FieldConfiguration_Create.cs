// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000040_FieldConfiguration_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000040)]
    public class Migration_202000040_FieldConfiguration_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FieldConfiguration")
                .WithColumn("FieldConfigurationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("CompanyId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_FieldConfiguration_CompanyId", "Company", "CompanyId")
                .WithColumn("Name").AsString(50).NotNullable().Unique("IX_FieldConfiguration_Name");
        }
    }
}