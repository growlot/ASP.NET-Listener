//-----------------------------------------------------------------------
// <copyright file="Migration_200001017_DeviceBatch_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001017)]
    public class Migration_200001017_DeviceBatch_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "oracle12c")
                .Create.Table("DeviceBatch")
                    .WithColumn("DeviceBatchId").AsInt32().NotNullable().PrimaryKey().Identity();

            IfDatabase("oracle")
                .Create.Table("DeviceBatch")
                    .WithColumn("DeviceBatchId").AsInt32().NotNullable().PrimaryKey();

            Alter.Table("DeviceBatch")
                .AddColumn("BatchNumber").AsString(10).NotNullable().Unique("IX_DeviceBatch_BatchNumber")
                .AddColumn("ExternalId").AsString(50).Nullable().Unique("IX_DeviceBatch_ExternalId");
        }
    }
}
