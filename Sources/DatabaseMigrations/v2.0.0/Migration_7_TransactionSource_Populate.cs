//-----------------------------------------------------------------------
// <copyright file="Migration_7_TransactionSource_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(7)]
    public class Migration_7_TransactionSource_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Insert.IntoTable("TransactionSource").Row(new { TransactionSourceId = "0", Description = "WNP" });
            this.Insert.IntoTable("TransactionSource").Row(new { TransactionSourceId = "1", Description = "Web Srvice Call" });
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("TransactionSource").AllRows();
        }
    }
}