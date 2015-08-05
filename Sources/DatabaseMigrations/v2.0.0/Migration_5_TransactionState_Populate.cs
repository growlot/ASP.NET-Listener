//-----------------------------------------------------------------------
// <copyright file="Migration_5_TransactionState_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(5)]
    public class Migration_5_TransactionState_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "1", Description = "Listener client start" });
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "2", Description = "Listener client send message" });
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "3", Description = "Listener service receive call" });
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "4", Description = "Listener service send message" });
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "5", Description = "Listener client end" });
            this.Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "6", Description = "Listener service end" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionState").AllRows();
        }
    }
}