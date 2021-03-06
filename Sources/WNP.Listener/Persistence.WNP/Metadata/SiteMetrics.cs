// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class SiteMetricsTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site { get; } = "site";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DateRead { get; } = "date_read";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceSide { get; } = "service_side";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadingType { get; } = "reading_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string MetricType { get; } = "metric_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner { get; } = "owner";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAReading { get; } = "phase_a_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseBReading { get; } = "phase_b_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseCReading { get; } = "phase_c_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NetReading { get; } = "net_reading";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RowNumber { get; } = "row_number";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WecoSn { get; } = "weco_sn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardSn { get; } = "standard_sn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TesterId { get; } = "tester_id";
	
	public string RealTableName
	{
		get { return "tsite_metrics".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "WNDBA.TSITE_METRICS";
	}
}
}
#pragma warning restore 1591
