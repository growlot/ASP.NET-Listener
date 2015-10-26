// <copyright file="Migration_202000130_TransactionMessageData_Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000130)]
    public class Migration_202000130_TransactionMessageData_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("TransactionMessageData")
                .WithColumn("RecordKey").AsString(60).NotNullable().Indexed()
                .WithColumn("MessageData").AsString(int.MaxValue).Nullable();
        }
    }
}