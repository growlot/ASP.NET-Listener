// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000105_Endpoint_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000105)]
    public class Migration_202000105_Endpoint_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var record = new
            {
                EndpointId = 1,
                Name = "Generic Jms Endpoint",
                ProtocolTypeId = 1,
                ConnectionConfiguration = "{\"Host\":\"localhost\", \"Port\":7001, \"QueueName\":\"jms/AMSIntegration\", \"UserName\":\"ams\", \"Password\":\"Password1\"}",
                EndpointTriggerTypeId = 2,
                AdapterConfiguration = "{\"MessageTypeTemplate\":\"{Data.EntityCategory}:{Data.OperationKey}\"}"
            };

            this.IfSqlServer().Insert.IntoTable("Endpoint").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("Endpoint")
                .Row(record);
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("Endpoint").AllRows();
        }
    }
}