//-----------------------------------------------------------------------
// <copyright file="Migration_10_ServiceType_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(10)]
    public class Migration_10_ServiceType_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("ServiceType").Row(new { ServiceTypeId = "1", ExternalCode = "E", InternalCode = "E", Description = "Electric" });
            this.Insert.IntoTable("ServiceType").Row(new { ServiceTypeId = "2", ExternalCode = "G", InternalCode = "G", Description = "Gas" });
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