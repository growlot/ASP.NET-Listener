//-----------------------------------------------------------------------
// <copyright file="Migration_202000013_FileAlignMode_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(202000013)]
    public class Migration_202000013_FileAlignMode_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            this.Create.Table("FileAlignMode")
                .WithColumn("FileAlignModeId").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Description").AsString(50).NotNullable().Unique("IX_AligMode_Description");
        }
    }
}