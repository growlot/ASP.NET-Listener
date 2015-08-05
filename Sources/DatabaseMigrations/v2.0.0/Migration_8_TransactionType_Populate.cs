//-----------------------------------------------------------------------
// <copyright file="Migration_8_TransactionType_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(8)]
    public class Migration_8_TransactionType_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionType").Row(new { TransactionTypeId = "100", Description = "Device Shop Test" });
            this.Insert.IntoTable("TransactionType").Row(new { TransactionTypeId = "400", Description = "Device Retrieve" });
            this.Insert.IntoTable("TransactionType").Row(new { TransactionTypeId = "701", Description = "Device Create External" });
            this.Insert.IntoTable("TransactionType").Row(new { TransactionTypeId = "801", Description = "Device Update External" });
            this.Insert.IntoTable("TransactionType").Row(new { TransactionTypeId = "1200", Description = "System Table Validate" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionType").AllRows();
        }
    }
}