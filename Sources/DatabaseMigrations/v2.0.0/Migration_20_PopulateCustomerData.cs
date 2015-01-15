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
                Update.Table("Company").Set(new { InternalCode = "1" }).Where(new { ExternalCode = "W" });
                Update.Table("Company").Set(new { InternalCode = "2" }).Where(new { ExternalCode = "I" });
            }

            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                Update.Table("Company").Set(new { InternalCode = "0" }).Where(new { CompanyId = "0" });
                Update.Table("Company").Set(new { InternalCode = "1" }).Where(new { CompanyId = "1" });
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