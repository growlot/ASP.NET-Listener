//-----------------------------------------------------------------------
// <copyright file="Migration_22_TransactionLog_AddStartEndTimes.cs" company="Advanced Metering Services LLC">
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
    [Migration(22)]
    public class Migration_22_TransactionLog_AddStartEndTimes : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Alter.Table("TransactionLog")
                .AddColumn("TransactionStart").AsDateTime().NotNullable().SetExistingRowsTo(DateTime.Now)
                .AddColumn("TransactionEnd").AsDateTime().Nullable();

            Execute.Sql(@"
                UPDATE TransactionLog 
                    SET TransactionStart = 
	                    (
		                    SELECT TOP(1) ExecutionTime 
		                    FROM TransactionLogState
		                    WHERE TransactionLog.TransactionLogId = TransactionLogId
		                    ORDER BY ExecutionTime ASC
	                    )");

            Execute.Sql(@"
                UPDATE TransactionLog 
                    SET TransactionEnd = 
	                    (
		                    SELECT TOP(1) ExecutionTime 
		                    FROM TransactionLogState
		                    WHERE TransactionLog.TransactionLogId = TransactionLogId
		                    ORDER BY ExecutionTime DESC
	                    )
	                WHERE TransactionStatusId != 1");
        }

        /// <summary>
        /// Reverts the database migration 
        /// </summary>
        public override void Down()
        {
            Delete
                .Column("TransactionStart")
                .Column("TransactionEnd")
                .FromTable("TransactionLog");
        }
    }
}