// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000021_ProtocolType_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000021)]
    public class Migration_202000021_ProtocolType_Populate : Migration
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
                    ProtocolTypeId = 1,
                    Name = "jms",
                    Description = "Java Message Service"
                }
             };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("ProtocolType").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("ProtocolType")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("ProtocolType").AllRows();
        }
    }
}