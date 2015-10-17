// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000087_EntityCategoryOperation_Create.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000087)]
    public class Migration_202000087_EntityCategoryOperation_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EntityCategoryOperation")
                .WithColumn("EntityCategoryOperationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("EntityCategoryId").AsInt32().NotNullable().ForeignKey("FK_EntiCateOper_EntiCate", "EntityCategory", "EntityCategoryId")
                .WithColumn("OperationId").AsInt32().NotNullable().ForeignKey("FK_EntiCateOper_Oper", "Operation", "OperationId");
        }
    }
}