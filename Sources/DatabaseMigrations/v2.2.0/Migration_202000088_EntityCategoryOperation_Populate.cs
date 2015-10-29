// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000088_EntityCategoryOperation_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000088)]
    public class Migration_202000088_EntityCategoryOperation_Populate : Migration
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
                    OperationId = 1
                },
                new
                {
                    EntityCategoryOperationId = 2,
                    EntityCategoryId = 2,
                    OperationId = 3
                },
                new
                {
                    EntityCategoryOperationId = 3,
                    EntityCategoryId = 3,
                    OperationId = 3
                },
                new
                {
                    EntityCategoryOperationId = 4,
                    EntityCategoryId = 4,
                    OperationId = 3
                },
                new
                {
                    EntityCategoryOperationId = 5,
                    EntityCategoryId = 5,
                    OperationId = 3
                },
                new
                {
                    EntityCategoryOperationId = 6,
                    EntityCategoryId = 6,
                    OperationId = 2
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