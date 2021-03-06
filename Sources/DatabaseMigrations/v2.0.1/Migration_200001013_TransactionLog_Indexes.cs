﻿//-----------------------------------------------------------------------
// <copyright file="Migration_200001013_TransactionLog_Indexes.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001013)]
    public class Migration_200001013_TransactionLog_Indexes : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Index("IX_TranLog_DeviTypeStat")
                .OnTable("TransactionLog")
                .OnColumn("DeviceId").Ascending()
                .OnColumn("TransactionTypeId").Ascending()
                .OnColumn("TransactionStatusId").Ascending();

            this.Create.Index("IX_TranLog_TestTypeStat")
                .OnTable("TransactionLog")
                .OnColumn("DeviceTestId").Ascending()
                .OnColumn("TransactionTypeId").Ascending()
                .OnColumn("TransactionStatusId").Ascending();
        }
    }
}