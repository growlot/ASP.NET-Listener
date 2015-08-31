using System.Collections.Generic;
public class ReferenceInstallHistoryImpl: ITableInformation {
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
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallDate = "install_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveDate = "remove_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallStatus = "install_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestEqpNo = "test_eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StationId = "station_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseType = "use_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PositionId = "position_id";
	
	public string RealTableName
	{
		get { return "TREFERENCE_INSTALL_HISTORY".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"InstallDate", new ColumnInformation() { DataType = "DateTime", ModelName = "InstallDate", ColumnName = "install_date"}},
				{"RemoveDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RemoveDate", ColumnName = "remove_date"}},
				{"InstallStatus", new ColumnInformation() { DataType = "string", ModelName = "InstallStatus", ColumnName = "install_status"}},
				{"TestEqpNo", new ColumnInformation() { DataType = "string", ModelName = "TestEqpNo", ColumnName = "test_eqp_no"}},
				{"StationId", new ColumnInformation() { DataType = "string", ModelName = "StationId", ColumnName = "station_id"}},
				{"UseType", new ColumnInformation() { DataType = "string", ModelName = "UseType", ColumnName = "use_type"}},
				{"PositionId", new ColumnInformation() { DataType = "string", ModelName = "PositionId", ColumnName = "position_id"}},
			};

	public override string ToString() 
	{
		return "wndba.treference_install_history";
	}
}
