//-----------------------------------------------------------------------
// <copyright file="Migration_200001003_TransactionDirection_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001003)]
    public class Migration_200001003_TransactionDirection_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("TransactionDirection")
                .WithColumn("TransactionDirectionId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Description").AsString(50).NotNullable().Unique("IX_TranDire_Description");
        }
    }
}