//-----------------------------------------------------------------------
// <copyright file="Migration_15_DeviceTest_Create.cs" company="Advanced Metering Services LLC">
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
    [Migration(15)]
    public class Migration_15_DeviceTest_Create : AutoReversingMigration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "oracle12c")
                .Create.Table("DeviceTest")
                    .WithColumn("DeviceTestId").AsInt32().NotNullable().PrimaryKey().Identity();

            IfDatabase("oracle")
                .Create.Table("DeviceTest")
                    .WithColumn("DeviceTestId").AsInt32().NotNullable().PrimaryKey();
            
            Alter.Table("DeviceTest")
                .AddColumn("ExternalId").AsString(50).Nullable()
                .AddColumn("DeviceId").AsInt32().NotNullable()
                .AddColumn("TestDate").AsDateTime().NotNullable();

            Create.ForeignKey("FK_DeviTest_Devi")
                .FromTable("DeviceTest").ForeignColumn("DeviceId")
                .ToTable("Device").PrimaryColumn("DeviceId");

            Create.Index("IX_DeviTest_DI_TD")
                .OnTable("DeviceTest")
                    .OnColumn("DeviceId").Ascending()
                    .OnColumn("TestDate").Ascending().WithOptions().Unique();
        }
    }
}