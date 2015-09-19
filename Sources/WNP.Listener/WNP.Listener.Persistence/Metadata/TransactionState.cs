using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TransactionStateImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionStateId = "TransactionStateId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description = "Description";
	
	public string RealTableName
	{
		get { return "TransactionState".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionStateId", new ColumnInformation() { DataType = "int", ModelName = "TransactionStateId", ColumnName = "TransactionStateId"}},
				{"Description", new ColumnInformation() { DataType = "string", ModelName = "Description", ColumnName = "Description"}},
			};

	public override string ToString() 
	{
		return "dbo.transactionstate";
	}
}
}
