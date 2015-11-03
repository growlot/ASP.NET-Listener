// <copyright file="Migration_202000097_EntityCategoryOperation_Populate.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000097)]
    public class Migration_202000097_EntityCategoryOperation_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var records = new[]
            {
                new
                {
                    EntityCategoryOperationId = 1,
                    EntityCategoryId = 1,
                    EnabledOperationId = 1,
                    FieldConfigurationId = (int?)1
                },
                new
                {
                    EntityCategoryOperationId = 2,
                    EntityCategoryId = 2,
                    EnabledOperationId = 3,
                    FieldConfigurationId = (int?)1
                },
                new
                {
                    EntityCategoryOperationId = 3,
                    EntityCategoryId = 3,
                    EnabledOperationId = 3,
                    FieldConfigurationId = (int?)1
                },
                new
                {
                    EntityCategoryOperationId = 4,
                    EntityCategoryId = 4,
                    EnabledOperationId = 3,
                    FieldConfigurationId = (int?)1
                },
                new
                {
                    EntityCategoryOperationId = 5,
                    EntityCategoryId = 5,
                    EnabledOperationId = 3,
                    FieldConfigurationId = (int?)1
                },
                new
                {
                    EntityCategoryOperationId = 6,
                    EntityCategoryId = 6,
                    EnabledOperationId = 2,
                    FieldConfigurationId = (int?)null
                },
                new
                {
                    EntityCategoryOperationId = 7,
                    EntityCategoryId = 7,
                    EnabledOperationId = 3,
                    FieldConfigurationId = (int?)1
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("EntityCategoryOperation").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("EntityCategoryOperation")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("EntityCategoryOperation").AllRows();
        }
    }
}