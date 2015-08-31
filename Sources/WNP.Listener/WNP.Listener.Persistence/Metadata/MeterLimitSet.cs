using System.Collections.Generic;
public class MeterLimitSetImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string LimitIndex = "limit_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LimitName = "limit_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LimitDesc = "limit_desc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflHi = "sfl_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflLo = "sfl_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllHi = "sll_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllLo = "sll_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfHi = "spf_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfLo = "spf_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflHi = "efl_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflLo = "efl_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllHi = "ell_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllLo = "ell_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfHi = "epf_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfLo = "epf_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EflBalance = "efl_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EllBalance = "ell_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EpfBalance = "epf_balance";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaHi = "wa_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaLo = "wa_lo";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdHi = "dmd_hi";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdLo = "dmd_lo";
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
		get { return "tmeter_limit_set".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "wndba.tmeter_limit_set";
	}
}
