using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SiteWiringCheckImpl: ITableInformation {
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
	public string TestDate = "test_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceCheck = "service_check";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseACheck = "phase_a_check";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseBCheck = "phase_b_check";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseCCheck = "phase_c_check";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Information = "information";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Va = "va";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Vb = "vb";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Vc = "vc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ia = "ia";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ib = "ib";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ic = "ic";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pa = "pa";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pb = "pb";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pc = "pc";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pab = "pab";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Pac = "pac";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Direction = "direction";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceWires = "service_wires";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PaRelative = "pa_relative";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbRelative = "pb_relative";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PcRelative = "pc_relative";
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
		get { return "tsite_wiring_check".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"TestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDate", ColumnName = "test_date"}},
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"ServiceCheck", new ColumnInformation() { DataType = "string", ModelName = "ServiceCheck", ColumnName = "service_check"}},
				{"PhaseACheck", new ColumnInformation() { DataType = "string", ModelName = "PhaseACheck", ColumnName = "phase_a_check"}},
				{"PhaseBCheck", new ColumnInformation() { DataType = "string", ModelName = "PhaseBCheck", ColumnName = "phase_b_check"}},
				{"PhaseCCheck", new ColumnInformation() { DataType = "string", ModelName = "PhaseCCheck", ColumnName = "phase_c_check"}},
				{"Information", new ColumnInformation() { DataType = "string", ModelName = "Information", ColumnName = "information"}},
				{"Va", new ColumnInformation() { DataType = "decimal", ModelName = "Va", ColumnName = "va"}},
				{"Vb", new ColumnInformation() { DataType = "decimal", ModelName = "Vb", ColumnName = "vb"}},
				{"Vc", new ColumnInformation() { DataType = "decimal", ModelName = "Vc", ColumnName = "vc"}},
				{"Ia", new ColumnInformation() { DataType = "decimal", ModelName = "Ia", ColumnName = "ia"}},
				{"Ib", new ColumnInformation() { DataType = "decimal", ModelName = "Ib", ColumnName = "ib"}},
				{"Ic", new ColumnInformation() { DataType = "decimal", ModelName = "Ic", ColumnName = "ic"}},
				{"Pa", new ColumnInformation() { DataType = "decimal", ModelName = "Pa", ColumnName = "pa"}},
				{"Pb", new ColumnInformation() { DataType = "decimal", ModelName = "Pb", ColumnName = "pb"}},
				{"Pc", new ColumnInformation() { DataType = "decimal", ModelName = "Pc", ColumnName = "pc"}},
				{"Pab", new ColumnInformation() { DataType = "decimal", ModelName = "Pab", ColumnName = "pab"}},
				{"Pac", new ColumnInformation() { DataType = "decimal", ModelName = "Pac", ColumnName = "pac"}},
				{"Direction", new ColumnInformation() { DataType = "string", ModelName = "Direction", ColumnName = "direction"}},
				{"ServiceWires", new ColumnInformation() { DataType = "int", ModelName = "ServiceWires", ColumnName = "service_wires"}},
				{"PaRelative", new ColumnInformation() { DataType = "decimal", ModelName = "PaRelative", ColumnName = "pa_relative"}},
				{"PbRelative", new ColumnInformation() { DataType = "decimal", ModelName = "PbRelative", ColumnName = "pb_relative"}},
				{"PcRelative", new ColumnInformation() { DataType = "decimal", ModelName = "PcRelative", ColumnName = "pc_relative"}},
				{"WecoSn", new ColumnInformation() { DataType = "string", ModelName = "WecoSn", ColumnName = "weco_sn"}},
				{"StandardSn", new ColumnInformation() { DataType = "string", ModelName = "StandardSn", ColumnName = "standard_sn"}},
				{"TesterId", new ColumnInformation() { DataType = "string", ModelName = "TesterId", ColumnName = "tester_id"}},
			};

	public override string ToString() 
	{
		return "wndba.tsite_wiring_check";
	}
}
}
