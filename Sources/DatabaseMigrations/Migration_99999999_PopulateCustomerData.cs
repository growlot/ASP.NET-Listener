//-----------------------------------------------------------------------
// <copyright file="Migration_99999999_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.DatabaseMigrations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FluentMigrator;

    /// <summary>
    /// Included for compatibility with listener client in earlier versions.
    /// </summary>
    [Migration(99999999)]
    public class Migration_99999999_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
        }
    }
}