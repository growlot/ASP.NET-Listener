// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000024_EntityCategory_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000024)]
    public class Migration_202000024_EntityCategory_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EntityCategory")
                .WithColumn("EntityCategoryId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Key").AsString(50).NotNullable().Unique("UX_EntityCategory_Key")
                .WithColumn("DisplayName").AsString(100).NotNullable();
        }
    }
}