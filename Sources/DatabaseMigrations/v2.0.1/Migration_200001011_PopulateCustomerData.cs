//-----------------------------------------------------------------------
// <copyright file="Migration_200001011_PopulateCustomerData.cs" company="Advanced Metering Services LLC">
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
    [Migration(200001011)]
    public class Migration_200001011_PopulateCustomerData : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            if (((string)this.ApplicationContext).Contains("Alliant"))
            {
                Insert.IntoTable("ExternalSystem").Row(new { Name = "CC&B", Description = "Oracle Utilities Customer Care & Billing" });

                // TransactionType populate
                Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (1
                               ,0
                               ,1
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'CC&B'
                               )
                               ,'Device Retrieve'
                               ,'')");

                Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (2
                               ,0
                               ,2
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'CC&B'
                               )
                               ,'Device Shop Test'
                               ,'')");

                Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (3
                               ,1
                               ,1
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'CC&B'
                               )
                               ,'System Table Validate'
                               ,'')");

                // TransactionLog update TransactionTypeId
                Execute.Sql(@"
                    UPDATE TransactionLog
                    SET TransactionTypeId = (
                                                select TransactionTypeId 
                                                from TransactionType 
                                                where TransactionDataId = 1
                                              )
                    WHERE TransactionTypeId = 701");

                Execute.Sql(@"
                    UPDATE TransactionLog
                    SET TransactionTypeId = (
                                                select TransactionTypeId 
                                                from TransactionType 
                                                where TransactionDataId = 2
                                              )
                    WHERE TransactionTypeId = 100");

                Execute.Sql(@"
                    UPDATE TransactionLog
                    SET TransactionTypeId = (
                                                select TransactionTypeId 
                                                from TransactionType 
                                                where TransactionDataId = 3
                                              )
                    WHERE TransactionTypeId = 1200");
            }

            if (((string)this.ApplicationContext).Contains("KCP&L"))
            {
                Insert.IntoTable("ExternalSystem").Row(new { Name = "CIS", Description = "Customer Information System" });
                Insert.IntoTable("ExternalSystem").Row(new { Name = "ODM", Description = "Oracle Utilities Operational Device Management" });

                // TransactionType populate
                Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (2
                               ,0
                               ,2
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'CIS'
                               )
                               ,'Device Shop Test (CIS)'
                               ,'')");

////                Execute.Sql(@"
////                    INSERT INTO TransactionType
////                               (TransactionDataId
////                               ,TransactionSourceId
////                               ,TransactionDirectionId
////                               ,TransactionCompletionId
////                               ,ExternalSystemId
////                               ,Name
////                               ,Description)
////                         VALUES
////                               (1
////                               ,0
////                               ,2
////                               ,2
////                               ,(
////                                    SELECT ExternalSystemId 
////                                    FROM ExternalSystem 
////                                    WHERE Name = 'ODM'
////                               )
////                               ,'Device Update (ODM)'
////                               ,'')");

////                Execute.Sql(@"
////                    INSERT INTO TransactionType
////                               (TransactionDataId
////                               ,TransactionSourceId
////                               ,TransactionDirectionId
////                               ,TransactionCompletionId
////                               ,ExternalSystemId
////                               ,Name
////                               ,Description)
////                         VALUES
////                               (2
////                               ,0
////                               ,2
////                               ,2
////                               ,(
////                                    SELECT ExternalSystemId 
////                                    FROM ExternalSystem 
////                                    WHERE Name = 'ODM'
////                               )
////                               ,'Device Shop Test (ODM)'
////                               ,'')");

                Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,Name
                               ,Description)
                         VALUES
                               (4
                               ,0
                               ,2
                               ,'New Batch Acceptance'
                               ,'')");

                // TransactionLog update TransactionTypeId
                Execute.Sql(@"
                    UPDATE TransactionLog
                    SET TransactionTypeId = (
                                                select TransactionTypeId 
                                                from TransactionType tt
                                                inner join ExternalSystem es on tt.ExternalSystemId = es.ExternalSystemId
                                                where tt.TransactionDataId = 2 and es.Name = 'CIS'
                                              )
                    WHERE TransactionTypeId = 100");
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