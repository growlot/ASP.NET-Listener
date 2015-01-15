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
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "1", Description = "Listener client start" });
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "2", Description = "Listener client send message" });
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "3", Description = "Listener service receive call" });
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "4", Description = "Listener service send message" });
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "5", Description = "Listener client end" });
            Insert.IntoTable("TransactionState").Row(new { TransactionStateId = "6", Description = "Listener service end" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            Delete.FromTable("TransactionState").AllRows();
        }
    }
}