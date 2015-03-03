//-----------------------------------------------------------------------
// <copyright file="Migration_200001010_TransactionType_Recreate.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001010)]
    public class Migration_200001010_TransactionType_Recreate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Delete.ForeignKey("FK_TranLog_TranType").OnTable("TransactionLog");
            Delete.Table("TransactionType");

            Create.Table("TransactionType")
                .WithColumn("TransactionTypeId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable().Unique("IX_TranType_Name")
                .WithColumn("TransactionDataId").AsInt32().NotNullable()
                .WithColumn("TransactionSourceId").AsInt32().NotNullable()
                .WithColumn("TransactionDirectionId").AsInt32().NotNullable()
                .WithColumn("TransactionCompletionId").AsInt32().NotNullable().WithDefaultValue(1)
                .WithColumn("ExternalSystemId").AsInt32().Nullable()
                .WithColumn("Description").AsString(500).Nullable();

            Create.ForeignKey("FK_TranType_TranData")
                .FromTable("TransactionType").ForeignColumn("TransactionDataId")
                .ToTable("TransactionData").PrimaryColumn("TransactionDataId");

            Create.ForeignKey("FK_TranType_TranSource")
                .FromTable("TransactionType").ForeignColumn("TransactionSourceId")
                .ToTable("TransactionSource").PrimaryColumn("TransactionSourceId");

            Create.ForeignKey("FK_TranType_TranDire")
                .FromTable("TransactionType").ForeignColumn("TransactionDirectionId")
                .ToTable("TransactionDirection").PrimaryColumn("TransactionDirectionId");

            Create.ForeignKey("FK_TranType_TranComp")
                .FromTable("TransactionType").ForeignColumn("TransactionCompletionId")
                .ToTable("TransactionCompletion").PrimaryColumn("TransactionCompletionId");

            Create.ForeignKey("FK_TranType_ExteSyst")
                .FromTable("TransactionType").ForeignColumn("ExternalSystemId")
                .ToTable("ExternalSystem").PrimaryColumn("ExternalSystemId");
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            // Not available.
        }
    }
}
