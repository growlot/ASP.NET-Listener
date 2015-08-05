//-----------------------------------------------------------------------
// <copyright file="Migration_200001004_TransactionDirection_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001004)]
    public class Migration_200001004_TransactionDirection_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionDirection").Row(new { TransactionDirectionId = "1", Description = "Incoming" });
            this.Insert.IntoTable("TransactionDirection").Row(new { TransactionDirectionId = "2", Description = "Outgoing" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionDirection").AllRows();
        }
    }
}