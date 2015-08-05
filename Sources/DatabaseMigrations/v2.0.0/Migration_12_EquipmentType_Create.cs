//-----------------------------------------------------------------------
// <copyright file="Migration_12_EquipmentType_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(12)]
    public class Migration_12_EquipmentType_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EquipmentType")
                .WithColumn("EquipmentTypeId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("ServiceTypeId").AsInt32().NotNullable()
                .WithColumn("ExternalCode").AsString(50).NotNullable()
                .WithColumn("InternalCode").AsString(50).NotNullable()
                .WithColumn("Description").AsString(50).NotNullable().Unique("IX_EquiType_Description");

            this.Create.ForeignKey("FK_EquiType_ServType")
                .FromTable("EquipmentType").ForeignColumn("ServiceTypeId")
                .ToTable("ServiceType").PrimaryColumn("ServiceTypeId");

            this.Create.Index("IX_EquiType_STI_EC")
                .OnTable("EquipmentType")
                    .OnColumn("ServiceTypeId").Ascending()
                    .OnColumn("ExternalCode").Ascending().WithOptions().Unique();

            this.Create.Index("IX_EquiType_STI_IC")
                .OnTable("EquipmentType")
                    .OnColumn("ServiceTypeId").Ascending()
                    .OnColumn("InternalCode").Ascending().WithOptions().Unique();
        }
    }
}