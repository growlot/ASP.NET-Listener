//-----------------------------------------------------------------------
// <copyright file="Migration_202000004_FileConverterKind_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000004)]
    public class Migration_202000004_FileConverterKind_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FileConverterKind")
                .WithColumn("FileConverterKindId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Description").AsString(50).NotNullable().Unique("IX_ConvKind_Description");
        }
    }
}