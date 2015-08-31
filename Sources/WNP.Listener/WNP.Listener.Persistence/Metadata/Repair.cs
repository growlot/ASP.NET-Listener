using System.Collections.Generic;
public class RepairImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "eqp_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairIndex = "repair_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairStatus = "repair_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request1 = "request1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request2 = "request2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request3 = "request3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request4 = "request4";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request5 = "request5";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Request6 = "request6";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RequestSrc = "request_src";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RequestDate = "request_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RequestBy = "request_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair1 = "repair1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair2 = "repair2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair3 = "repair3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair4 = "repair4";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair5 = "repair5";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Repair6 = "repair6";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairDate = "repair_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairBy = "repair_by";
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
		get { return "TREPAIR".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"RepairIndex", new ColumnInformation() { DataType = "int", ModelName = "RepairIndex", ColumnName = "repair_index"}},
				{"RepairStatus", new ColumnInformation() { DataType = "string", ModelName = "RepairStatus", ColumnName = "repair_status"}},
				{"Request1", new ColumnInformation() { DataType = "string", ModelName = "Request1", ColumnName = "request1"}},
				{"Request2", new ColumnInformation() { DataType = "string", ModelName = "Request2", ColumnName = "request2"}},
				{"Request3", new ColumnInformation() { DataType = "string", ModelName = "Request3", ColumnName = "request3"}},
				{"Request4", new ColumnInformation() { DataType = "string", ModelName = "Request4", ColumnName = "request4"}},
				{"Request5", new ColumnInformation() { DataType = "string", ModelName = "Request5", ColumnName = "request5"}},
				{"Request6", new ColumnInformation() { DataType = "string", ModelName = "Request6", ColumnName = "request6"}},
				{"RequestSrc", new ColumnInformation() { DataType = "string", ModelName = "RequestSrc", ColumnName = "request_src"}},
				{"RequestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RequestDate", ColumnName = "request_date"}},
				{"RequestBy", new ColumnInformation() { DataType = "string", ModelName = "RequestBy", ColumnName = "request_by"}},
				{"Repair1", new ColumnInformation() { DataType = "string", ModelName = "Repair1", ColumnName = "repair1"}},
				{"Repair2", new ColumnInformation() { DataType = "string", ModelName = "Repair2", ColumnName = "repair2"}},
				{"Repair3", new ColumnInformation() { DataType = "string", ModelName = "Repair3", ColumnName = "repair3"}},
				{"Repair4", new ColumnInformation() { DataType = "string", ModelName = "Repair4", ColumnName = "repair4"}},
				{"Repair5", new ColumnInformation() { DataType = "string", ModelName = "Repair5", ColumnName = "repair5"}},
				{"Repair6", new ColumnInformation() { DataType = "string", ModelName = "Repair6", ColumnName = "repair6"}},
				{"RepairDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RepairDate", ColumnName = "repair_date"}},
				{"RepairBy", new ColumnInformation() { DataType = "string", ModelName = "RepairBy", ColumnName = "repair_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.trepair";
	}
}
