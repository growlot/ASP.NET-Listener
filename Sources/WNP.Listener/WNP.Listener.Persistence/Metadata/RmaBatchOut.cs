using System.Collections.Generic;
public class RmaBatchOutImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string RmaBatchOutNo = "rma_batch_out_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchOutDesc = "rma_batch_out_desc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchOutStatus = "rma_batch_out_status";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorId = "vendor_id";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContactIndex = "contact_index";
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
	public string RmaNo = "rma_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuoteNo = "quote_no";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuoteCost = "quote_cost";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QtySent = "qty_sent";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QtyReturned = "qty_returned";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QtyFail = "qty_fail";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PromiseDate = "promise_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShipDate = "ship_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AllReceivedDate = "all_received_date";
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
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "trma_batch_out".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"RmaBatchOutNo", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchOutNo", ColumnName = "rma_batch_out_no"}},
				{"RmaBatchOutDesc", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchOutDesc", ColumnName = "rma_batch_out_desc"}},
				{"RmaBatchOutStatus", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchOutStatus", ColumnName = "rma_batch_out_status"}},
				{"VendorId", new ColumnInformation() { DataType = "int", ModelName = "VendorId", ColumnName = "vendor_id"}},
				{"ContactIndex", new ColumnInformation() { DataType = "int", ModelName = "ContactIndex", ColumnName = "contact_index"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"RmaNo", new ColumnInformation() { DataType = "string", ModelName = "RmaNo", ColumnName = "rma_no"}},
				{"QuoteNo", new ColumnInformation() { DataType = "string", ModelName = "QuoteNo", ColumnName = "quote_no"}},
				{"QuoteCost", new ColumnInformation() { DataType = "decimal", ModelName = "QuoteCost", ColumnName = "quote_cost"}},
				{"QtySent", new ColumnInformation() { DataType = "int", ModelName = "QtySent", ColumnName = "qty_sent"}},
				{"QtyReturned", new ColumnInformation() { DataType = "int", ModelName = "QtyReturned", ColumnName = "qty_returned"}},
				{"QtyFail", new ColumnInformation() { DataType = "int", ModelName = "QtyFail", ColumnName = "qty_fail"}},
				{"PromiseDate", new ColumnInformation() { DataType = "DateTime", ModelName = "PromiseDate", ColumnName = "promise_date"}},
				{"ShipDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ShipDate", ColumnName = "ship_date"}},
				{"AllReceivedDate", new ColumnInformation() { DataType = "DateTime", ModelName = "AllReceivedDate", ColumnName = "all_received_date"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.trma_batch_out";
	}
}
