//-----------------------------------------------------------------------
// <copyright file="Migration_200001012_TransactionLog_UpdateSchema.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001012)]
    public class Migration_200001012_TransactionLog_UpdateSchema : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Delete.ForeignKey("FK_TranLog_TranSource").OnTable("TransactionLog");
            Delete.Column("TransactionSourceId").FromTable("TransactionLog");

            Alter.Table("TransactionLog")
                .AddColumn("DeviceBatchId").AsInt32().Nullable()
                .AddColumn("DataHash").AsAnsiString(40).Nullable();

            Create.ForeignKey("FK_TranLog_TranType")
                .FromTable("TransactionLog").ForeignColumn("TransactionTypeId")
                .ToTable("TransactionType").PrimaryColumn("TransactionTypeId");

            Create.ForeignKey("FK_TranLog_DeviBatc")
                .FromTable("TransactionLog").ForeignColumn("DeviceBatchId")
                .ToTable("DeviceBatch").PrimaryColumn("DeviceBatchId");
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            // not available
        }
    }
}
