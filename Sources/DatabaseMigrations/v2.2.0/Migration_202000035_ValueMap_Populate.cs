// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000035_ValueMap_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000035)]
    public class Migration_202000035_ValueMap_Populate : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            var record = new { ValueMapId = 1, CompanyId = 0, ValueType = "integer", Name = "Default [UserName] map" };

            this.IfSqlServer().Insert.IntoTable("ValueMap").WithIdentityInsert()
                .Row(record);

            this.IfOracle().Insert.IntoTable("ValueMap")
                .Row(record);
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("ValueMap").AllRows();
        }
    }
}