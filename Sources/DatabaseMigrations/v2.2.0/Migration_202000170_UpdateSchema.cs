// <copyright file="Migration_202000170_UpdateSchema.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000170)]
    public class Migration_202000170_UpdateSchema : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Execute.Sql("ALTER TABLE FieldConfigurationEntry DROP CONSTRAINT  FK_FielConfEntr_FielConf");

            this.Create.ForeignKey("FK_FielConfEntr_FielConf")
                .FromTable("FieldConfigurationEntry").ForeignColumn("FieldConfigurationId")
                .ToTable("FieldConfiguration")
                .PrimaryColumn("FieldConfigurationId")
                .OnDeleteOrUpdate(System.Data.Rule.Cascade);

            this.Execute.Sql("ALTER TABLE ValueMapEntry DROP CONSTRAINT FK_ValuMapEntr_ValuMap");

            this.Create.ForeignKey("FK_ValuMapEntr_ValuMap")
                .FromTable("ValueMapEntry").ForeignColumn("ValueMapId")
                .ToTable("ValueMap").PrimaryColumn("ValueMapId").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            this.Alter.Table("ValueMapEntry").AlterColumn("RecordKey").AsString(100).Nullable();
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Execute.Sql("ALTER TABLE FieldConfigurationEntry DROP CONSTRAINT FK_FielConfEntr_FielConf");

            this.Create.ForeignKey("FK_FielConfEntr_FielConf")
               .FromTable("FieldConfigurationEntry").ForeignColumn("FieldConfigurationId")
               .ToTable("FieldConfiguration").PrimaryColumn("FieldConfigurationId");

            this.Execute.Sql("ALTER TABLE ValueMapEntry DROP CONSTRAINT FK_ValuMapEntr_ValuMap");

            this.Create.ForeignKey("FK_ValuMapEntr_ValuMap")
               .FromTable("ValueMapEntry").ForeignColumn("ValueMapId")
               .ToTable("ValueMap").PrimaryColumn("ValueMapId");

            this.Alter.Table("ValueMapEntry").AlterColumn("RecordKey").AsString(100).NotNullable();
        }
    }
}