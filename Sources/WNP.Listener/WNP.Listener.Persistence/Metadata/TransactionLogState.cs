


using AMSLLC.Listener.Persistence.Metadata;

// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `WNPDatabase`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=(local);Initial Catalog=wattnetplusAlliant;Integrated Security=True;`
//     Schema:                 ``
//     Include Views:          `False`

using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TransactionLogStateImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TransactionLogStateId = "TransactionLogStateId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionLogId = "TransactionLogId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionStateId = "TransactionStateId";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExecutionTime = "ExecutionTime";
	
	public string RealTableName
	{
		get { return "TransactionLogState".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TransactionLogStateId", new ColumnInformation() { DataType = "int", ModelName = "TransactionLogStateId", ColumnName = "TransactionLogStateId"}},
				{"TransactionLogId", new ColumnInformation() { DataType = "int", ModelName = "TransactionLogId", ColumnName = "TransactionLogId"}},
				{"TransactionStateId", new ColumnInformation() { DataType = "int", ModelName = "TransactionStateId", ColumnName = "TransactionStateId"}},
				{"ExecutionTime", new ColumnInformation() { DataType = "DateTime", ModelName = "ExecutionTime", ColumnName = "ExecutionTime"}},
			};

	public override string ToString() 
	{
		return "dbo.transactionlogstate";
	}
}
}
