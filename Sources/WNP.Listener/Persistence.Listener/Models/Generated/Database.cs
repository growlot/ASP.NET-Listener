



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `Listener`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Server=localhost\sqlexpress;Database=ListenerKCPL220;User Id=wndba; password=**zapped**;`
//     Schema:                 ``
//     Include Views:          `True`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AMSLLC.Listener.Persistence.Listener
{

	public partial class ListenerDB : Database
	{
		public ListenerDB() 
			: base("Listener")
		{
			CommonConstruct();
		}

		public ListenerDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			ListenerDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static ListenerDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new ListenerDB();
        }

		[ThreadStatic] static ListenerDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static ListenerDB repo { get { return ListenerDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    
	[TableName("Endpoint")]


	[PrimaryKey("EndpointId")]



	[ExplicitColumns]
    public partial class EndpointEntity : ListenerDB.Record<EndpointEntity>  
    {



		[Column] public int EndpointId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public int ProtocolTypeId { get; set; }





		[Column] public string ConnectionCfgJson { get; set; }





		[Column] public int? FieldConfigurationId { get; set; }





		[Column] public int EndpointTriggerTypeId { get; set; }



	}

    
	[TableName("OperationEndpoint")]


	[PrimaryKey("OperationEndpointId")]



	[ExplicitColumns]
    public partial class OperationEndpointEntity : ListenerDB.Record<OperationEndpointEntity>  
    {



		[Column] public int OperationEndpointId { get; set; }





		[Column] public int EnabledOperationId { get; set; }





		[Column] public int EndpointId { get; set; }



	}

    
	[TableName("TransactionRegistry")]


	[PrimaryKey("TransactionId")]



	[ExplicitColumns]
    public partial class TransactionRegistryEntity : ListenerDB.Record<TransactionRegistryEntity>  
    {



		[Column] public int TransactionId { get; set; }





		[Column] public int? ParentTransactionId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public int EnabledOperationId { get; set; }





		[Column] public int TransactionStatusId { get; set; }





		[Column] public string Header { get; set; }





		[Column] public string Data { get; set; }





		[Column] public string Summary { get; set; }





		[Column] public string User { get; set; }





		[Column] public string TransactionHash { get; set; }





		[Column] public string Message { get; set; }





		[Column] public string Details { get; set; }





		[Column] public DateTime CreatedDateTime { get; set; }





		[Column] public DateTime? UpdatedDateTime { get; set; }



	}

    
	[TableName("VersionInfo")]


	[ExplicitColumns]
    public partial class VersionInfoEntity : ListenerDB.Record<VersionInfoEntity>  
    {



		[Column] public long Version { get; set; }





		[Column] public DateTime? AppliedOn { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("TransactionStatus")]


	[PrimaryKey("TransactionStatusId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionStatusEntity : ListenerDB.Record<TransactionStatusEntity>  
    {



		[Column] public int TransactionStatusId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("TransactionState")]


	[PrimaryKey("TransactionStateId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionStateEntity : ListenerDB.Record<TransactionStateEntity>  
    {



		[Column] public int TransactionStateId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("TransactionSource")]


	[PrimaryKey("TransactionSourceId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionSourceEntity : ListenerDB.Record<TransactionSourceEntity>  
    {



		[Column] public int TransactionSourceId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("ServiceType")]


	[PrimaryKey("ServiceTypeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class ServiceTypeEntity : ListenerDB.Record<ServiceTypeEntity>  
    {



		[Column] public int ServiceTypeId { get; set; }





		[Column] public string ExternalCode { get; set; }





		[Column] public string InternalCode { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("Company")]


	[PrimaryKey("CompanyId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class CompanyEntity : ListenerDB.Record<CompanyEntity>  
    {



		[Column] public int CompanyId { get; set; }





		[Column] public string ExternalCode { get; set; }





		[Column] public string InternalCode { get; set; }





		[Column] public string Name { get; set; }



	}

    
	[TableName("EquipmentType")]


	[PrimaryKey("EquipmentTypeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class EquipmentTypeEntity : ListenerDB.Record<EquipmentTypeEntity>  
    {



		[Column] public int EquipmentTypeId { get; set; }





		[Column] public int ServiceTypeId { get; set; }





		[Column] public string ExternalCode { get; set; }





		[Column] public string InternalCode { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("Device")]


	[PrimaryKey("DeviceId")]



	[ExplicitColumns]
    public partial class DeviceEntity : ListenerDB.Record<DeviceEntity>  
    {



		[Column] public int DeviceId { get; set; }





		[Column] public string ExternalId { get; set; }





		[Column] public int CompanyId { get; set; }





		[Column] public string EquipmentNumber { get; set; }





		[Column] public int EquipmentTypeId { get; set; }



	}

    
	[TableName("DeviceTest")]


	[PrimaryKey("DeviceTestId")]



	[ExplicitColumns]
    public partial class DeviceTestEntity : ListenerDB.Record<DeviceTestEntity>  
    {



		[Column] public int DeviceTestId { get; set; }





		[Column] public string ExternalId { get; set; }





		[Column] public int DeviceId { get; set; }





		[Column] public DateTime TestDate { get; set; }



	}

    
	[TableName("Batch")]


	[PrimaryKey("BatchId")]



	[ExplicitColumns]
    public partial class BatchEntity : ListenerDB.Record<BatchEntity>  
    {



		[Column] public int BatchId { get; set; }





		[Column] public int TotalChunks { get; set; }





		[Column] public int LatestChunk { get; set; }



	}

    
	[TableName("TransactionLog")]


	[PrimaryKey("TransactionLogId")]



	[ExplicitColumns]
    public partial class TransactionLogEntity : ListenerDB.Record<TransactionLogEntity>  
    {



		[Column] public int TransactionLogId { get; set; }





		[Column] public string ExternalId { get; set; }





		[Column] public int? DeviceId { get; set; }





		[Column] public int? DeviceTestId { get; set; }





		[Column] public int? BatchId { get; set; }





		[Column] public int TransactionTypeId { get; set; }





		[Column] public int TransactionStatusId { get; set; }





		[Column] public string Message { get; set; }





		[Column] public string DebugInfo { get; set; }





		[Column] public DateTime TransactionStart { get; set; }





		[Column] public DateTime? TransactionEnd { get; set; }





		[Column] public int? DeviceBatchId { get; set; }





		[Column] public string DataHash { get; set; }



	}

    
	[TableName("TransactionLogState")]


	[PrimaryKey("TransactionLogStateId")]



	[ExplicitColumns]
    public partial class TransactionLogStateEntity : ListenerDB.Record<TransactionLogStateEntity>  
    {



		[Column] public int TransactionLogStateId { get; set; }





		[Column] public int TransactionLogId { get; set; }





		[Column] public int TransactionStateId { get; set; }





		[Column] public DateTime ExecutionTime { get; set; }



	}

    
	[TableName("TransactionData")]


	[PrimaryKey("TransactionDataId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionDatumEntity : ListenerDB.Record<TransactionDatumEntity>  
    {



		[Column] public int TransactionDataId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("TransactionDirection")]


	[PrimaryKey("TransactionDirectionId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionDirectionEntity : ListenerDB.Record<TransactionDirectionEntity>  
    {



		[Column] public int TransactionDirectionId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("ExternalSystem")]


	[PrimaryKey("ExternalSystemId")]



	[ExplicitColumns]
    public partial class ExternalSystemEntity : ListenerDB.Record<ExternalSystemEntity>  
    {



		[Column] public int ExternalSystemId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("TransactionCompletion")]


	[PrimaryKey("TransactionCompletionId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class TransactionCompletionEntity : ListenerDB.Record<TransactionCompletionEntity>  
    {



		[Column] public int TransactionCompletionId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("DeviceBatch")]


	[PrimaryKey("DeviceBatchId")]



	[ExplicitColumns]
    public partial class DeviceBatchEntity : ListenerDB.Record<DeviceBatchEntity>  
    {



		[Column] public int DeviceBatchId { get; set; }





		[Column] public string BatchNumber { get; set; }





		[Column] public string ExternalId { get; set; }



	}

    
	[TableName("TransactionType")]


	[PrimaryKey("TransactionTypeId")]



	[ExplicitColumns]
    public partial class TransactionTypeEntity : ListenerDB.Record<TransactionTypeEntity>  
    {



		[Column] public int TransactionTypeId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public int TransactionDataId { get; set; }





		[Column] public int TransactionSourceId { get; set; }





		[Column] public int TransactionDirectionId { get; set; }





		[Column] public int TransactionCompletionId { get; set; }





		[Column] public int? ExternalSystemId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileConverterKind")]


	[PrimaryKey("FileConverterKindId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileConverterKindEntity : ListenerDB.Record<FileConverterKindEntity>  
    {



		[Column] public int FileConverterKindId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileConverter")]


	[PrimaryKey("FileConverterId")]



	[ExplicitColumns]
    public partial class FileConverterEntity : ListenerDB.Record<FileConverterEntity>  
    {



		[Column] public int FileConverterId { get; set; }





		[Column] public string Argument1 { get; set; }





		[Column] public string Argument2 { get; set; }





		[Column] public string Argument3 { get; set; }





		[Column] public int FileConverterKindId { get; set; }



	}

    
	[TableName("FileTrimMode")]


	[PrimaryKey("FileTrimModeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileTrimModeEntity : ListenerDB.Record<FileTrimModeEntity>  
    {



		[Column] public int FileTrimModeId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileQuoteMode")]


	[PrimaryKey("FileQuoteModeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileQuoteModeEntity : ListenerDB.Record<FileQuoteModeEntity>  
    {



		[Column] public int FileQuoteModeId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileQuoteMultiline")]


	[PrimaryKey("FileQuoteMultilineId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileQuoteMultilineEntity : ListenerDB.Record<FileQuoteMultilineEntity>  
    {



		[Column] public int FileQuoteMultilineId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileAlignMode")]


	[PrimaryKey("FileAlignModeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileAlignModeEntity : ListenerDB.Record<FileAlignModeEntity>  
    {



		[Column] public int FileAlignModeId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("FileFixedMode")]


	[PrimaryKey("FileFixedModeId", autoIncrement=false)]

	[ExplicitColumns]
    public partial class FileFixedModeEntity : ListenerDB.Record<FileFixedModeEntity>  
    {



		[Column] public int FileFixedModeId { get; set; }





		[Column] public string Description { get; set; }



	}

    
	[TableName("File")]


	[PrimaryKey("FileId")]



	[ExplicitColumns]
    public partial class FileEntity : ListenerDB.Record<FileEntity>  
    {



		[Column] public int FileId { get; set; }





		[Column] public int ExternalSystemId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public int? FileFixedModeId { get; set; }





		[Column] public string Delimiter { get; set; }





		[Column] public bool System { get; set; }



	}

    
	[TableName("FileField")]


	[PrimaryKey("FileFieldId")]



	[ExplicitColumns]
    public partial class FileFieldEntity : ListenerDB.Record<FileFieldEntity>  
    {



		[Column] public int FileFieldId { get; set; }





		[Column] public int FileId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public int Index { get; set; }





		[Column] public string FieldType { get; set; }





		[Column] public int? FileConverterId { get; set; }





		[Column] public string NullValue { get; set; }





		[Column] public string TrimChars { get; set; }





		[Column] public int? FileTrimModeId { get; set; }





		[Column] public string Description { get; set; }





		[Column] public string AllignChar { get; set; }





		[Column] public int? FileAlignModeId { get; set; }





		[Column] public int? Length { get; set; }





		[Column] public bool? IsQuoted { get; set; }





		[Column] public string QuoteChar { get; set; }





		[Column] public int? FileQuoteModeId { get; set; }





		[Column] public int? FileQuoteMultilineId { get; set; }



	}

    
	[TableName("ProtocolType")]


	[PrimaryKey("ProtocolTypeId")]



	[ExplicitColumns]
    public partial class ProtocolTypeEntity : ListenerDB.Record<ProtocolTypeEntity>  
    {



		[Column] public int ProtocolTypeId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public string Name { get; set; }



	}

    
	[TableName("EndpointTriggerType")]


	[PrimaryKey("EndpointTriggerTypeId")]



	[ExplicitColumns]
    public partial class EndpointTriggerTypeEntity : ListenerDB.Record<EndpointTriggerTypeEntity>  
    {



		[Column] public int EndpointTriggerTypeId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public string Name { get; set; }



	}

    
	[TableName("EntityCategory")]


	[PrimaryKey("EntityCategoryId")]



	[ExplicitColumns]
    public partial class EntityCategoryEntity : ListenerDB.Record<EntityCategoryEntity>  
    {



		[Column] public int EntityCategoryId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public string DisplayName { get; set; }



	}

    
	[TableName("ValueMap")]


	[PrimaryKey("ValueMapId")]



	[ExplicitColumns]
    public partial class ValueMapEntity : ListenerDB.Record<ValueMapEntity>  
    {



		[Column] public int ValueMapId { get; set; }





		[Column] public int CompanyId { get; set; }





		[Column] public string ValueType { get; set; }





		[Column] public string Name { get; set; }



	}

    
	[TableName("FieldConfiguration")]


	[PrimaryKey("FieldConfigurationId")]



	[ExplicitColumns]
    public partial class FieldConfigurationEntity : ListenerDB.Record<FieldConfigurationEntity>  
    {



		[Column] public int FieldConfigurationId { get; set; }





		[Column] public int CompanyId { get; set; }





		[Column] public string Name { get; set; }



	}

    
	[TableName("ValueMapEntry")]


	[PrimaryKey("ValueMapEntryId")]



	[ExplicitColumns]
    public partial class ValueMapEntryEntity : ListenerDB.Record<ValueMapEntryEntity>  
    {



		[Column] public int ValueMapEntryId { get; set; }





		[Column] public int ValueMapId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public string Value { get; set; }



	}

    
	[TableName("FieldConfigurationEntry")]


	[PrimaryKey("FieldConfigurationEntryId")]



	[ExplicitColumns]
    public partial class FieldConfigurationEntryEntity : ListenerDB.Record<FieldConfigurationEntryEntity>  
    {



		[Column] public int FieldConfigurationEntryId { get; set; }





		[Column] public int FieldConfigurationId { get; set; }





		[Column] public string FieldName { get; set; }





		[Column] public int? ValueMapId { get; set; }





		[Column] public bool IncludeInHash { get; set; }





		[Column] public string MapToName { get; set; }



	}

    
	[TableName("Application")]


	[PrimaryKey("ApplicationId")]



	[ExplicitColumns]
    public partial class ApplicationEntity : ListenerDB.Record<ApplicationEntity>  
    {



		[Column] public int ApplicationId { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Key { get; set; }



	}

    
	[TableName("Operation")]


	[PrimaryKey("OperationId")]



	[ExplicitColumns]
    public partial class OperationEntity : ListenerDB.Record<OperationEntity>  
    {



		[Column] public int OperationId { get; set; }





		[Column] public string Key { get; set; }





		[Column] public string DisplayName { get; set; }



	}

    
	[TableName("EntityCategoryOperation")]


	[PrimaryKey("EntityCategoryOperationId")]



	[ExplicitColumns]
    public partial class EntityCategoryOperationEntity : ListenerDB.Record<EntityCategoryOperationEntity>  
    {



		[Column] public int EntityCategoryOperationId { get; set; }





		[Column] public int EntityCategoryId { get; set; }





		[Column] public int OperationId { get; set; }



	}

    
	[TableName("EnabledOperation")]


	[PrimaryKey("EnabledOperationId")]



	[ExplicitColumns]
    public partial class EnabledOperationEntity : ListenerDB.Record<EnabledOperationEntity>  
    {



		[Column] public int EnabledOperationId { get; set; }





		[Column] public int ApplicationId { get; set; }





		[Column] public int CompanyId { get; set; }





		[Column] public int OperationId { get; set; }



	}


}



