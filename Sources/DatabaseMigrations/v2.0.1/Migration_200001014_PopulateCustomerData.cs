//-----------------------------------------------------------------------
// <copyright file="Migration_200001014_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001014)]
    public class Migration_200001014_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                this.Execute.Sql(@"
                                    INSERT INTO TransactionType
                                               (TransactionDataId
                                               ,TransactionSourceId
                                               ,TransactionDirectionId
                                               ,TransactionCompletionId
                                               ,ExternalSystemId
                                               ,Name
                                               ,Description)
                                         VALUES
                                               (1
                                               ,0
                                               ,2
                                               ,2
                                               ,(
                                                    SELECT ExternalSystemId 
                                                    FROM ExternalSystem 
                                                    WHERE Name = 'ODM'
                                               )
                                               ,'Device Update (ODM)'
                                               ,'')");

                this.Execute.Sql(@"
                                    INSERT INTO TransactionType
                                               (TransactionDataId
                                               ,TransactionSourceId
                                               ,TransactionDirectionId
                                               ,TransactionCompletionId
                                               ,ExternalSystemId
                                               ,Name
                                               ,Description)
                                         VALUES
                                               (2
                                               ,0
                                               ,2
                                               ,2
                                               ,(
                                                    SELECT ExternalSystemId 
                                                    FROM ExternalSystem 
                                                    WHERE Name = 'ODM'
                                               )
                                               ,'Device Shop Test (ODM)'
                                               ,'')");
            }

            if (((string)this.ApplicationContext).Contains("LabTrack"))
            {
                this.Insert.IntoTable("ExternalSystem").Row(new { Name = "LabTrack", Description = "Meter Data Collection and Inventory System" });

                // TransactionType populate
                this.Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,TransactionCompletionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (2
                               ,0
                               ,2
                               ,1
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'LabTrack'
                               )
                               ,'Device Shop Test (LabTrack)'
                               ,'')");

                this.Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,TransactionCompletionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (4
                               ,0
                               ,2
                               ,1
                               ,NULL
                               ,'New Batch Acceptance'
                               ,'')");
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