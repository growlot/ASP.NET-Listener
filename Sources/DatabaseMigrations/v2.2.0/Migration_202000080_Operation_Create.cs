// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000080_Operation_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000080)]
    public class Migration_202000080_Operation_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("Operation")
                .WithColumn("OperationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("DisplayName").AsString(100).NotNullable();
        }
    }
}