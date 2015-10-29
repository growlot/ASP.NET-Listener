// //-----------------------------------------------------------------------
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
                .WithColumn("TransactionId").AsInt32().NotNullable().Identity().Indexed()
                .WithColumn("BatchKey").AsGuid().Nullable()
                .WithColumn("RecordKey").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("EnabledOperationId").AsInt32().NotNullable()
                .WithColumn("TransactionStatusId").AsInt32().NotNullable()
                .WithColumn("TransactionKey").AsString(255).NotNullable().Indexed()
                .WithColumn("Data").AsString(int.MaxValue).NotNullable()
                .WithColumn("Summary").AsXml().Nullable()
                .WithColumn("AppUser").AsString(100).Nullable()
                .WithColumn("TransactionHash").AsString(50).Nullable().Indexed()
                .WithColumn("Message").AsString(255).Nullable()
                .WithColumn("Details").AsString(int.MaxValue).Nullable()
                .WithColumn("CreatedDateTime").AsDateTime().NotNullable()
                .WithColumn("UpdatedDateTime").AsDateTime().Nullable();

            // this.Create.PrimaryKey("PK_TranRegi").OnTable("TransactionRegistry").Columns("TransactionId", "RecordKey");
            this.Create.ForeignKey("FK_TranRegi_TranStat")
                .FromTable("TransactionRegistry").ForeignColumn("TransactionStatusId")
                .ToTable("TransactionStatus").PrimaryColumn("TransactionStatusId");

            this.Create.ForeignKey("FK_TranRegi_EnabOper")
                .FromTable("TransactionRegistry").ForeignColumn("EnabledOperationId")
                .ToTable("EnabledOperation").PrimaryColumn("EnabledOperationId");

            this.Create.ForeignKey("FK_TranRegi_Pare")
                .FromTable("TransactionRegistry").ForeignColumn("BatchKey")
                .ToTable("TransactionRegistry").PrimaryColumn("RecordKey");
        }
    }
}