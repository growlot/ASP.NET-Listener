// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000023_EndpointTriggerType_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000023)]
    public class Migration_202000023_EndpointTriggerType_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var records = new object[]
            {
                new
                {
                    EndpointTriggerTypeId = 1,
                    Key = "Always",
                    Name = "Trigger Always"
                },
                new
                {
                    EndpointTriggerTypeId = 2,
                    Key = "Changed",
                    Name = "When Changed"
                },
                new
                {
                    EndpointTriggerTypeId = 3,
                    Key = "Unchanged",
                    Name = "When Unchanged"
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("EndpointTriggerType").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("EndpointTriggerType")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("EndpointTriggerType").AllRows();
        }
    }
}