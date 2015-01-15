//-----------------------------------------------------------------------
// <copyright file="Migration_19_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
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
    [Migration(19)]
    public class Migration_19_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            if (((string)this.ApplicationContext).Contains("Alliant"))
            {               
                Insert.IntoTable("Company").Row(new { CompanyId = "1", ExternalCode = "W", InternalCode = "Wisconsin", Name = "WPL" });
                Insert.IntoTable("Company").Row(new { CompanyId = "2", ExternalCode = "I", InternalCode = "Iowa", Name = "IPL" });

                Update.Table("EquipmentType").Set(new { ExternalCode = "MR" }).Where(new { EquipmentTypeId = "1" });
            }

            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                Insert.IntoTable("Company").Row(new { CompanyId = "0", Name = "KCP&L" });
                Insert.IntoTable("Company").Row(new { CompanyId = "1", Name = "KCP&L GMO" });
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