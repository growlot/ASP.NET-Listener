// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000120_TransactionRegistry_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

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
                .WithColumn("EntityCategoryOperationId").AsInt32().NotNullable()
                .WithColumn("TransactionStatusId").AsInt32().NotNullable()
                .WithColumn("Priority").AsInt32().Nullable()
                .WithColumn("IncomingHash").AsString(50).NotNullable().Indexed()
                .WithColumn("Data").AsString(int.MaxValue).Nullable()
                .WithColumn("Summary").AsXml().Nullable()
                .WithColumn("AppUser").AsString(100).Nullable()
                .WithColumn("OutgoingHash").AsString(50).Nullable().Indexed()
                .WithColumn("Message").AsString(255).Nullable()
                .WithColumn("Details").AsString(int.MaxValue).Nullable()
                .WithColumn("CreatedDateTime").AsDateTime().NotNullable()
                .WithColumn("UpdatedDateTime").AsDateTime().Nullable();

            // this.Create.PrimaryKey("PK_TranRegi").OnTable("TransactionRegistry").Columns("TransactionId", "RecordKey");
            this.Create.ForeignKey("FK_TranRegi_TranStat")
                .FromTable("TransactionRegistry").ForeignColumn("TransactionStatusId")
                .ToTable("TransactionStatus").PrimaryColumn("TransactionStatusId");

            this.Create.ForeignKey("FK_TranRegi_EnabOper")
                .FromTable("TransactionRegistry").ForeignColumn("EntityCategoryOperationId")
                .ToTable("EntityCategoryOperation").PrimaryColumn("EntityCategoryOperationId");

            // this.Create.ForeignKey("FK_TranRegi_Pare")
            //    .FromTable("TransactionRegistry").ForeignColumn("BatchKey")
            //    .ToTable("TransactionRegistry").PrimaryColumn("RecordKey");
            // this.Create.PrimaryKey("FK_Tran_Reg_Prim").OnTable("TransactionRegistry").Columns("RecordKey", "BatchKey").NonClustered();
            this.IfSqlServer().Execute.Sql(@"CREATE NONCLUSTERED INDEX IX_BatchKey
                ON TransactionRegistry (BatchKey)
                WHERE BatchKey IS NOT NULL");
        }
    }
}