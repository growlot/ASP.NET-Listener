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
                    Key = "ElectricMeters",
                    DisplayName = "Electric Meters"
                },
                new
                {
                    EntityCategoryId = 2,
                    Key = "Circuits",
                    DisplayName = "Circuits"
                },
                new
                {
                    EntityCategoryId = 3,
                    Key = "Sites",
                    DisplayName = "Sites"
                },
                new
                {
                    EntityCategoryId = 4,
                    Key = "Users",
                    DisplayName = "Users"
                },
                new
                {
                    EntityCategoryId = 5,
                    Key = "Vehicles",
                    DisplayName = "Vehicles"
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