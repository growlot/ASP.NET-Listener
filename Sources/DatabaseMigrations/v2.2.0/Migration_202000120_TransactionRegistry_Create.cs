﻿// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000120_TransactionRegistry_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000120)]
    public class Migration_202000120_TransactionRegistry_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("TransactionRegistry")
                .WithColumn("TransactionId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ParentTransactionId").AsInt32().Nullable()
                .WithColumn("Key").AsString(60).NotNullable()
                .WithColumn("EnabledOperationId").AsInt32().NotNullable()
                .WithColumn("TransactionStatusId").AsInt32().NotNullable()
                .WithColumn("Header").AsString(int.MaxValue).Nullable()
                .WithColumn("Data").AsString(int.MaxValue).Nullable()
                .WithColumn("Summary").AsXml().Nullable()
                .WithColumn("User").AsString(100).Nullable()
                .WithColumn("TransactionHash").AsString(50).Nullable()
                .WithColumn("Message").AsString(255).Nullable()
                .WithColumn("Details").AsString(int.MaxValue).Nullable()
                .WithColumn("CreatedDateTime").AsDateTime().NotNullable()
                .WithColumn("UpdatedDateTime").AsDateTime().Nullable();

            this.Create.ForeignKey("FK_TransactionRegistry_TransactionStatus")
                .FromTable("TransactionRegistry").ForeignColumn("TransactionStatusId")
                .ToTable("TransactionStatus").PrimaryColumn("TransactionStatusId");

            this.Create.ForeignKey("FK_TransactionRegistry_EnabledOperation")
                .FromTable("TransactionRegistry").ForeignColumn("EnabledOperationId")
                .ToTable("EnabledOperation").PrimaryColumn("EnabledOperationId");

            this.Create.ForeignKey("FK_TransactionRegistry_Parent")
                .FromTable("TransactionRegistry").ForeignColumn("ParentTransactionId")
                .ToTable("TransactionRegistry").PrimaryColumn("TransactionId");
        }
    }
}