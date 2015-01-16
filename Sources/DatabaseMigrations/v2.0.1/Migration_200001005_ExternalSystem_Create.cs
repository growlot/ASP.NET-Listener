//-----------------------------------------------------------------------
// <copyright file="Migration_200001005_ExternalSystem_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001005)]
    public class Migration_200001005_ExternalSystem_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Create.Table("ExternalSystem")
                .WithColumn("ExternalSystemId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable().Unique("IX_ExteSyst_Name")
                .WithColumn("Description").AsString(500).Nullable();
        }
    }
}