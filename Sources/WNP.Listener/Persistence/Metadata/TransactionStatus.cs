using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TransactionStatusImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionStatusId = "TransactionStatusId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description = "Description";
	
	public string RealTableName
	{
		get { return "TransactionStatus".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionStatusId", new ColumnInformation() { DataType = "int", ModelName = "TransactionStatusId", ColumnName = "TransactionStatusId"}},
				{"Description", new ColumnInformation() { DataType = "string", ModelName = "Description", ColumnName = "Description"}},
			};

	public override string ToString() 
	{
		return "dbo.transactionstatus";
	}
}
}
