// <copyright file="Migration_202000160_AddOperation_StateChange.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000160)]
    public class Migration_202000160_AddOperation_StateChange : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var record = new
            {
                OperationId = 6,
                Name = "StateChange",
                DisplayName = "Change device state"
            };

            this.IfSqlServer().Insert.IntoTable("Operation").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("Operation")
                .Row(record);

            var enabledOperation = new
            {
                EnabledOperationId = 6,
                ApplicationId = 1,
                CompanyId = 0,
                OperationId = 6
            };

            this.IfSqlServer().Insert.IntoTable("EnabledOperation").WithIdentityInsert()
                .Row(enabledOperation);

            this.IfOracle().Insert.IntoTable("EnabledOperation")
                .Row(enabledOperation);

            var entityCategoryEnabledOperation = new
            {
                EntityCategoryOperationId = 10,
                EntityCategoryId = 1,
                EnabledOperationId = 6,
                FieldConfigurationId = (int?)1
            };

            this.IfSqlServer().Insert.IntoTable("EntityCategoryOperation").WithIdentityInsert()
                .Row(entityCategoryEnabledOperation);

            this.IfOracle().Insert.IntoTable("EntityCategoryOperation")
                .Row(entityCategoryEnabledOperation);

            var operationEndpoint = new
            {
                OperationEndpointId = 9,
                EntityCategoryOperationId = 10,
                EndpointId = 1
            };

            this.IfSqlServer().Insert.IntoTable("OperationEndpoint").WithIdentityInsert()
                .Row(operationEndpoint);

            this.IfOracle().Insert.IntoTable("OperationEndpoint")
                .Row(operationEndpoint);
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("OperationEndpoint").Row(new
            {
                OperationEndpointId = 9
            });

            this.Delete.FromTable("EntityCategoryOperation").Row(new
            {
                EntityCategoryOperationId = 10
            });

            this.Delete.FromTable("EnabledOperation").Row(new
            {
                EnabledOperationId = 6
            });

            this.Delete.FromTable("Operation").Row(new
            {
                OperationId = 6
            });
        }
    }
}