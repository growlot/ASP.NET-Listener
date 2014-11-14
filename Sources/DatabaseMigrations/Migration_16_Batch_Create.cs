//-----------------------------------------------------------------------
// <copyright file="Migration_16_Batch_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(16)]
    public class Migration_16_Batch_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "oracle12c")
                .Create.Table("Batch")
                    .WithColumn("BatchId").AsInt32().NotNullable().PrimaryKey().Identity();

            IfDatabase("oracle")
                .Create.Table("Batch")
                    .WithColumn("BatchId").AsInt32().NotNullable().PrimaryKey();

            Alter.Table("Batch")
                .AddColumn("TotalChunks").AsInt32().NotNullable()
                .AddColumn("LatestChunk").AsInt32().NotNullable();
        }
    }
}