// <copyright file="Migration_202000096_EntityCategoryOperation_Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000096)]
    public class Migration_202000096_EntityCategoryOperation_Create : AutoReversingMigration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Create.Table("EntityCategoryOperation")
                .WithColumn("EntityCategoryOperationId").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("EntityCategoryId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_EntiCateOper_EntiCate", "EntityCategory", "EntityCategoryId")
                .WithColumn("EnabledOperationId")
                .AsInt32()
                .NotNullable()
                .ForeignKey("FK_EntiCateOper_EnOper", "EnabledOperation", "EnabledOperationId")
                .WithColumn("FieldConfigurationId")
                .AsInt32()
                .Nullable()
                .ForeignKey("FK_EntiCateOper_FielConf", "FieldConfiguration", "FieldConfigurationId");
        }
    }
}