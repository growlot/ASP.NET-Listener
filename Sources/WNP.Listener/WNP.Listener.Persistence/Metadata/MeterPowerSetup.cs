using System.Collections.Generic;
public class MeterPowerSetupImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string FormNo = "FORM_NO";
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
	public string Service = "SERVICE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SeriesStdWeight1 = "SERIES_STD_WEIGHT_1";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SeriesStdWeight2 = "SERIES_STD_WEIGHT_2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SeriesStdWeight3 = "SERIES_STD_WEIGHT_3";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LeftStdWeight1 = "LEFT_STD_WEIGHT_1";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LeftStdWeight2 = "LEFT_STD_WEIGHT_2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LeftStdWeight3 = "LEFT_STD_WEIGHT_3";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CenterStdWeight1 = "CENTER_STD_WEIGHT_1";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CenterStdWeight2 = "CENTER_STD_WEIGHT_2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CenterStdWeight3 = "CENTER_STD_WEIGHT_3";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RightStdWeight1 = "RIGHT_STD_WEIGHT_1";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RightStdWeight2 = "RIGHT_STD_WEIGHT_2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RightStdWeight3 = "RIGHT_STD_WEIGHT_3";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaMult = "VA_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaPhase = "VA_PHASE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VbMult = "VB_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VbPhase = "VB_PHASE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VcMult = "VC_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VcPhase = "VC_PHASE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IaMult = "IA_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IaPhase = "IA_PHASE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IbMult = "IB_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IbPhase = "IB_PHASE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IcMult = "IC_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IcPhase = "IC_PHASE";
	
	public string RealTableName
	{
		get { return "TMETER_POWER_SETUP".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"FormNo", new ColumnInformation() { DataType = "string", ModelName = "FormNo", ColumnName = "FORM_NO"}},
				{"Base", new ColumnInformation() { DataType = "string", ModelName = "Base", ColumnName = "base"}},
				{"Service", new ColumnInformation() { DataType = "string", ModelName = "Service", ColumnName = "SERVICE"}},
				{"SeriesStdWeight1", new ColumnInformation() { DataType = "decimal", ModelName = "SeriesStdWeight1", ColumnName = "SERIES_STD_WEIGHT_1"}},
				{"SeriesStdWeight2", new ColumnInformation() { DataType = "decimal", ModelName = "SeriesStdWeight2", ColumnName = "SERIES_STD_WEIGHT_2"}},
				{"SeriesStdWeight3", new ColumnInformation() { DataType = "decimal", ModelName = "SeriesStdWeight3", ColumnName = "SERIES_STD_WEIGHT_3"}},
				{"LeftStdWeight1", new ColumnInformation() { DataType = "decimal", ModelName = "LeftStdWeight1", ColumnName = "LEFT_STD_WEIGHT_1"}},
				{"LeftStdWeight2", new ColumnInformation() { DataType = "decimal", ModelName = "LeftStdWeight2", ColumnName = "LEFT_STD_WEIGHT_2"}},
				{"LeftStdWeight3", new ColumnInformation() { DataType = "decimal", ModelName = "LeftStdWeight3", ColumnName = "LEFT_STD_WEIGHT_3"}},
				{"CenterStdWeight1", new ColumnInformation() { DataType = "decimal", ModelName = "CenterStdWeight1", ColumnName = "CENTER_STD_WEIGHT_1"}},
				{"CenterStdWeight2", new ColumnInformation() { DataType = "decimal", ModelName = "CenterStdWeight2", ColumnName = "CENTER_STD_WEIGHT_2"}},
				{"CenterStdWeight3", new ColumnInformation() { DataType = "decimal", ModelName = "CenterStdWeight3", ColumnName = "CENTER_STD_WEIGHT_3"}},
				{"RightStdWeight1", new ColumnInformation() { DataType = "decimal", ModelName = "RightStdWeight1", ColumnName = "RIGHT_STD_WEIGHT_1"}},
				{"RightStdWeight2", new ColumnInformation() { DataType = "decimal", ModelName = "RightStdWeight2", ColumnName = "RIGHT_STD_WEIGHT_2"}},
				{"RightStdWeight3", new ColumnInformation() { DataType = "decimal", ModelName = "RightStdWeight3", ColumnName = "RIGHT_STD_WEIGHT_3"}},
				{"VaMult", new ColumnInformation() { DataType = "decimal", ModelName = "VaMult", ColumnName = "VA_MULT"}},
				{"VaPhase", new ColumnInformation() { DataType = "decimal", ModelName = "VaPhase", ColumnName = "VA_PHASE"}},
				{"VbMult", new ColumnInformation() { DataType = "decimal", ModelName = "VbMult", ColumnName = "VB_MULT"}},
				{"VbPhase", new ColumnInformation() { DataType = "decimal", ModelName = "VbPhase", ColumnName = "VB_PHASE"}},
				{"VcMult", new ColumnInformation() { DataType = "decimal", ModelName = "VcMult", ColumnName = "VC_MULT"}},
				{"VcPhase", new ColumnInformation() { DataType = "decimal", ModelName = "VcPhase", ColumnName = "VC_PHASE"}},
				{"IaMult", new ColumnInformation() { DataType = "decimal", ModelName = "IaMult", ColumnName = "IA_MULT"}},
				{"IaPhase", new ColumnInformation() { DataType = "decimal", ModelName = "IaPhase", ColumnName = "IA_PHASE"}},
				{"IbMult", new ColumnInformation() { DataType = "decimal", ModelName = "IbMult", ColumnName = "IB_MULT"}},
				{"IbPhase", new ColumnInformation() { DataType = "decimal", ModelName = "IbPhase", ColumnName = "IB_PHASE"}},
				{"IcMult", new ColumnInformation() { DataType = "decimal", ModelName = "IcMult", ColumnName = "IC_MULT"}},
				{"IcPhase", new ColumnInformation() { DataType = "decimal", ModelName = "IcPhase", ColumnName = "IC_PHASE"}},
			};

	public override string ToString() 
	{
		return "wndba.tmeter_power_setup";
	}
}
