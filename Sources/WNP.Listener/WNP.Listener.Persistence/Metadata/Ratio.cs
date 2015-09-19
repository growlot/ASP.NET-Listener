using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class RatioImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string EqpType = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string NameplateRatio = "NAMEPLATE_RATIO";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Windings = "WINDINGS";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Taps = "TAPS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioTap1 = "RATIO_TAP1";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MultTap1 = "MULT_TAP1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioTap2 = "RATIO_TAP2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MultTap2 = "MULT_TAP2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioTap3 = "RATIO_TAP3";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MultTap3 = "MULT_TAP3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioTap4 = "RATIO_TAP4";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MultTap4 = "MULT_TAP4";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioSec = "ratio_sec";
	
	public string RealTableName
	{
		get { return "TRATIO".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"NameplateRatio", new ColumnInformation() { DataType = "string", ModelName = "NameplateRatio", ColumnName = "NAMEPLATE_RATIO"}},
				{"Windings", new ColumnInformation() { DataType = "int", ModelName = "Windings", ColumnName = "WINDINGS"}},
				{"Taps", new ColumnInformation() { DataType = "int", ModelName = "Taps", ColumnName = "TAPS"}},
				{"RatioTap1", new ColumnInformation() { DataType = "string", ModelName = "RatioTap1", ColumnName = "RATIO_TAP1"}},
				{"MultTap1", new ColumnInformation() { DataType = "decimal", ModelName = "MultTap1", ColumnName = "MULT_TAP1"}},
				{"RatioTap2", new ColumnInformation() { DataType = "string", ModelName = "RatioTap2", ColumnName = "RATIO_TAP2"}},
				{"MultTap2", new ColumnInformation() { DataType = "decimal", ModelName = "MultTap2", ColumnName = "MULT_TAP2"}},
				{"RatioTap3", new ColumnInformation() { DataType = "string", ModelName = "RatioTap3", ColumnName = "RATIO_TAP3"}},
				{"MultTap3", new ColumnInformation() { DataType = "decimal", ModelName = "MultTap3", ColumnName = "MULT_TAP3"}},
				{"RatioTap4", new ColumnInformation() { DataType = "string", ModelName = "RatioTap4", ColumnName = "RATIO_TAP4"}},
				{"MultTap4", new ColumnInformation() { DataType = "decimal", ModelName = "MultTap4", ColumnName = "MULT_TAP4"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"RatioSec", new ColumnInformation() { DataType = "string", ModelName = "RatioSec", ColumnName = "ratio_sec"}},
			};

	public override string ToString() 
	{
		return "wndba.tratio";
	}
}
}
