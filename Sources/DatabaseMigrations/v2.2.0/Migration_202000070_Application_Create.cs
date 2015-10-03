// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000070_Application_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000070)]
    public class Migration_202000070_Application_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("Application")
                .WithColumn("ApplicationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable().Unique("IX_Application_Name")
                .WithColumn("Key").AsString(60).NotNullable().Unique("IX_Application_Key");
        }
    }
}