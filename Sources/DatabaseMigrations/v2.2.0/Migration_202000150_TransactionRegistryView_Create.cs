// <copyright file="Migration_202000150_TransactionRegistryView_Create.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.DatabaseMigrations
{
    using FluentMigrator;

    /// <summary>
    /// Database migration step
    /// </summary>
    [Migration(202000150)]
    public class Migration_202000150_TransactionRegistryView_Create : Migration
    {
        /// <summary>
        /// Perform database upgrade action
        /// </summary>
        public override void Up()
        {
            this.Execute.Sql(@"CREATE VIEW TransactionRegistryView AS 
  SELECT TR.TransactionId,
		TR.[RecordKey],
		TR.[BatchKey],
		TR.[TransactionStatusId],
		TR.[CreatedDateTime],
		TR.[UpdatedDateTime],
		TR.[Message],
		TR.[Details],
		EC.[Name] AS EntityCategory,
		O.[Name] AS OperationName,
		A.[RecordKey] AS ApplicationKey,
		C.[ExternalCode] AS CompanyCode,
		TR.Summary.value('(/root/EntityKey)[1]', 'varchar(50)') AS EntityKey,
		TR.Summary.value('(/root/BatchNumber)[1]', 'varchar(50)') AS BatchNumber,
		TR.Summary.value('(/root/StartDate)[1]', 'date') AS StartDate
  FROM TransactionRegistry TR
  INNER JOIN EntityCategoryOperation ECO ON TR.EntityCategoryOperationId = ECO.EntityCategoryOperationId
  INNER JOIN EntityCategory EC ON ECO.EntityCategoryId = EC.EntityCategoryId
  INNER JOIN EnabledOperation EO ON ECO.EnabledOperationId = EO.EnabledOperationId
  INNER JOIN Operation O ON EO.OperationId = O.OperationId
  INNER JOIN [Application] A ON EO.ApplicationId = A.ApplicationId
  INNER JOIN [Company] C ON EO.CompanyId = C.CompanyId");
        }

        /// <summary>
        /// Perform database downgrade action
        /// </summary>
        public override void Down()
        {
            this.Execute.Sql(@"IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS
         WHERE TABLE_NAME = 'TransactionRegistryView')
   DROP VIEW TransactionRegistryView");
        }
    }
}