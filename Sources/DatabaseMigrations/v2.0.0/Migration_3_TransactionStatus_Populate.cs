//-----------------------------------------------------------------------
// <copyright file="Migration_3_TransactionStatus_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(3)]
    public class Migration_3_TransactionStatus_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionStatus").Row(new { TransactionStatusId = "0", Description = "Success" });
            this.Insert.IntoTable("TransactionStatus").Row(new { TransactionStatusId = "1", Description = "In Progress" });
            this.Insert.IntoTable("TransactionStatus").Row(new { TransactionStatusId = "2", Description = "Failed" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionStatus").AllRows();
        }
    }
}
