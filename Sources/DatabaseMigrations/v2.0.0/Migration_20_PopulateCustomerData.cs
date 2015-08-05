//-----------------------------------------------------------------------
// <copyright file="Migration_20_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(20)]
    public class Migration_20_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            if (((string)this.ApplicationContext).Contains("Alliant"))
            {
                this.Update.Table("Company").Set(new { InternalCode = "1" }).Where(new { ExternalCode = "W" });
                this.Update.Table("Company").Set(new { InternalCode = "2" }).Where(new { ExternalCode = "I" });
            }

            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                this.Update.Table("Company").Set(new { InternalCode = "0" }).Where(new { CompanyId = "0" });
                this.Update.Table("Company").Set(new { InternalCode = "1" }).Where(new { CompanyId = "1" });
            }

            if (((string)this.ApplicationContext).Contains("Concord"))
            {
                this.Insert.IntoTable("Company").Row(new { CompanyId = "0", ExternalCode = "CCD", InternalCode = "0", Name = "Concord" });
            }

            if (((string)this.ApplicationContext).Contains("LabTrack"))
            {
                this.Update.Table("EquipmentType").Set(new { ExternalCode = "EMT" }).Where(new { EquipmentTypeId = "1" });
                this.Update.Table("EquipmentType").Set(new { ExternalCode = "VT" }).Where(new { EquipmentTypeId = "3" });
            }
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
        }
    }
}