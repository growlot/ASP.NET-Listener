using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ListenerTransactionTypeImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionType = "transaction_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionTypeDesc = "transaction_type_desc";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "tlistener_transaction_type".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionType", new ColumnInformation() { DataType = "int", ModelName = "TransactionType", ColumnName = "transaction_type"}},
				{"TransactionTypeDesc", new ColumnInformation() { DataType = "string", ModelName = "TransactionTypeDesc", ColumnName = "transaction_type_desc"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tlistener_transaction_type";
	}
}
}
