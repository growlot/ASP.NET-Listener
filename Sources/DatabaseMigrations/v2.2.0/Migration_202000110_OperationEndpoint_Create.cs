// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000110_OperationEndpoint_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000110)]
    public class Migration_202000110_OperationEndpoint_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("OperationEndpoint")
                .WithColumn("OperationEndpointId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("EntityCategoryOperationId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_OperEndp_EntCatOper", "EntityCategoryOperation", "EntityCategoryOperationId")
                .WithColumn("EndpointId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_OperEndp_Endp", "Endpoint", "EndpointId");
        }
    }
}