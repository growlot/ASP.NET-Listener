using System.Collections.Generic;
public class ReferenceTestResultsImpl: ITableInformation {
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
	public string EqpNo = "eqp_no";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStart = "test_date_start";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestStep = "test_step";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element = "element";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVolts = "test_volts";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestAmps = "test_amps";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle = "phase_angle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyDirection = "energy_direction";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceType = "service_type";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency = "frequency";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StandardMode = "standard_mode";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime = "test_time";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Accuracy = "accuracy";
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
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string XferStdOwner = "xfer_std_owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string XferStdEqpNo = "xfer_std_eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestEqpNo = "test_eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StationId = "station_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TesterId = "tester_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestType = "test_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PositionId = "position_id";
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
	public string ExternalRefFlag = "external_ref_flag";
	
	public string RealTableName
	{
		get { return "TREFERENCE_TEST_RESULTS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"TestDateStart", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDateStart", ColumnName = "test_date_start"}},
				{"TestStep", new ColumnInformation() { DataType = "int", ModelName = "TestStep", ColumnName = "test_step"}},
				{"Element", new ColumnInformation() { DataType = "string", ModelName = "Element", ColumnName = "element"}},
				{"TestVolts", new ColumnInformation() { DataType = "double", ModelName = "TestVolts", ColumnName = "test_volts"}},
				{"TestAmps", new ColumnInformation() { DataType = "double", ModelName = "TestAmps", ColumnName = "test_amps"}},
				{"PhaseAngle", new ColumnInformation() { DataType = "int", ModelName = "PhaseAngle", ColumnName = "phase_angle"}},
				{"EnergyDirection", new ColumnInformation() { DataType = "string", ModelName = "EnergyDirection", ColumnName = "energy_direction"}},
				{"ServiceType", new ColumnInformation() { DataType = "string", ModelName = "ServiceType", ColumnName = "service_type"}},
				{"Frequency", new ColumnInformation() { DataType = "decimal", ModelName = "Frequency", ColumnName = "frequency"}},
				{"StandardMode", new ColumnInformation() { DataType = "string", ModelName = "StandardMode", ColumnName = "standard_mode"}},
				{"TestTime", new ColumnInformation() { DataType = "int", ModelName = "TestTime", ColumnName = "test_time"}},
				{"Accuracy", new ColumnInformation() { DataType = "double", ModelName = "Accuracy", ColumnName = "accuracy"}},
				{"UpperLimit", new ColumnInformation() { DataType = "double", ModelName = "UpperLimit", ColumnName = "upper_limit"}},
				{"LowerLimit", new ColumnInformation() { DataType = "double", ModelName = "LowerLimit", ColumnName = "lower_limit"}},
				{"XferStdOwner", new ColumnInformation() { DataType = "int", ModelName = "XferStdOwner", ColumnName = "xfer_std_owner"}},
				{"XferStdEqpNo", new ColumnInformation() { DataType = "string", ModelName = "XferStdEqpNo", ColumnName = "xfer_std_eqp_no"}},
				{"TestEqpNo", new ColumnInformation() { DataType = "string", ModelName = "TestEqpNo", ColumnName = "test_eqp_no"}},
				{"StationId", new ColumnInformation() { DataType = "string", ModelName = "StationId", ColumnName = "station_id"}},
				{"TesterId", new ColumnInformation() { DataType = "string", ModelName = "TesterId", ColumnName = "tester_id"}},
				{"TestType", new ColumnInformation() { DataType = "string", ModelName = "TestType", ColumnName = "test_type"}},
				{"PositionId", new ColumnInformation() { DataType = "string", ModelName = "PositionId", ColumnName = "position_id"}},
				{"HarmonicConfiguration", new ColumnInformation() { DataType = "string", ModelName = "HarmonicConfiguration", ColumnName = "harmonic_configuration"}},
				{"HarmonicRevision", new ColumnInformation() { DataType = "int", ModelName = "HarmonicRevision", ColumnName = "harmonic_revision"}},
				{"ExternalRefFlag", new ColumnInformation() { DataType = "string", ModelName = "ExternalRefFlag", ColumnName = "external_ref_flag"}},
			};

	public override string ToString() 
	{
		return "wndba.treference_test_results";
	}
}
