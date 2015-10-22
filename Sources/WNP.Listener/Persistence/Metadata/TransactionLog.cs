using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TransactionLogImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionLogId = "TransactionLogId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExternalId = "ExternalId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DeviceId = "DeviceId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DeviceTestId = "DeviceTestId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionTypeId = "TransactionTypeId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionStatusId = "TransactionStatusId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionSourceId = "TransactionSourceId";
	
	public string RealTableName
	{
		get { return "TransactionLog".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionLogId", new ColumnInformation() { DataType = "int", ModelName = "TransactionLogId", ColumnName = "TransactionLogId"}},
				{"ExternalId", new ColumnInformation() { DataType = "string", ModelName = "ExternalId", ColumnName = "ExternalId"}},
				{"DeviceId", new ColumnInformation() { DataType = "int", ModelName = "DeviceId", ColumnName = "DeviceId"}},
				{"DeviceTestId", new ColumnInformation() { DataType = "int", ModelName = "DeviceTestId", ColumnName = "DeviceTestId"}},
				{"TransactionTypeId", new ColumnInformation() { DataType = "int", ModelName = "TransactionTypeId", ColumnName = "TransactionTypeId"}},
				{"TransactionStatusId", new ColumnInformation() { DataType = "int", ModelName = "TransactionStatusId", ColumnName = "TransactionStatusId"}},
				{"TransactionSourceId", new ColumnInformation() { DataType = "int", ModelName = "TransactionSourceId", ColumnName = "TransactionSourceId"}},
			};

	public override string ToString() 
	{
		return "dbo.transactionlog";
	}
}
}
