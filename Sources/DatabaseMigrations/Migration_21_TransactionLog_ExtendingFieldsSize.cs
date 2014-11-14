//-----------------------------------------------------------------------
// <copyright file="Migration_21_TransactionLog_ExtendingFieldsSize.cs" company="Advanced Metering Services LLC">
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
    [Migration(21)]
    public class Migration_21_TransactionLog_ExtendingFieldsSize : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Alter.Table("TransactionLog")
                .AlterColumn("Message").AsString(1000).Nullable()
                .AlterColumn("DebugInfo").AsString(Int32.MaxValue).Nullable();
        }
    }
}