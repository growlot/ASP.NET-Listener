// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000055_ValueMapEntry_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000055)]
    public class Migration_202000055_ValueMapEntry_Populate : Migration
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
                    ValueMapEntryId = 1,
                    ValueMapId = 1,
                    Key = string.Empty,
                    Value = "ListenerUser"
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("ValueMapEntry").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("ValueMapEntry")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("ValueMapEntry").AllRows();
        }
    }
}