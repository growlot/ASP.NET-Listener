// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000115_OperationEndpoint_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000115)]
    public class Migration_202000115_OperationEndpoint_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var record = new
            {
                OperationEndpointId = 1,
                EnabledOperationId = 1,
                EndpointId = 1
            };

            this.IfSqlServer().Insert.IntoTable("OperationEndpoint").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("OperationEndpoint")
                .Row(record);
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("OperationEndpoint").AllRows();
        }
    }
}