// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000045_FieldConfiguration_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000045)]
    public class Migration_202000045_FieldConfiguration_Populate : Migration
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
                    FieldConfigurationId = 1,
                    CompanyId = 0,
                    Name = "Default [Install Meter] Field Configuration"
                },
                new
                {
                    FieldConfigurationId = 2,
                    CompanyId = 0,
                    Name = "Batch root field configuration"
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("FieldConfiguration").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("FieldConfiguration")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("FieldConfiguration").AllRows();
        }
    }
}