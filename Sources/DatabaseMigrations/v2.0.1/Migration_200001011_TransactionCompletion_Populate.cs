//-----------------------------------------------------------------------
// <copyright file="Migration_200001011_TransactionCompletion_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001011)]
    public class Migration_200001011_TransactionCompletion_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Insert.IntoTable("TransactionCompletion").Row(new { TransactionCompletionId = "1", Description = "Complete after call" });
            Insert.IntoTable("TransactionCompletion").Row(new { TransactionCompletionId = "2", Description = "Wait for callback" });
            Insert.IntoTable("TransactionCompletion").Row(new { TransactionCompletionId = "3", Description = "Call to check" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            Delete.FromTable("TransactionCompletion").AllRows();
        }
    }
}