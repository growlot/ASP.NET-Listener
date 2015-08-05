//-----------------------------------------------------------------------
// <copyright file="Migration_13_EquipmentType_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(13)]
    public class Migration_13_EquipmentType_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("EquipmentType").Row(new { EquipmentTypeId = "1", ServiceTypeId = "1", ExternalCode = "EM", InternalCode = "EM", Description = "Electric Meter" });
            this.Insert.IntoTable("EquipmentType").Row(new { EquipmentTypeId = "2", ServiceTypeId = "1", ExternalCode = "CT", InternalCode = "CT", Description = "Current Transformer" });
            this.Insert.IntoTable("EquipmentType").Row(new { EquipmentTypeId = "3", ServiceTypeId = "1", ExternalCode = "PT", InternalCode = "PT", Description = "Potential Transformer" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("ServiceType").AllRows();
        }
    }
}