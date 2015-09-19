using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ListenerTransactionStatisticsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionId = "transaction_id";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CallTime = "call_time";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionState = "transaction_state";
	
	public string RealTableName
	{
		get { return "tlistener_transaction_statistics".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"CallTime", new ColumnInformation() { DataType = "DateTime", ModelName = "CallTime", ColumnName = "call_time"}},
				{"TransactionState", new ColumnInformation() { DataType = "int", ModelName = "TransactionState", ColumnName = "transaction_state"}},
			};

	public override string ToString() 
	{
		return "wndba.tlistener_transaction_statistics";
	}
}
}
