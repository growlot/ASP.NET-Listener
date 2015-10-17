// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000050_ValueMapEntry_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000050)]
    public class Migration_202000050_ValueMapEntry_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("ValueMapEntry")
                .WithColumn("ValueMapEntryId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ValueMapId").AsInt32().NotNullable()
                .WithColumn("RecordKey").AsString(100).NotNullable()
                .WithColumn("Value").AsString(100).Nullable();

            this.Create.ForeignKey("FK_ValuMapEntr_ValuMap")
                .FromTable("ValueMapEntry").ForeignColumn("ValueMapId")
                .ToTable("ValueMap").PrimaryColumn("ValueMapId");

            this.Create.UniqueConstraint("IX_ValuMapEntr_Key").OnTable("ValueMapEntry").Columns("ValueMapId", "RecordKey");
        }
    }
}