//-----------------------------------------------------------------------
// <copyright file="Migration_200001013_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001013)]
    public class Migration_200001013_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {            
            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                // TransactionType populate
                Execute.Sql(@"
                    UPDATE TransactionType
                       SET TransactionCompletionId = 2
                     WHERE ExternalSystemId in 
                               (
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'ODM'
                               )");
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
