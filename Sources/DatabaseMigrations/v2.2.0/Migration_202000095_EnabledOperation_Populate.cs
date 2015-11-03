// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000095_EnabledOperation_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000095)]
    public class Migration_202000095_EnabledOperation_Populate : Migration
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
                    EnabledOperationId = 1,
                    ApplicationId = 1,
                    CompanyId = 0,
                    OperationId = 1
                },
                new
                {
                    EnabledOperationId = 2,
                    ApplicationId = 1,
                    CompanyId = 0,
                    OperationId = 2
                },
                new
                {
                    EnabledOperationId = 3,
                    ApplicationId = 1,
                    CompanyId = 0,
                    OperationId = 3
                }
            };
            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("EnabledOperation").WithIdentityInsert()
                                    .Row(record);

                this.IfOracle().Insert.IntoTable("EnabledOperation")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("EnabledOperation").AllRows();
        }
    }
}