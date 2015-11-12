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
            var records = new object[]
            {
                new
                {
                    OperationEndpointId = 1,
                    EntityCategoryOperationId = 1,
                    EndpointId = 1
                },
                new
                {
                    OperationEndpointId = 2,
                    EntityCategoryOperationId = 2,
                    EndpointId = 1
                },
                new
                {
                    OperationEndpointId = 3,
                    EntityCategoryOperationId = 3,
                    EndpointId = 1
                },
                new
                {
                    OperationEndpointId = 4,
                    EntityCategoryOperationId = 4,
                    EndpointId = 1
                },
                new
                {
                    OperationEndpointId = 5,
                    EntityCategoryOperationId = 5,
                    EndpointId = 1
                },

                // No mapping for batch operation @6
                new
                {
                    OperationEndpointId = 7,
                    EntityCategoryOperationId = 7,
                    EndpointId = 1
                },
                new
                {
                    OperationEndpointId = 8,
                    EntityCategoryOperationId = 8,
                    EndpointId = 1
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("OperationEndpoint").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("OperationEndpoint")
                    .Row(record);
            }
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