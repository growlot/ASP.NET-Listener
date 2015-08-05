//-----------------------------------------------------------------------
// <copyright file="Migration_200001002_TransactionData_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001002)]
    public class Migration_200001002_TransactionData_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionData").Row(new { TransactionDataId = "1", Description = "Device" });
            this.Insert.IntoTable("TransactionData").Row(new { TransactionDataId = "2", Description = "Device Test" });
            this.Insert.IntoTable("TransactionData").Row(new { TransactionDataId = "3", Description = "Barcode" });
            this.Insert.IntoTable("TransactionData").Row(new { TransactionDataId = "4", Description = "New Batch" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionData").AllRows();
        }
    }
}
