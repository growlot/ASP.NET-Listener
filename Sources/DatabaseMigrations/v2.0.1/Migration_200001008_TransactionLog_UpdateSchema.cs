//-----------------------------------------------------------------------
// <copyright file="Migration_200001008_TransactionLog_UpdateSchema.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001008)]
    public class Migration_200001008_TransactionLog_UpdateSchema : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Delete.ForeignKey("FK_TranLog_TranSource").OnTable("TransactionLog");
            Delete.Column("TransactionSourceId").FromTable("TransactionLog");

            Create.ForeignKey("FK_TranLog_TranType")
                .FromTable("TransactionLog").ForeignColumn("TransactionTypeId")
                .ToTable("TransactionType").PrimaryColumn("TransactionTypeId");
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
