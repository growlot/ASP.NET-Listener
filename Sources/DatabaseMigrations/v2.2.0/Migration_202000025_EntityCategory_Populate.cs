// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000025_EntityCategory_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000025)]
    public class Migration_202000025_EntityCategory_Populate : Migration
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
                    EntityCategoryId = 1,
                    Name = "ElectricMeters",
                    DisplayName = "Electric Meters"
                },
                new
                {
                    EntityCategoryId = 2,
                    Name = "Circuits",
                    DisplayName = "Circuits"
                },
                new
                {
                    EntityCategoryId = 3,
                    Name = "Sites",
                    DisplayName = "Sites"
                },
                new
                {
                    EntityCategoryId = 4,
                    Name = "Users",
                    DisplayName = "Users"
                },
                new
                {
                    EntityCategoryId = 5,
                    Name = "Vehicles",
                    DisplayName = "Vehicles"
                },
                new
                {
                    EntityCategoryId = 6,
                    Name = "Batch",
                    DisplayName = "Batch"
                },
                new
                {
                    EntityCategoryId = 7,
                    Name = "MeterTest",
                    DisplayName = "Meter Test"
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("EntityCategory").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("EntityCategory")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("DeviceCategory").AllRows();
        }
    }
}