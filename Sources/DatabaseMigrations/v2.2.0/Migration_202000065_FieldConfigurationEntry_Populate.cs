// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000065_FieldConfigurationEntry_Populate.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000065)]
    public class Migration_202000065_FieldConfigurationEntry_Populate : Migration
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
                    FieldConfigurationEntryId = 1,
                    FieldConfigurationId = 1,
                    FieldName = "UserName",
                    ValueMapId = 1,
                    OutgoingSequence = 1,
                    IncludeInSummary = true,
                    IncomingSequence = (short?)null,
                    MapToName = (string)null
                },
                new
                {
                    FieldConfigurationEntryId = 2,
                    FieldConfigurationId = 1,
                    FieldName = "EntityKey",
                    ValueMapId = (int?)null,
                    OutgoingSequence = 2,
                    IncludeInSummary = true,
                    IncomingSequence = 1,
                    MapToName = (string)null
                }
            };

            foreach (var record in records)
            {
                this.IfSqlServer().Insert.IntoTable("FieldConfigurationEntry").WithIdentityInsert()
                    .Row(record);

                this.IfOracle().Insert.IntoTable("FieldConfigurationEntry")
                    .Row(record);
            }
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Delete.FromTable("FieldConfigurationEntry").AllRows();
        }
    }
}