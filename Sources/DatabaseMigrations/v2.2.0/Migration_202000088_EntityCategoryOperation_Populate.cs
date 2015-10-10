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
            var record = new
            {
                EntityCategoryOperationId = 1,
                EntityCategoryId = 1,
                OperationId = 1
            };

            this.IfSqlServer().Insert.IntoTable("EntityCategoryOperation").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("EntityCategoryOperation")
                .Row(record);
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