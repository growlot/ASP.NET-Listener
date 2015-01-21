//-----------------------------------------------------------------------
// <copyright file="Migration_200001012_TransactionType_Update.cs" company="Advanced Metering Services LLC">
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
    public class Migration_200001012_TransactionType_Update : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Alter.Table("TransactionType")
                .AddColumn("TransactionCompletionId").AsInt32().NotNullable().WithDefaultValue(1);

            Create.ForeignKey("FK_TranType_TranComp")
                .FromTable("TransactionType").ForeignColumn("TransactionCompletionId")
                .ToTable("TransactionCompletion").PrimaryColumn("TransactionCompletionId");
        }
    }
}