// //-----------------------------------------------------------------------
// // <copyright file="Migration_202000003_TransactionType_Populate.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Performs a database migration
    /// </summary>
    [Migration(202000003)]
    public class Migration_202000003_TransactionType_Populate : Migration
    {
        /// <summary>
        /// Performs the database migration
        /// </summary>
        public override void Up()
        {
            // TransactionType populate
            this.Execute.Sql(@"
                    INSERT INTO TransactionType
                               (TransactionDataId
                               ,TransactionSourceId
                               ,TransactionDirectionId
                               ,ExternalSystemId
                               ,Name
                               ,Description)
                         VALUES
                               (5
                               ,1
                               ,2
                               ,(
                                    SELECT ExternalSystemId 
                                    FROM ExternalSystem 
                                    WHERE Name = 'WecoMobile'
                               )
                               ,'Site Checkout (Weco Mobile)'
                               ,'')");
        }

        /// <summary>
        /// Rolls back the database migration
        /// </summary>
        public override void Down()
        {
        }
    }
}