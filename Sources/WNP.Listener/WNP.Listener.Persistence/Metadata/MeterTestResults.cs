using System.Collections.Generic;
public class MeterTestResultsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStart = "TEST_DATE_START";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string StepNo = "STEP_NO";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStop = "TEST_DATE_STOP";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestLocation = "TEST_LOCATION";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site = "SITE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "CIRCUIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CustLoadTest = "CUST_LOAD_TEST";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestReason = "TEST_REASON";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element = "ELEMENT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestType = "TEST_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReversePower = "REVERSE_POWER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestRevs = "TEST_REVS";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit = "UPPER_LIMIT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit = "LOWER_LIMIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceType = "SERVICE_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardMode = "STANDARD_MODE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle = "PHASE_ANGLE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency = "FREQUENCY";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVolts = "TEST_VOLTS";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestAmps = "TEST_AMPS";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Af = "AF";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Al = "AL";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StepDuration = "STEP_DURATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccuracyStatus = "ACCURACY_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TesterId = "TESTER_ID";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StationId = "STATION_ID";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WecoSn = "WECO_SN";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardSn = "STANDARD_SN";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Optics = "OPTICS";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DesiredAccuracy = "DESIRED_ACCURACY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HarmonicConfiguration = "harmonic_configuration";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HarmonicRevision = "harmonic_revision";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardSn2 = "standard_sn2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardSn3 = "standard_sn3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Kh = "kh";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser01 = "RESULTS_USER01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser02 = "RESULTS_USER02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser03 = "RESULTS_USER03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser04 = "RESULTS_USER04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser05 = "RESULTS_USER05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser06 = "RESULTS_USER06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser07 = "RESULTS_USER07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser08 = "RESULTS_USER08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser09 = "RESULTS_USER09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser10 = "RESULTS_USER10";
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
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ImpedPhaseA = "imped_phase_a";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ImpedPhaseB = "imped_phase_b";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ImpedPhaseC = "imped_phase_c";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaPhaseA = "va_phase_a";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaPhaseB = "va_phase_b";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaPhaseC = "va_phase_c";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTag = "PROCESS_TAG";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE1 = "TROUBLE1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE2 = "TROUBLE2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE3 = "TROUBLE3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE4 = "TROUBLE4";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE5 = "TROUBLE5";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
	
	public string RealTableName
	{
		get { return "TMETER_TEST_RESULTS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"TestDateStart", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStart", ColumnName = "TEST_DATE_START"}},
				{"StepNo", new ColumnInformation() { DataType = "int", ModelName = "StepNo", ColumnName = "STEP_NO"}},
				{"TestDateStop", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStop", ColumnName = "TEST_DATE_STOP"}},
				{"TestLocation", new ColumnInformation() { DataType = "string", ModelName = "TestLocation", ColumnName = "TEST_LOCATION"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "SITE"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "CIRCUIT"}},
				{"CustLoadTest", new ColumnInformation() { DataType = "string", ModelName = "CustLoadTest", ColumnName = "CUST_LOAD_TEST"}},
				{"TestReason", new ColumnInformation() { DataType = "string", ModelName = "TestReason", ColumnName = "TEST_REASON"}},
				{"Element", new ColumnInformation() { DataType = "string", ModelName = "Element", ColumnName = "ELEMENT"}},
				{"TestType", new ColumnInformation() { DataType = "string", ModelName = "TestType", ColumnName = "TEST_TYPE"}},
				{"ReversePower", new ColumnInformation() { DataType = "string", ModelName = "ReversePower", ColumnName = "REVERSE_POWER"}},
				{"TestRevs", new ColumnInformation() { DataType = "int", ModelName = "TestRevs", ColumnName = "TEST_REVS"}},
				{"UpperLimit", new ColumnInformation() { DataType = "decimal", ModelName = "UpperLimit", ColumnName = "UPPER_LIMIT"}},
				{"LowerLimit", new ColumnInformation() { DataType = "decimal", ModelName = "LowerLimit", ColumnName = "LOWER_LIMIT"}},
				{"ServiceType", new ColumnInformation() { DataType = "string", ModelName = "ServiceType", ColumnName = "SERVICE_TYPE"}},
				{"StandardMode", new ColumnInformation() { DataType = "string", ModelName = "StandardMode", ColumnName = "STANDARD_MODE"}},
				{"PhaseAngle", new ColumnInformation() { DataType = "double", ModelName = "PhaseAngle", ColumnName = "PHASE_ANGLE"}},
				{"Frequency", new ColumnInformation() { DataType = "decimal", ModelName = "Frequency", ColumnName = "FREQUENCY"}},
				{"TestVolts", new ColumnInformation() { DataType = "double", ModelName = "TestVolts", ColumnName = "TEST_VOLTS"}},
				{"TestAmps", new ColumnInformation() { DataType = "double", ModelName = "TestAmps", ColumnName = "TEST_AMPS"}},
				{"Af", new ColumnInformation() { DataType = "double", ModelName = "Af", ColumnName = "AF"}},
				{"Al", new ColumnInformation() { DataType = "double", ModelName = "Al", ColumnName = "AL"}},
				{"StepDuration", new ColumnInformation() { DataType = "int", ModelName = "StepDuration", ColumnName = "STEP_DURATION"}},
				{"AccuracyStatus", new ColumnInformation() { DataType = "string", ModelName = "AccuracyStatus", ColumnName = "ACCURACY_STATUS"}},
				{"TesterId", new ColumnInformation() { DataType = "string", ModelName = "TesterId", ColumnName = "TESTER_ID"}},
				{"StationId", new ColumnInformation() { DataType = "string", ModelName = "StationId", ColumnName = "STATION_ID"}},
				{"WecoSn", new ColumnInformation() { DataType = "string", ModelName = "WecoSn", ColumnName = "WECO_SN"}},
				{"StandardSn", new ColumnInformation() { DataType = "string", ModelName = "StandardSn", ColumnName = "STANDARD_SN"}},
				{"Optics", new ColumnInformation() { DataType = "string", ModelName = "Optics", ColumnName = "OPTICS"}},
				{"DesiredAccuracy", new ColumnInformation() { DataType = "double", ModelName = "DesiredAccuracy", ColumnName = "DESIRED_ACCURACY"}},
				{"HarmonicConfiguration", new ColumnInformation() { DataType = "string", ModelName = "HarmonicConfiguration", ColumnName = "harmonic_configuration"}},
				{"HarmonicRevision", new ColumnInformation() { DataType = "int", ModelName = "HarmonicRevision", ColumnName = "harmonic_revision"}},
				{"StandardSn2", new ColumnInformation() { DataType = "string", ModelName = "StandardSn2", ColumnName = "standard_sn2"}},
				{"StandardSn3", new ColumnInformation() { DataType = "string", ModelName = "StandardSn3", ColumnName = "standard_sn3"}},
				{"Kh", new ColumnInformation() { DataType = "string", ModelName = "Kh", ColumnName = "kh"}},
				{"ResultsUser01", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser01", ColumnName = "RESULTS_USER01"}},
				{"ResultsUser02", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser02", ColumnName = "RESULTS_USER02"}},
				{"ResultsUser03", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser03", ColumnName = "RESULTS_USER03"}},
				{"ResultsUser04", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser04", ColumnName = "RESULTS_USER04"}},
				{"ResultsUser05", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser05", ColumnName = "RESULTS_USER05"}},
				{"ResultsUser06", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser06", ColumnName = "RESULTS_USER06"}},
				{"ResultsUser07", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser07", ColumnName = "RESULTS_USER07"}},
				{"ResultsUser08", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser08", ColumnName = "RESULTS_USER08"}},
				{"ResultsUser09", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser09", ColumnName = "RESULTS_USER09"}},
				{"ResultsUser10", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser10", ColumnName = "RESULTS_USER10"}},
				{"BatchNo", new ColumnInformation() { DataType = "string", ModelName = "BatchNo", ColumnName = "BATCH_NO"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "shop_cycle"}},
				{"ImpedPhaseA", new ColumnInformation() { DataType = "double", ModelName = "ImpedPhaseA", ColumnName = "imped_phase_a"}},
				{"ImpedPhaseB", new ColumnInformation() { DataType = "double", ModelName = "ImpedPhaseB", ColumnName = "imped_phase_b"}},
				{"ImpedPhaseC", new ColumnInformation() { DataType = "double", ModelName = "ImpedPhaseC", ColumnName = "imped_phase_c"}},
				{"VaPhaseA", new ColumnInformation() { DataType = "double", ModelName = "VaPhaseA", ColumnName = "va_phase_a"}},
				{"VaPhaseB", new ColumnInformation() { DataType = "double", ModelName = "VaPhaseB", ColumnName = "va_phase_b"}},
				{"VaPhaseC", new ColumnInformation() { DataType = "double", ModelName = "VaPhaseC", ColumnName = "va_phase_c"}},
				{"ProcessTag", new ColumnInformation() { DataType = "string", ModelName = "ProcessTag", ColumnName = "PROCESS_TAG"}},
				{"TROUBLE1", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE1", ColumnName = "TROUBLE1"}},
				{"TROUBLE2", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE2", ColumnName = "TROUBLE2"}},
				{"TROUBLE3", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE3", ColumnName = "TROUBLE3"}},
				{"TROUBLE4", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE4", ColumnName = "TROUBLE4"}},
				{"TROUBLE5", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE5", ColumnName = "TROUBLE5"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
			};

	public override string ToString() 
	{
		return "wndba.tmeter_test_results";
	}
}
