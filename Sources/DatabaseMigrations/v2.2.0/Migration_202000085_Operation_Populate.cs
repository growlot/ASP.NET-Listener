// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000085_Operation_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000085)]
    public class Migration_202000085_Operation_Populate : Migration
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
                    OperationId = 1,
                    Name = "Install",
                    DisplayName = "Install Device"
                },
                new
                {
                    OperationId = 2,
                    Name = "Batch",
                    DisplayName = "Batch processing"
                },
                new
                {
                    OperationId = 3,
                    Name = "Add",
                    DisplayName = "Add Entity"
                },
                new
                {
                    OperationId = 4,
                    Name = "Test",
                    DisplayName = "Test Entity"
                },
                new
                {
                    OperationId = 5,
                    Name = "Update",
                    DisplayName = "Update Entity"
                }
            };
            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("Operation").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("Operation")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("Operation").AllRows();
        }
    }
}