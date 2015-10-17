// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000022_EndpointTriggerType_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000022)]
    public class Migration_202000022_EndpointTriggerType_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EndpointTriggerType")
               .WithColumn("EndpointTriggerTypeId").AsInt32().NotNullable().PrimaryKey()
               .WithColumn("Name").AsString(15).NotNullable().Unique("UX_EndpTrigType_Key")
               .WithColumn("Description").AsString(100).NotNullable();
        }
    }
}