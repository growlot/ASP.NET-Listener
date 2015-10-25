// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MeterLimitSetTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string LimitIndex { get; } = "limit_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LimitName { get; } = "limit_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LimitDesc { get; } = "limit_desc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflHi { get; } = "sfl_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflLo { get; } = "sfl_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllHi { get; } = "sll_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllLo { get; } = "sll_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfHi { get; } = "spf_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfLo { get; } = "spf_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflHi { get; } = "efl_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflLo { get; } = "efl_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllHi { get; } = "ell_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllLo { get; } = "ell_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfHi { get; } = "epf_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfLo { get; } = "epf_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflBalance { get; } = "efl_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllBalance { get; } = "ell_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfBalance { get; } = "epf_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaHi { get; } = "wa_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaLo { get; } = "wa_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdHi { get; } = "dmd_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdLo { get; } = "dmd_lo";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "mod_by";
	
	public string RealTableName
	{
		get { return "tmeter_limit_set".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"LimitIndex", new ColumnInformation() { DataType = "int", ModelName = "LimitIndex", ColumnName = "limit_index"}},
				{"LimitName", new ColumnInformation() { DataType = "string", ModelName = "LimitName", ColumnName = "limit_name"}},
				{"LimitDesc", new ColumnInformation() { DataType = "string", ModelName = "LimitDesc", ColumnName = "limit_desc"}},
				{"SflHi", new ColumnInformation() { DataType = "decimal", ModelName = "SflHi", ColumnName = "sfl_hi"}},
				{"SflLo", new ColumnInformation() { DataType = "decimal", ModelName = "SflLo", ColumnName = "sfl_lo"}},
				{"SllHi", new ColumnInformation() { DataType = "decimal", ModelName = "SllHi", ColumnName = "sll_hi"}},
				{"SllLo", new ColumnInformation() { DataType = "decimal", ModelName = "SllLo", ColumnName = "sll_lo"}},
				{"SpfHi", new ColumnInformation() { DataType = "decimal", ModelName = "SpfHi", ColumnName = "spf_hi"}},
				{"SpfLo", new ColumnInformation() { DataType = "decimal", ModelName = "SpfLo", ColumnName = "spf_lo"}},
				{"EflHi", new ColumnInformation() { DataType = "decimal", ModelName = "EflHi", ColumnName = "efl_hi"}},
				{"EflLo", new ColumnInformation() { DataType = "decimal", ModelName = "EflLo", ColumnName = "efl_lo"}},
				{"EllHi", new ColumnInformation() { DataType = "decimal", ModelName = "EllHi", ColumnName = "ell_hi"}},
				{"EllLo", new ColumnInformation() { DataType = "decimal", ModelName = "EllLo", ColumnName = "ell_lo"}},
				{"EpfHi", new ColumnInformation() { DataType = "decimal", ModelName = "EpfHi", ColumnName = "epf_hi"}},
				{"EpfLo", new ColumnInformation() { DataType = "decimal", ModelName = "EpfLo", ColumnName = "epf_lo"}},
				{"EflBalance", new ColumnInformation() { DataType = "decimal", ModelName = "EflBalance", ColumnName = "efl_balance"}},
				{"EllBalance", new ColumnInformation() { DataType = "decimal", ModelName = "EllBalance", ColumnName = "ell_balance"}},
				{"EpfBalance", new ColumnInformation() { DataType = "decimal", ModelName = "EpfBalance", ColumnName = "epf_balance"}},
				{"WaHi", new ColumnInformation() { DataType = "decimal", ModelName = "WaHi", ColumnName = "wa_hi"}},
				{"WaLo", new ColumnInformation() { DataType = "decimal", ModelName = "WaLo", ColumnName = "wa_lo"}},
				{"DmdHi", new ColumnInformation() { DataType = "decimal", ModelName = "DmdHi", ColumnName = "dmd_hi"}},
				{"DmdLo", new ColumnInformation() { DataType = "decimal", ModelName = "DmdLo", ColumnName = "dmd_lo"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TMETER_LIMIT_SET";
	}
}
}
#pragma warning restore 1591
