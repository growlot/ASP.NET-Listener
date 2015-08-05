//-----------------------------------------------------------------------
// <copyright file="Migration_9_ServiceType_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(9)]
    public class Migration_9_ServiceType_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("ServiceType")
                .WithColumn("ServiceTypeId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("ExternalCode").AsString(50).NotNullable().Unique("IX_ServType_ExternalCode")
                .WithColumn("InternalCode").AsString(50).NotNullable().Unique("IX_ServType_InternalCode")
                .WithColumn("Description").AsString(50).NotNullable().Unique("IX_ServType_Description");
        }
    }
}