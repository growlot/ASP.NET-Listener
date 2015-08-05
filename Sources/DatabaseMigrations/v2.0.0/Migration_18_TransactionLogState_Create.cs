//-----------------------------------------------------------------------
// <copyright file="Migration_18_TransactionLogState_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(18)]
    public class Migration_18_TransactionLogState_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.IfDatabase("sqlserver", "oracle12c")
                .Create.Table("TransactionLogState")
                    .WithColumn("TransactionLogStateId").AsInt32().NotNullable().PrimaryKey().Identity();

            this.IfDatabase("oracle")
                .Create.Table("TransactionLogState")
                    .WithColumn("TransactionLogStateId").AsInt32().NotNullable().PrimaryKey();

            this.Alter.Table("TransactionLogState")
                .AddColumn("TransactionLogId").AsInt32().NotNullable()
                .AddColumn("TransactionStateId").AsInt32().NotNullable()
                .AddColumn("ExecutionTime").AsDateTime().NotNullable();

            this.Create.ForeignKey("FK_TranLState_TranLog")
                .FromTable("TransactionLogState").ForeignColumn("TransactionLogId")
                .ToTable("TransactionLog").PrimaryColumn("TransactionLogId");

            this.Create.ForeignKey("FK_TranLState_TranState")
                .FromTable("TransactionLogState").ForeignColumn("TransactionStateId")
                .ToTable("TransactionState").PrimaryColumn("TransactionStateId");

            this.Create.Index("IX_TranLState_TLI_TSI")
                .OnTable("TransactionLogState")
                    .OnColumn("TransactionLogId").Ascending()
                    .OnColumn("TransactionStateId").Ascending().WithOptions().Unique();
        }
    }
}