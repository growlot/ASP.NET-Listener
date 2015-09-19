using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MeterTestSequenceImpl: ITableInformation {
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
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element = "element";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestType = "test_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestRevs = "test_revs";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TimeRev = "time_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReversePower = "reverse_power";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVoltage = "test_voltage";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestCurrent = "test_current";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle = "phase_angle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestService = "test_service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMode = "energy_mode";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency = "frequency";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Optics = "optics";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit = "upper_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit = "lower_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DesiredAccuracy = "desired_accuracy";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Commands = "commands";
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
	public string Kh = "kh";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime = "test_time";
	
	public string RealTableName
	{
		get { return "tmeter_test_sequence".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"SequenceDescription", new ColumnInformation() { DataType = "string", ModelName = "SequenceDescription", ColumnName = "sequence_description"}},
				{"StepNo", new ColumnInformation() { DataType = "int", ModelName = "StepNo", ColumnName = "step_no"}},
				{"Element", new ColumnInformation() { DataType = "string", ModelName = "Element", ColumnName = "element"}},
				{"TestType", new ColumnInformation() { DataType = "string", ModelName = "TestType", ColumnName = "test_type"}},
				{"TestRevs", new ColumnInformation() { DataType = "int", ModelName = "TestRevs", ColumnName = "test_revs"}},
				{"TimeRev", new ColumnInformation() { DataType = "string", ModelName = "TimeRev", ColumnName = "time_rev"}},
				{"ReversePower", new ColumnInformation() { DataType = "string", ModelName = "ReversePower", ColumnName = "reverse_power"}},
				{"TestVoltage", new ColumnInformation() { DataType = "decimal", ModelName = "TestVoltage", ColumnName = "test_voltage"}},
				{"TestCurrent", new ColumnInformation() { DataType = "decimal", ModelName = "TestCurrent", ColumnName = "test_current"}},
				{"PhaseAngle", new ColumnInformation() { DataType = "decimal", ModelName = "PhaseAngle", ColumnName = "phase_angle"}},
				{"TestService", new ColumnInformation() { DataType = "string", ModelName = "TestService", ColumnName = "test_service"}},
				{"EnergyMode", new ColumnInformation() { DataType = "string", ModelName = "EnergyMode", ColumnName = "energy_mode"}},
				{"Frequency", new ColumnInformation() { DataType = "decimal", ModelName = "Frequency", ColumnName = "frequency"}},
				{"Optics", new ColumnInformation() { DataType = "string", ModelName = "Optics", ColumnName = "optics"}},
				{"UpperLimit", new ColumnInformation() { DataType = "decimal", ModelName = "UpperLimit", ColumnName = "upper_limit"}},
				{"LowerLimit", new ColumnInformation() { DataType = "decimal", ModelName = "LowerLimit", ColumnName = "lower_limit"}},
				{"DesiredAccuracy", new ColumnInformation() { DataType = "decimal", ModelName = "DesiredAccuracy", ColumnName = "desired_accuracy"}},
				{"Commands", new ColumnInformation() { DataType = "string", ModelName = "Commands", ColumnName = "commands"}},
				{"HarmonicConfiguration", new ColumnInformation() { DataType = "string", ModelName = "HarmonicConfiguration", ColumnName = "harmonic_configuration"}},
				{"HarmonicRevision", new ColumnInformation() { DataType = "int", ModelName = "HarmonicRevision", ColumnName = "harmonic_revision"}},
				{"Kh", new ColumnInformation() { DataType = "string", ModelName = "Kh", ColumnName = "kh"}},
				{"TestTime", new ColumnInformation() { DataType = "int", ModelName = "TestTime", ColumnName = "test_time"}},
			};

	public override string ToString() 
	{
		return "wndba.tmeter_test_sequence";
	}
}
}
