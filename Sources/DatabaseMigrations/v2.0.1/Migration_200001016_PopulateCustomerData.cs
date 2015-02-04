//-----------------------------------------------------------------------
// <copyright file="Migration_200001016_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
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
    /// Performs a database migration
    /// </summary>
    [Migration(200001016)]
    public class Migration_200001016_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {            
            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                Update.Table("TransactionType").Set(new { TransactionCompletionId = "1" }).Where(new { TransactionDataId = "4" });
            }
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
            // Not available.
        }
    }
}
