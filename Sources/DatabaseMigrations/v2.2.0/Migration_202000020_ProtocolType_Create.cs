// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000020_ProtocolType_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using System.Data;
    using FluentMigrator;
    using FluentMigrator.Expressions;
    using FluentMigrator.Model;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000020)]
    public class Migration_202000020_ProtocolType_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("ProtocolType")
               .WithColumn("ProtocolTypeId").AsInt32().NotNullable().PrimaryKey().Identity()
               .WithColumn("Name").AsString(10).NotNullable().Unique("UX_ProtType_Key")
               .WithColumn("Description").AsString(100).NotNullable();
        }
    }
}