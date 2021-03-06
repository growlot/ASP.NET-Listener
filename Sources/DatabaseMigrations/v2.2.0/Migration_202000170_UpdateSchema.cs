﻿// <copyright file="Migration_202000170_UpdateSchema.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

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

            this.Execute.Sql("ALTER TABLE OperationEndpoint DROP CONSTRAINT  FK_OperEndp_EntCatOper");

            this.Create.ForeignKey("FK_OperEndp_EntCatOper")
                .FromTable("OperationEndpoint").ForeignColumn("EntityCategoryOperationId")
                .ToTable("EntityCategoryOperation").PrimaryColumn("EntityCategoryOperationId").OnDeleteOrUpdate(System.Data.Rule.Cascade);

            this.Alter.Table("Endpoint")
                .AddColumn("CompanyId")
                .AsInt32().Nullable()
                .ForeignKey("FK_Endp_Comp", "Company", "CompanyId");

            this.Update.Table("Endpoint").Set(new { CompanyId = 0 }).AllRows();

            this.Alter.Table("Endpoint").AlterColumn("CompanyId").AsInt32().NotNullable();

            this.Alter.Table("EntityCategoryOperation")
                .AddColumn("CompanyId")
                .AsInt32().Nullable()
                .ForeignKey("FK_EnCaOp_Comp", "Company", "CompanyId");

            this.Update.Table("EntityCategoryOperation").Set(new { CompanyId = 0 }).AllRows();

            this.Alter.Table("EntityCategoryOperation").AlterColumn("CompanyId").AsInt32().NotNullable();

            this.Alter.Table("EntityCategoryOperation")
                .AddColumn("OperationTransactionKey")
                .AsGuid()
                .NotNullable()
                .WithDefault(SystemMethods.NewGuid);

            this.Alter.Table("EntityCategoryOperation")
                .AddColumn("OperationTransactionName")
                .AsString()
                .Nullable();

            this.Alter.Table("EntityCategoryOperation")
                .AddColumn("AutoSucceed")
                .AsBoolean()
                .NotNullable().WithDefaultValue(false);

            this.Insert.IntoTable("EntityCategoryOperation").Row(new
            {
                EntityCategoryOperationId = 11,
                EntityCategoryId = 1,
                EnabledOperationId = 4,
                FieldConfigurationId = 1,
                CompanyId = 0,
                OperationTransactionName = "Test As Left",
                AutoSucceed = false
            }).WithIdentityInsert();

            this.Update.Table("EntityCategoryOperation").Set(new { OperationTransactionName = "Test As Found" }).Where(new { EntityCategoryOperationId = 7 });
            this.Update.Table("EntityCategoryOperation").Set(new { AutoSucceed = 1 }).Where(new { EntityCategoryOperationId = 1 });

            var record = new
            {
                EndpointId = 2,
                Name = "Test Jms Endpoint",
                ProtocolTypeId = 1,
                ConnectionConfiguration = "{\"Host\":\"localhost\", \"Port\":7001, \"QueueName\":\"jms/AMSIntegration\", \"UserName\":\"ams\", \"Password\":\"Password1\"}",
                EndpointTriggerTypeId = 2,
                AdapterConfiguration = "{\"MessageTypeTemplate\":\"{Data.EntityCategory}:{Data.OperationKey}:AsLeft\"}",
                CompanyId = 0
            };

            this.IfSqlServer().Insert.IntoTable("Endpoint").WithIdentityInsert()
                .Row(record);

            this.Insert.IntoTable("OperationEndpoint").Row(new
            {
                OperationEndpointId = 10,
                EntityCategoryOperationId = 11,
                EndpointId = 2
            }).WithIdentityInsert();
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

            this.Execute.Sql("ALTER TABLE OperationEndpoint DROP CONSTRAINT  FK_OperEndp_EntCatOper");

            this.Create.ForeignKey("FK_OperEndp_EntCatOper")
                .FromTable("OperationEndpoint").ForeignColumn("EntityCategoryOperationId")
                .ToTable("EntityCategoryOperation").PrimaryColumn("EntityCategoryOperationId");

            this.Execute.Sql("ALTER TABLE Endpoint DROP CONSTRAINT FK_Endp_Comp");
            this.Execute.Sql("ALTER TABLE EntityCategoryOperation DROP CONSTRAINT FK_EnCaOp_Comp");
        }
    }
}