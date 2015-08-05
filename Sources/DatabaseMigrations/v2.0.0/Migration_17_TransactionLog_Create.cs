//-----------------------------------------------------------------------
// <copyright file="Migration_17_TransactionLog_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(17)]
    public class Migration_17_TransactionLog_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.IfDatabase("sqlserver", "oracle12c")
                .Create.Table("TransactionLog")
                    .WithColumn("TransactionLogId").AsInt32().NotNullable().PrimaryKey().Identity();

            this.IfDatabase("oracle")
                .Create.Table("TransactionLog")
                    .WithColumn("TransactionLogId").AsInt32().NotNullable().PrimaryKey();

            this.Alter.Table("TransactionLog")
                .AddColumn("ExternalId").AsString(50).Nullable()
                .AddColumn("DeviceId").AsInt32().Nullable()
                .AddColumn("DeviceTestId").AsInt32().Nullable()
                .AddColumn("BatchId").AsInt32().Nullable()
                .AddColumn("TransactionTypeId").AsInt32().NotNullable()
                .AddColumn("TransactionStatusId").AsInt32().NotNullable()
                .AddColumn("TransactionSourceId").AsInt32().NotNullable()
                .AddColumn("Message").AsString().Nullable()
                .AddColumn("DebugInfo").AsString().Nullable();

            this.Create.ForeignKey("FK_TranLog_Devi")
                .FromTable("TransactionLog").ForeignColumn("DeviceId")
                .ToTable("Device").PrimaryColumn("DeviceId");

            this.Create.ForeignKey("FK_TranLog_DeviTest")
                .FromTable("TransactionLog").ForeignColumn("DeviceTestId")
                .ToTable("DeviceTest").PrimaryColumn("DeviceTestId");

            this.Create.ForeignKey("FK_TranLog_Batc")
                .FromTable("TransactionLog").ForeignColumn("BatchId")
                .ToTable("Batch").PrimaryColumn("BatchId");

            this.Create.ForeignKey("FK_TranLog_TranType")
                .FromTable("TransactionLog").ForeignColumn("TransactionTypeId")
                .ToTable("TransactionType").PrimaryColumn("TransactionTypeId");

            this.Create.ForeignKey("FK_TranLog_TranStatus")
                .FromTable("TransactionLog").ForeignColumn("TransactionStatusId")
                .ToTable("TransactionStatus").PrimaryColumn("TransactionStatusId");

            this.Create.ForeignKey("FK_TranLog_TranSource")
                .FromTable("TransactionLog").ForeignColumn("TransactionSourceId")
                .ToTable("TransactionSource").PrimaryColumn("TransactionSourceId");
        }
    }
}