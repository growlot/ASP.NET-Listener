//-----------------------------------------------------------------------
// <copyright file="Migration_14_Device_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(14)]
    public class Migration_14_Device_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            IfDatabase(new string[] { "sqlserver", "oracle12c" })
                .Create.Table("Device")
                    .WithColumn("DeviceId").AsInt32().NotNullable().PrimaryKey().Identity();

            IfDatabase("oracle")
                .Create.Table("Device")
                    .WithColumn("DeviceId").AsInt32().NotNullable().PrimaryKey();                    
            IfDatabase("oracle").Create.Sequence("hibernate_sequence");

            Alter.Table("Device")
                .AddColumn("ExternalId").AsString(50).Nullable()
                .AddColumn("CompanyId").AsInt32().NotNullable()
                .AddColumn("EquipmentNumber").AsString(20).NotNullable()
                .AddColumn("EquipmentTypeId").AsInt32().NotNullable();

            Create.ForeignKey("FK_Devi_EquiType")
                .FromTable("Device").ForeignColumn("EquipmentTypeId")
                .ToTable("EquipmentType").PrimaryColumn("EquipmentTypeId");

            Create.ForeignKey("FK_Devi_Comp")
                .FromTable("Device").ForeignColumn("CompanyId")
                .ToTable("Company").PrimaryColumn("CompanyId");

            Create.Index("IX_Devi_CI_EN_ETI")
                .OnTable("Device")
                    .OnColumn("CompanyId").Ascending()
                    .OnColumn("EquipmentNumber").Ascending()
                    .OnColumn("EquipmentTypeId").Ascending().WithOptions().Unique();
        }
    }
}
