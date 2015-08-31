using System.Collections.Generic;
public class PtTestResultsImpl: ITableInformation {
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
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccuracyClass = "ACCURACY_CLASS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase = "PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LoadLabel = "LOAD_LABEL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PercentLoad = "PERCENT_LOAD";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PercentChange = "PERCENT_CHANGE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Accuracy = "ACCURACY";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatioResult = "RATIO_RESULT";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVoltage = "TEST_VOLTAGE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SecondaryVoltage = "SECONDARY_VOLTAGE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectedRatio = "SELECTED_RATIO";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseError = "PHASE_ERROR";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BurdenValue = "BURDEN_VALUE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Rcf = "RCF";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStop = "TEST_DATE_STOP";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InLimits = "IN_LIMITS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PriTestReason = "PRI_TEST_REASON";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SecTestReason = "SEC_TEST_REASON";
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
	public string BoardNo = "BOARD_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestLocation = "TEST_LOCATION";
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
	public string BatchNo = "BATCH_NO";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "shop_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser01 = "results_user01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser02 = "results_user02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser03 = "results_user03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser04 = "results_user04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser05 = "results_user05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser06 = "results_user06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser07 = "results_user07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser08 = "results_user08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser09 = "results_user09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ResultsUser10 = "results_user10";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTag = "PROCESS_TAG";
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
	
	public string RealTableName
	{
		get { return "TPT_TEST_RESULTS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"TestDateStart", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStart", ColumnName = "TEST_DATE_START"}},
				{"StepNo", new ColumnInformation() { DataType = "int", ModelName = "StepNo", ColumnName = "STEP_NO"}},
				{"AccuracyClass", new ColumnInformation() { DataType = "decimal", ModelName = "AccuracyClass", ColumnName = "ACCURACY_CLASS"}},
				{"Phase", new ColumnInformation() { DataType = "string", ModelName = "Phase", ColumnName = "PHASE"}},
				{"LoadLabel", new ColumnInformation() { DataType = "string", ModelName = "LoadLabel", ColumnName = "LOAD_LABEL"}},
				{"PercentLoad", new ColumnInformation() { DataType = "string", ModelName = "PercentLoad", ColumnName = "PERCENT_LOAD"}},
				{"PercentChange", new ColumnInformation() { DataType = "double", ModelName = "PercentChange", ColumnName = "PERCENT_CHANGE"}},
				{"Accuracy", new ColumnInformation() { DataType = "double", ModelName = "Accuracy", ColumnName = "ACCURACY"}},
				{"RatioResult", new ColumnInformation() { DataType = "double", ModelName = "RatioResult", ColumnName = "RATIO_RESULT"}},
				{"TestVoltage", new ColumnInformation() { DataType = "double", ModelName = "TestVoltage", ColumnName = "TEST_VOLTAGE"}},
				{"SecondaryVoltage", new ColumnInformation() { DataType = "double", ModelName = "SecondaryVoltage", ColumnName = "SECONDARY_VOLTAGE"}},
				{"SelectedRatio", new ColumnInformation() { DataType = "string", ModelName = "SelectedRatio", ColumnName = "SELECTED_RATIO"}},
				{"PhaseError", new ColumnInformation() { DataType = "double", ModelName = "PhaseError", ColumnName = "PHASE_ERROR"}},
				{"BurdenValue", new ColumnInformation() { DataType = "string", ModelName = "BurdenValue", ColumnName = "BURDEN_VALUE"}},
				{"Rcf", new ColumnInformation() { DataType = "double", ModelName = "Rcf", ColumnName = "RCF"}},
				{"TestDateStop", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStop", ColumnName = "TEST_DATE_STOP"}},
				{"InLimits", new ColumnInformation() { DataType = "string", ModelName = "InLimits", ColumnName = "IN_LIMITS"}},
				{"PriTestReason", new ColumnInformation() { DataType = "string", ModelName = "PriTestReason", ColumnName = "PRI_TEST_REASON"}},
				{"SecTestReason", new ColumnInformation() { DataType = "string", ModelName = "SecTestReason", ColumnName = "SEC_TEST_REASON"}},
				{"TesterId", new ColumnInformation() { DataType = "string", ModelName = "TesterId", ColumnName = "TESTER_ID"}},
				{"BoardNo", new ColumnInformation() { DataType = "string", ModelName = "BoardNo", ColumnName = "BOARD_NO"}},
				{"TestLocation", new ColumnInformation() { DataType = "string", ModelName = "TestLocation", ColumnName = "TEST_LOCATION"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"BatchNo", new ColumnInformation() { DataType = "string", ModelName = "BatchNo", ColumnName = "BATCH_NO"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "shop_cycle"}},
				{"ResultsUser01", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser01", ColumnName = "results_user01"}},
				{"ResultsUser02", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser02", ColumnName = "results_user02"}},
				{"ResultsUser03", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser03", ColumnName = "results_user03"}},
				{"ResultsUser04", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser04", ColumnName = "results_user04"}},
				{"ResultsUser05", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser05", ColumnName = "results_user05"}},
				{"ResultsUser06", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser06", ColumnName = "results_user06"}},
				{"ResultsUser07", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser07", ColumnName = "results_user07"}},
				{"ResultsUser08", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser08", ColumnName = "results_user08"}},
				{"ResultsUser09", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser09", ColumnName = "results_user09"}},
				{"ResultsUser10", new ColumnInformation() { DataType = "string", ModelName = "ResultsUser10", ColumnName = "results_user10"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
				{"ProcessTag", new ColumnInformation() { DataType = "string", ModelName = "ProcessTag", ColumnName = "PROCESS_TAG"}},
				{"UpperLimit", new ColumnInformation() { DataType = "double", ModelName = "UpperLimit", ColumnName = "upper_limit"}},
				{"LowerLimit", new ColumnInformation() { DataType = "double", ModelName = "LowerLimit", ColumnName = "lower_limit"}},
				{"TROUBLE1", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE1", ColumnName = "TROUBLE1"}},
				{"TROUBLE2", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE2", ColumnName = "TROUBLE2"}},
				{"TROUBLE3", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE3", ColumnName = "TROUBLE3"}},
				{"TROUBLE4", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE4", ColumnName = "TROUBLE4"}},
				{"TROUBLE5", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE5", ColumnName = "TROUBLE5"}},
			};

	public override string ToString() 
	{
		return "wndba.tpt_test_results";
	}
}
