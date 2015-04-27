//-----------------------------------------------------------------------
// <copyright file="Migration_21_TransactionLog_ExtendingFieldsSize.cs" company="Advanced Metering Services LLC">
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
    [Migration(21)]
    public class Migration_21_TransactionLog_ExtendingFieldsSize : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Delete.Column("DebugInfo").FromTable("TransactionLog");

            Alter.Table("TransactionLog")
                .AlterColumn("Message").AsString(1000).Nullable()
                .AddColumn("DebugInfo").AsString(int.MaxValue).Nullable();
        }

        /// <summary>
        /// Reverts the database migration 
        /// </summary>
        public override void Down()
        {
            Delete
                .Column("Message")
                .Column("DebugInfo")
                .FromTable("TransactionLog");

            Alter.Table("TransactionLog")
                .AddColumn("Message").AsString().Nullable()
                .AddColumn("DebugInfo").AsString().Nullable();
        }
    }
}