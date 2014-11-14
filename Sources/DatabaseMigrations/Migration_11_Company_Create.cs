//-----------------------------------------------------------------------
// <copyright file="Migration_11_Company_Create.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(11)]
    public class Migration_11_Company_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            Create.Table("Company")
                .WithColumn("CompanyId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("ExternalCode").AsString(50).Nullable()
                .WithColumn("InternalCode").AsString(50).Nullable()
                .WithColumn("Name").AsString(50).NotNullable().Unique("IX_Comp_Name");
        }
    }
}
