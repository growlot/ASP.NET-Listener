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
                .WithColumn("TransactionId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Key").AsString(60).NotNullable()
                .WithColumn("ApplicationId").AsInt32().NotNullable()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("OperationId").AsInt32().NotNullable()
                .WithColumn("TransactionStatusId").AsInt32().NotNullable()
                .WithColumn("EntityCategoryId").AsInt32().Nullable()
                .WithColumn("EntityKey").AsString(50).Nullable()
                .WithColumn("User").AsString(100).Nullable()
                .WithColumn("Data").AsString(int.MaxValue).NotNullable()
                .WithColumn("Message").AsString(255).Nullable()
                .WithColumn("Details").AsString(int.MaxValue).Nullable()
                .WithColumn("CreatedDateTime").AsDateTime().NotNullable()
                .WithColumn("UpdatedDateTime").AsDateTime().Nullable();

            this.Create.ForeignKey("FK_TransactionRegistry_Application")
                .FromTable("TransactionRegistry").ForeignColumn("ApplicationId")
                .ToTable("Application").PrimaryColumn("ApplicationId");

            this.Create.ForeignKey("FK_TransactionRegistry_Company")
                .FromTable("TransactionRegistry").ForeignColumn("CompanyId")
                .ToTable("Company").PrimaryColumn("CompanyId");

            this.Create.ForeignKey("FK_TransactionRegistry_Operation")
                .FromTable("TransactionRegistry").ForeignColumn("OperationId")
                .ToTable("Operation").PrimaryColumn("OperationId");

            this.Create.ForeignKey("FK_TransactionRegistry_TransactionStatus")
                .FromTable("TransactionRegistry").ForeignColumn("TransactionStatusId")
                .ToTable("TransactionStatus").PrimaryColumn("TransactionStatusId");

            this.Create.ForeignKey("FK_TransactionRegistry_EntityCategory")
                .FromTable("TransactionRegistry").ForeignColumn("EntityCategoryId")
                .ToTable("EntityCategory").PrimaryColumn("EntityCategoryId");
        }
    }
}