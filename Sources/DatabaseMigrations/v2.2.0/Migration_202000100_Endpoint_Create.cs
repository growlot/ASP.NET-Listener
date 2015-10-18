// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000100_Endpoint_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000100)]
    public class Migration_202000100_Endpoint_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("Endpoint")
                .WithColumn("EndpointId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ProtocolTypeId").AsInt32().NotNullable().ForeignKey("FK_Endp_ProtType", "ProtocolType", "ProtocolTypeId")
                .WithColumn("ConnectionConfiguration").AsString().NotNullable()
                .WithColumn("AdapterConfiguration").AsString().Nullable()
                .WithColumn("EndpointTriggerTypeId").AsInt32().NotNullable().ForeignKey("FK_Endp_EndpTrigType", "EndpointTriggerType", "EndpointTriggerTypeId");
        }
    }
}