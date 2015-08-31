using System.Collections.Generic;
public class StandardsCompareSequenceImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string SequenceDescription = "sequence_description";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string StepNo = "step_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element = "element";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DeliveredReceived = "delivered_received";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVoltage = "test_voltage";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestCurrent = "test_current";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle = "phase_angle";
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
	public string TestService = "test_service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMode = "energy_mode";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit = "upper_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit = "lower_limit";
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
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime = "test_time";
	
	public string RealTableName
	{
		get { return "tstandards_compare_sequence".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"SequenceDescription", new ColumnInformation() { DataType = "string", ModelName = "SequenceDescription", ColumnName = "sequence_description"}},
				{"StepNo", new ColumnInformation() { DataType = "int", ModelName = "StepNo", ColumnName = "step_no"}},
				{"Element", new ColumnInformation() { DataType = "string", ModelName = "Element", ColumnName = "element"}},
				{"DeliveredReceived", new ColumnInformation() { DataType = "string", ModelName = "DeliveredReceived", ColumnName = "delivered_received"}},
				{"TestVoltage", new ColumnInformation() { DataType = "double", ModelName = "TestVoltage", ColumnName = "test_voltage"}},
				{"TestCurrent", new ColumnInformation() { DataType = "double", ModelName = "TestCurrent", ColumnName = "test_current"}},
				{"PhaseAngle", new ColumnInformation() { DataType = "double", ModelName = "PhaseAngle", ColumnName = "phase_angle"}},
				{"Frequency", new ColumnInformation() { DataType = "decimal", ModelName = "Frequency", ColumnName = "frequency"}},
				{"TestService", new ColumnInformation() { DataType = "string", ModelName = "TestService", ColumnName = "test_service"}},
				{"EnergyMode", new ColumnInformation() { DataType = "string", ModelName = "EnergyMode", ColumnName = "energy_mode"}},
				{"UpperLimit", new ColumnInformation() { DataType = "decimal", ModelName = "UpperLimit", ColumnName = "upper_limit"}},
				{"LowerLimit", new ColumnInformation() { DataType = "decimal", ModelName = "LowerLimit", ColumnName = "lower_limit"}},
				{"HarmonicConfiguration", new ColumnInformation() { DataType = "string", ModelName = "HarmonicConfiguration", ColumnName = "harmonic_configuration"}},
				{"HarmonicRevision", new ColumnInformation() { DataType = "int", ModelName = "HarmonicRevision", ColumnName = "harmonic_revision"}},
				{"TestTime", new ColumnInformation() { DataType = "int", ModelName = "TestTime", ColumnName = "test_time"}},
			};

	public override string ToString() 
	{
		return "wndba.tstandards_compare_sequence";
	}
}
