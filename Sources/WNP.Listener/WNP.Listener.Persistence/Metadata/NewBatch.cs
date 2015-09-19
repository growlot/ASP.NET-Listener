using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class NewBatchImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string NewBatchNo = "NEW_BATCH_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewBatchStatus = "NEW_BATCH_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestMethod = "TEST_METHOD";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TotalQty = "TOTAL_QTY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectedQty = "SELECTED_QTY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestQty = "TEST_QTY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceDate = "ACCEPTANCE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceBy = "ACCEPTANCE_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewBatchDesc = "NEW_BATCH_DESC";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceDecision = "acceptance_decision";
	
	public string RealTableName
	{
		get { return "TNEW_BATCH".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"NewBatchNo", new ColumnInformation() { DataType = "string", ModelName = "NewBatchNo", ColumnName = "NEW_BATCH_NO"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"NewBatchStatus", new ColumnInformation() { DataType = "string", ModelName = "NewBatchStatus", ColumnName = "NEW_BATCH_STATUS"}},
				{"TestMethod", new ColumnInformation() { DataType = "string", ModelName = "TestMethod", ColumnName = "TEST_METHOD"}},
				{"TotalQty", new ColumnInformation() { DataType = "int", ModelName = "TotalQty", ColumnName = "TOTAL_QTY"}},
				{"SelectedQty", new ColumnInformation() { DataType = "int", ModelName = "SelectedQty", ColumnName = "SELECTED_QTY"}},
				{"TestQty", new ColumnInformation() { DataType = "int", ModelName = "TestQty", ColumnName = "TEST_QTY"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"AcceptanceDate", new ColumnInformation() { DataType = "DateTime", ModelName = "AcceptanceDate", ColumnName = "ACCEPTANCE_DATE"}},
				{"AcceptanceBy", new ColumnInformation() { DataType = "string", ModelName = "AcceptanceBy", ColumnName = "ACCEPTANCE_BY"}},
				{"NewBatchDesc", new ColumnInformation() { DataType = "string", ModelName = "NewBatchDesc", ColumnName = "NEW_BATCH_DESC"}},
				{"AcceptanceDecision", new ColumnInformation() { DataType = "string", ModelName = "AcceptanceDecision", ColumnName = "acceptance_decision"}},
			};

	public override string ToString() 
	{
		return "wndba.tnew_batch";
	}
}
}
