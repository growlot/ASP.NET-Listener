using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class RmaBatchInImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string RmaBatchInNo = "rma_batch_in_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchInDesc = "rma_batch_in_desc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchInStatus = "rma_batch_in_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "eqp_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestMethod = "test_method";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TotalQty = "total_qty";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectedQty = "selected_qty";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestQty = "test_qty";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceDate = "acceptance_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceBy = "acceptance_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceDecision = "acceptance_decision";
	
	public string RealTableName
	{
		get { return "trma_batch_in".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"RmaBatchInNo", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchInNo", ColumnName = "rma_batch_in_no"}},
				{"RmaBatchInDesc", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchInDesc", ColumnName = "rma_batch_in_desc"}},
				{"RmaBatchInStatus", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchInStatus", ColumnName = "rma_batch_in_status"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"TestMethod", new ColumnInformation() { DataType = "string", ModelName = "TestMethod", ColumnName = "test_method"}},
				{"TotalQty", new ColumnInformation() { DataType = "int", ModelName = "TotalQty", ColumnName = "total_qty"}},
				{"SelectedQty", new ColumnInformation() { DataType = "int", ModelName = "SelectedQty", ColumnName = "selected_qty"}},
				{"TestQty", new ColumnInformation() { DataType = "int", ModelName = "TestQty", ColumnName = "test_qty"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"AcceptanceDate", new ColumnInformation() { DataType = "DateTime", ModelName = "AcceptanceDate", ColumnName = "acceptance_date"}},
				{"AcceptanceBy", new ColumnInformation() { DataType = "string", ModelName = "AcceptanceBy", ColumnName = "acceptance_by"}},
				{"AcceptanceDecision", new ColumnInformation() { DataType = "string", ModelName = "AcceptanceDecision", ColumnName = "acceptance_decision"}},
			};

	public override string ToString() 
	{
		return "wndba.trma_batch_in";
	}
}
}
