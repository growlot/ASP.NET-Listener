using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TransducerTestResultsImpl: ITableInformation {
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
	public string SiteName = "site_name";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStart = "test_date_start";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStop = "test_date_stop";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string StepNo = "step_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase = "phase";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Volts = "volts";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Amps = "amps";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle = "phase_angle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtRatio = "ct_ratio";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PtRatio = "pt_ratio";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDuration = "test_duration";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Af = "af";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Al = "al";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceType = "service_type";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestConditionXdcr = "test_condition_xdcr";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestConditionPrimary = "test_condition_primary";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScadaMwAf = "scada_mw_af";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScadaMwAl = "scada_mw_al";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScadaBitsAl = "scada_bits_al";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency = "frequency";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestConditionXdcrAl = "test_condition_xdcr_al";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestConditionPrimaryAl = "test_condition_primary_al";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CompanyNo = "company_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Model = "model";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Manufacturer = "manufacturer";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit = "upper_limit";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit = "lower_limit";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Type = "type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Output = "output";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Scale = "scale";
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
	public string BatchNo = "BATCH_NO";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "shop_cycle";
	
	public string RealTableName
	{
		get { return "ttransducer_test_results".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"SiteName", new ColumnInformation() { DataType = "string", ModelName = "SiteName", ColumnName = "site_name"}},
				{"TestDateStart", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStart", ColumnName = "test_date_start"}},
				{"TestDateStop", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStop", ColumnName = "test_date_stop"}},
				{"StepNo", new ColumnInformation() { DataType = "int", ModelName = "StepNo", ColumnName = "step_no"}},
				{"Phase", new ColumnInformation() { DataType = "string", ModelName = "Phase", ColumnName = "phase"}},
				{"Volts", new ColumnInformation() { DataType = "double", ModelName = "Volts", ColumnName = "volts"}},
				{"Amps", new ColumnInformation() { DataType = "double", ModelName = "Amps", ColumnName = "amps"}},
				{"PhaseAngle", new ColumnInformation() { DataType = "double", ModelName = "PhaseAngle", ColumnName = "phase_angle"}},
				{"CtRatio", new ColumnInformation() { DataType = "string", ModelName = "CtRatio", ColumnName = "ct_ratio"}},
				{"PtRatio", new ColumnInformation() { DataType = "string", ModelName = "PtRatio", ColumnName = "pt_ratio"}},
				{"TestDuration", new ColumnInformation() { DataType = "int", ModelName = "TestDuration", ColumnName = "test_duration"}},
				{"Af", new ColumnInformation() { DataType = "double", ModelName = "Af", ColumnName = "af"}},
				{"Al", new ColumnInformation() { DataType = "double", ModelName = "Al", ColumnName = "al"}},
				{"ServiceType", new ColumnInformation() { DataType = "string", ModelName = "ServiceType", ColumnName = "service_type"}},
				{"TestConditionXdcr", new ColumnInformation() { DataType = "double", ModelName = "TestConditionXdcr", ColumnName = "test_condition_xdcr"}},
				{"TestConditionPrimary", new ColumnInformation() { DataType = "double", ModelName = "TestConditionPrimary", ColumnName = "test_condition_primary"}},
				{"ScadaMwAf", new ColumnInformation() { DataType = "double", ModelName = "ScadaMwAf", ColumnName = "scada_mw_af"}},
				{"ScadaMwAl", new ColumnInformation() { DataType = "double", ModelName = "ScadaMwAl", ColumnName = "scada_mw_al"}},
				{"ScadaBitsAl", new ColumnInformation() { DataType = "double", ModelName = "ScadaBitsAl", ColumnName = "scada_bits_al"}},
				{"Frequency", new ColumnInformation() { DataType = "double", ModelName = "Frequency", ColumnName = "frequency"}},
				{"TestConditionXdcrAl", new ColumnInformation() { DataType = "double", ModelName = "TestConditionXdcrAl", ColumnName = "test_condition_xdcr_al"}},
				{"TestConditionPrimaryAl", new ColumnInformation() { DataType = "double", ModelName = "TestConditionPrimaryAl", ColumnName = "test_condition_primary_al"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"CompanyNo", new ColumnInformation() { DataType = "string", ModelName = "CompanyNo", ColumnName = "company_no"}},
				{"Model", new ColumnInformation() { DataType = "string", ModelName = "Model", ColumnName = "model"}},
				{"Manufacturer", new ColumnInformation() { DataType = "string", ModelName = "Manufacturer", ColumnName = "manufacturer"}},
				{"UpperLimit", new ColumnInformation() { DataType = "double", ModelName = "UpperLimit", ColumnName = "upper_limit"}},
				{"LowerLimit", new ColumnInformation() { DataType = "double", ModelName = "LowerLimit", ColumnName = "lower_limit"}},
				{"Type", new ColumnInformation() { DataType = "string", ModelName = "Type", ColumnName = "type"}},
				{"Output", new ColumnInformation() { DataType = "string", ModelName = "Output", ColumnName = "output"}},
				{"Scale", new ColumnInformation() { DataType = "string", ModelName = "Scale", ColumnName = "scale"}},
				{"WecoSn", new ColumnInformation() { DataType = "string", ModelName = "WecoSn", ColumnName = "weco_sn"}},
				{"StandardSn", new ColumnInformation() { DataType = "string", ModelName = "StandardSn", ColumnName = "standard_sn"}},
				{"BatchNo", new ColumnInformation() { DataType = "string", ModelName = "BatchNo", ColumnName = "BATCH_NO"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "shop_cycle"}},
			};

	public override string ToString() 
	{
		return "wndba.ttransducer_test_results";
	}
}
}
