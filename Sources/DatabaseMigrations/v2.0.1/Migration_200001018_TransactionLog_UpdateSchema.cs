//-----------------------------------------------------------------------
// <copyright file="Migration_200001018_TransactionLog_UpdateSchema.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001018)]
    public class Migration_200001018_TransactionLog_UpdateSchema : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Alter.Table("TransactionLog")
                .AddColumn("DeviceBatchId").AsInt32().Nullable();
            
            Create.ForeignKey("FK_TranLog_DeviBatc")
                .FromTable("TransactionLog").ForeignColumn("DeviceBatchId")
                .ToTable("DeviceBatch").PrimaryColumn("DeviceBatchId");
        }
    }
}