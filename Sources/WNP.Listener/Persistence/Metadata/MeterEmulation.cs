// <auto-generated>
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MeterEmulationImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Form = "form";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Base = "base";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Service = "service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseEmulationMode = "use_emulation_mode";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V1Va = "v1_va";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V1Vb = "v1_vb";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V1Vc = "v1_vc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V2Va = "v2_va";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V2Vb = "v2_vb";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V2Vc = "v2_vc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V3Va = "v3_va";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V3Vb = "v3_vb";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string V3Vc = "v3_vc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I1Ia = "i1_ia";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I1Ib = "i1_ib";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I1Ic = "i1_ic";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I2Ia = "i2_ia";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I2Ib = "i2_ib";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I2Ic = "i2_ic";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I3Ia = "i3_ia";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I3Ib = "i3_ib";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string I3Ic = "i3_ic";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pb = "pb";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pc = "pc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pab = "pab";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pac = "pac";
	
	public string RealTableName
	{
		get { return "tmeter_emulation".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Form", new ColumnInformation() { DataType = "string", ModelName = "Form", ColumnName = "form"}},
				{"Base", new ColumnInformation() { DataType = "string", ModelName = "Base", ColumnName = "base"}},
				{"Service", new ColumnInformation() { DataType = "string", ModelName = "Service", ColumnName = "service"}},
				{"UseEmulationMode", new ColumnInformation() { DataType = "string", ModelName = "UseEmulationMode", ColumnName = "use_emulation_mode"}},
				{"V1Va", new ColumnInformation() { DataType = "decimal", ModelName = "V1Va", ColumnName = "v1_va"}},
				{"V1Vb", new ColumnInformation() { DataType = "decimal", ModelName = "V1Vb", ColumnName = "v1_vb"}},
				{"V1Vc", new ColumnInformation() { DataType = "decimal", ModelName = "V1Vc", ColumnName = "v1_vc"}},
				{"V2Va", new ColumnInformation() { DataType = "decimal", ModelName = "V2Va", ColumnName = "v2_va"}},
				{"V2Vb", new ColumnInformation() { DataType = "decimal", ModelName = "V2Vb", ColumnName = "v2_vb"}},
				{"V2Vc", new ColumnInformation() { DataType = "decimal", ModelName = "V2Vc", ColumnName = "v2_vc"}},
				{"V3Va", new ColumnInformation() { DataType = "decimal", ModelName = "V3Va", ColumnName = "v3_va"}},
				{"V3Vb", new ColumnInformation() { DataType = "decimal", ModelName = "V3Vb", ColumnName = "v3_vb"}},
				{"V3Vc", new ColumnInformation() { DataType = "decimal", ModelName = "V3Vc", ColumnName = "v3_vc"}},
				{"I1Ia", new ColumnInformation() { DataType = "decimal", ModelName = "I1Ia", ColumnName = "i1_ia"}},
				{"I1Ib", new ColumnInformation() { DataType = "decimal", ModelName = "I1Ib", ColumnName = "i1_ib"}},
				{"I1Ic", new ColumnInformation() { DataType = "decimal", ModelName = "I1Ic", ColumnName = "i1_ic"}},
				{"I2Ia", new ColumnInformation() { DataType = "decimal", ModelName = "I2Ia", ColumnName = "i2_ia"}},
				{"I2Ib", new ColumnInformation() { DataType = "decimal", ModelName = "I2Ib", ColumnName = "i2_ib"}},
				{"I2Ic", new ColumnInformation() { DataType = "decimal", ModelName = "I2Ic", ColumnName = "i2_ic"}},
				{"I3Ia", new ColumnInformation() { DataType = "decimal", ModelName = "I3Ia", ColumnName = "i3_ia"}},
				{"I3Ib", new ColumnInformation() { DataType = "decimal", ModelName = "I3Ib", ColumnName = "i3_ib"}},
				{"I3Ic", new ColumnInformation() { DataType = "decimal", ModelName = "I3Ic", ColumnName = "i3_ic"}},
				{"Pb", new ColumnInformation() { DataType = "string", ModelName = "Pb", ColumnName = "pb"}},
				{"Pc", new ColumnInformation() { DataType = "string", ModelName = "Pc", ColumnName = "pc"}},
				{"Pab", new ColumnInformation() { DataType = "string", ModelName = "Pab", ColumnName = "pab"}},
				{"Pac", new ColumnInformation() { DataType = "string", ModelName = "Pac", ColumnName = "pac"}},
			};

	public override string ToString() 
	{
		return "wndba.tmeter_emulation";
	}
}
}
