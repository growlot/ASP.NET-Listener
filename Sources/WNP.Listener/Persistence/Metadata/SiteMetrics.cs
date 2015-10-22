using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SiteMetricsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site = "site";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DateRead = "date_read";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceSide = "service_side";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadingType = "reading_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string MetricType = "metric_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAReading = "phase_a_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseBReading = "phase_b_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseCReading = "phase_c_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NetReading = "net_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RowNumber = "row_number";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WecoSn = "weco_sn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardSn = "standard_sn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TesterId = "tester_id";
	
	public string RealTableName
	{
		get { return "tsite_metrics".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"DateRead", new ColumnInformation() { DataType = "DateTime", ModelName = "DateRead", ColumnName = "date_read"}},
				{"ServiceSide", new ColumnInformation() { DataType = "string", ModelName = "ServiceSide", ColumnName = "service_side"}},
				{"ReadingType", new ColumnInformation() { DataType = "string", ModelName = "ReadingType", ColumnName = "reading_type"}},
				{"MetricType", new ColumnInformation() { DataType = "string", ModelName = "MetricType", ColumnName = "metric_type"}},
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"PhaseAReading", new ColumnInformation() { DataType = "double", ModelName = "PhaseAReading", ColumnName = "phase_a_reading"}},
				{"PhaseBReading", new ColumnInformation() { DataType = "double", ModelName = "PhaseBReading", ColumnName = "phase_b_reading"}},
				{"PhaseCReading", new ColumnInformation() { DataType = "double", ModelName = "PhaseCReading", ColumnName = "phase_c_reading"}},
				{"NetReading", new ColumnInformation() { DataType = "double", ModelName = "NetReading", ColumnName = "net_reading"}},
				{"RowNumber", new ColumnInformation() { DataType = "double", ModelName = "RowNumber", ColumnName = "row_number"}},
				{"WecoSn", new ColumnInformation() { DataType = "string", ModelName = "WecoSn", ColumnName = "weco_sn"}},
				{"StandardSn", new ColumnInformation() { DataType = "string", ModelName = "StandardSn", ColumnName = "standard_sn"}},
				{"TesterId", new ColumnInformation() { DataType = "string", ModelName = "TesterId", ColumnName = "tester_id"}},
			};

	public override string ToString() 
	{
		return "wndba.tsite_metrics";
	}
}
}
