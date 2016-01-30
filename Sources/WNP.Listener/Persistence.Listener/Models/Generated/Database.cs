// <auto-generated>

// This file was automatically generated by the AsyncPoco T4 Template
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
using System.Threading.Tasks;
using System.Web;

namespace AMSLLC.Listener.Persistence.Listener
{
	using Poco;

	[CLSCompliant(false)]
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
			public Task<object> InsertAsync() { return repo.InsertAsync(this); }
			public Task SaveAsync() { return repo.SaveAsync(this); }
			public Task<int> UpdateAsync() { return repo.UpdateAsync(this); }
			public Task<int> UpdateAsync(IEnumerable<string> columns) { return repo.UpdateAsync(this, columns); }
			public static Task<int> UpdateAsync(string sql, params object[] args) { return repo.UpdateAsync<T>(sql, args); }
			public static Task<int> UpdateAsync(Sql sql) { return repo.UpdateAsync<T>(sql); }
			public Task<int> DeleteAsync() { return repo.DeleteAsync(this); }
			public static Task<int> DeleteAsync(string sql, params object[] args) { return repo.DeleteAsync<T>(sql, args); }
			public static Task<int> DeleteAsync(Sql sql) { return repo.DeleteAsync<T>(sql); }
			public static Task<int> DeleteAsync(object primaryKey) { return repo.DeleteAsync<T>(primaryKey); }
			public static Task<bool> ExistsAsync(object primaryKey) { return repo.ExistsAsync<T>(primaryKey); }
			public static Task<bool> ExistsAsync(string sql, params object[] args) { return repo.ExistsAsync<T>(sql, args); }
			public static Task<T> SingleOrDefaultAsync(object primaryKey) { return repo.SingleOrDefaultAsync<T>(primaryKey); }
			public static Task<T> SingleOrDefaultAsync(string sql, params object[] args) { return repo.SingleOrDefaultAsync<T>(sql, args); }
			public static Task<T> SingleOrDefaultAsync(Sql sql) { return repo.SingleOrDefaultAsync<T>(sql); }
			public static Task<T> FirstOrDefaultAsync(string sql, params object[] args) { return repo.FirstOrDefaultAsync<T>(sql, args); }
			public static Task<T> FirstOrDefaultAsync(Sql sql) { return repo.FirstOrDefaultAsync<T>(sql); }
			public static Task<T> SingleAsync(object primaryKey) { return repo.SingleAsync<T>(primaryKey); }
			public static Task<T> SingleAsync(string sql, params object[] args) { return repo.SingleAsync<T>(sql, args); }
			public static Task<T> SingleAsync(Sql sql) { return repo.SingleAsync<T>(sql); }
			public static Task<T> FirstAsync(string sql, params object[] args) { return repo.FirstAsync<T>(sql, args); }
			public static Task<T> FirstAsync(Sql sql) { return repo.FirstAsync<T>(sql); }
			public static Task<List<T>> FetchAsync(string sql, params object[] args) { return repo.FetchAsync<T>(sql, args); }
			public static Task<List<T>> FetchAsync(Sql sql) { return repo.FetchAsync<T>(sql); }
			public static Task<List<T>> FetchAsync(long page, long itemsPerPage, string sql, params object[] args) { return repo.FetchAsync<T>(page, itemsPerPage, sql, args); }
			public static Task<List<T>> FetchAsync(long page, long itemsPerPage, Sql sql) { return repo.FetchAsync<T>(page, itemsPerPage, sql); }
			public static Task<List<T>> SkipTakeAsync(long skip, long take, string sql, params object[] args) { return repo.SkipTakeAsync<T>(skip, take, sql, args); }
			public static Task<List<T>> SkipTakeAsync(long skip, long take, Sql sql) { return repo.SkipTakeAsync<T>(skip, take, sql); }
			public static Task<Page<T>> PageAsync(long page, long itemsPerPage, string sql, params object[] args) { return repo.PageAsync<T>(page, itemsPerPage, sql, args); }
			public static Task<Page<T>> PageAsync(long page, long itemsPerPage, Sql sql) { return repo.PageAsync<T>(page, itemsPerPage, sql); }
			public static Task QueryAsync(string sql, object[] args, Action<T> action) { return repo.QueryAsync<T>(sql, args, action); }
			public static Task QueryAsync(string sql, object[] args, Func<T, bool> func) { return repo.QueryAsync<T>(sql, args, func); }
			public static Task QueryAsync(string sql, Action<T> action) { return repo.QueryAsync<T>(sql, action); }
			public static Task QueryAsync(string sql, Func<T, bool> func) { return repo.QueryAsync<T>(sql, func); }
			public static Task QueryAsync(Sql sql, Action<T> action) { return repo.QueryAsync<T>(sql, action); }
			public static Task QueryAsync(Sql sql, Func<T, bool> func) { return repo.QueryAsync<T>(sql, func); }
		}
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
    
	[TableName("ProtocolType")]
	[PrimaryKey("ProtocolTypeId")]
	[ExplicitColumns]
    public partial class ProtocolTypeEntity : ListenerDB.Record<ProtocolTypeEntity>  
    {
		[Column] public int ProtocolTypeId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string Description { get; set; }
	}
    
	[TableName("EndpointTriggerType")]
	[PrimaryKey("EndpointTriggerTypeId", autoIncrement=false)]
	[ExplicitColumns]
    public partial class EndpointTriggerTypeEntity : ListenerDB.Record<EndpointTriggerTypeEntity>  
    {
		[Column] public int EndpointTriggerTypeId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string Description { get; set; }
	}
    
	[TableName("EntityCategory")]
	[PrimaryKey("EntityCategoryId")]
	[ExplicitColumns]
    public partial class EntityCategoryEntity : ListenerDB.Record<EntityCategoryEntity>  
    {
		[Column] public int EntityCategoryId { get; set; }
		[Column] public string Name { get; set; }
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
		[Column] public string RecordKey { get; set; }
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
		[Column] public short? OutgoingSequence { get; set; }
		[Column] public short? IncomingSequence { get; set; }
		[Column] public bool IncludeInSummary { get; set; }
		[Column] public string MapToName { get; set; }
	}
    
	[TableName("Application")]
	[PrimaryKey("ApplicationId")]
	[ExplicitColumns]
    public partial class ApplicationEntity : ListenerDB.Record<ApplicationEntity>  
    {
		[Column] public int ApplicationId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string RecordKey { get; set; }
	}
    
	[TableName("Operation")]
	[PrimaryKey("OperationId")]
	[ExplicitColumns]
    public partial class OperationEntity : ListenerDB.Record<OperationEntity>  
    {
		[Column] public int OperationId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string DisplayName { get; set; }
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
    
	[TableName("EntityCategoryOperation")]
	[PrimaryKey("EntityCategoryOperationId")]
	[ExplicitColumns]
    public partial class EntityCategoryOperationEntity : ListenerDB.Record<EntityCategoryOperationEntity>  
    {
		[Column] public int EntityCategoryOperationId { get; set; }
		[Column] public int EntityCategoryId { get; set; }
		[Column] public int EnabledOperationId { get; set; }
		[Column] public int? FieldConfigurationId { get; set; }
		[Column] public int CompanyId { get; set; }
		[Column] public Guid OperationTransactionKey { get; set; }
		[Column] public string OperationTransactionName { get; set; }
		[Column] public bool AutoSucceed { get; set; }
	}
    
	[TableName("Endpoint")]
	[PrimaryKey("EndpointId")]
	[ExplicitColumns]
    public partial class EndpointEntity : ListenerDB.Record<EndpointEntity>  
    {
		[Column] public int EndpointId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public int ProtocolTypeId { get; set; }
		[Column] public string ConnectionConfiguration { get; set; }
		[Column] public string AdapterConfiguration { get; set; }
		[Column] public int EndpointTriggerTypeId { get; set; }
		[Column] public int CompanyId { get; set; }
	}
    
	[TableName("OperationEndpoint")]
	[PrimaryKey("OperationEndpointId")]
	[ExplicitColumns]
    public partial class OperationEndpointEntity : ListenerDB.Record<OperationEndpointEntity>  
    {
		[Column] public int OperationEndpointId { get; set; }
		[Column] public int EntityCategoryOperationId { get; set; }
		[Column] public int EndpointId { get; set; }
	}
    
	[TableName("TransactionRegistry")]
	[PrimaryKey("RecordKey", autoIncrement=false)]
	[ExplicitColumns]
    public partial class TransactionRegistryEntity : ListenerDB.Record<TransactionRegistryEntity>  
    {
		[Column] public int TransactionId { get; set; }
		[Column] public Guid? BatchKey { get; set; }
		[Column] public Guid RecordKey { get; set; }
		[Column] public int EntityCategoryOperationId { get; set; }
		[Column] public int TransactionStatusId { get; set; }
		[Column] public int? Priority { get; set; }
		[Column] public string IncomingHash { get; set; }
		[Column] public string Data { get; set; }
		[Column] public string Summary { get; set; }
		[Column] public string AppUser { get; set; }
		[Column] public string OutgoingHash { get; set; }
		[Column] public string Message { get; set; }
		[Column] public string Details { get; set; }
		[Column] public DateTime CreatedDateTime { get; set; }
		[Column] public DateTime? UpdatedDateTime { get; set; }
	}
    
	[TableName("TransactionMessageData")]
	[PrimaryKey("RecordKey", autoIncrement=false)]
	[ExplicitColumns]
    public partial class TransactionMessageDatumEntity : ListenerDB.Record<TransactionMessageDatumEntity>  
    {
		[Column] public Guid RecordKey { get; set; }
		[Column] public string MessageData { get; set; }
	}
    
	[TableName("TransactionRegistryView")]
	[ExplicitColumns]
    public partial class TransactionRegistryViewEntity : ListenerDB.Record<TransactionRegistryViewEntity>  
    {
		[Column] public int TransactionId { get; set; }
		[Column] public Guid RecordKey { get; set; }
		[Column] public Guid? BatchKey { get; set; }
		[Column] public int TransactionStatusId { get; set; }
		[Column] public DateTime CreatedDateTime { get; set; }
		[Column] public DateTime? UpdatedDateTime { get; set; }
		[Column] public string Message { get; set; }
		[Column] public string Details { get; set; }
		[Column] public string EntityCategory { get; set; }
		[Column] public string OperationName { get; set; }
		[Column] public string ApplicationKey { get; set; }
		[Column] public string ApplicationName { get; set; }
		[Column] public string CompanyCode { get; set; }
		[Column] public string CompanyName { get; set; }
		[Column] public string EntityKey { get; set; }
		[Column] public string BatchNumber { get; set; }
		[Column] public DateTime? StartDate { get; set; }
	}
}
