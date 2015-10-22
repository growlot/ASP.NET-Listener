// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MeterTestSequenceTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string SequenceDescription { get; } = "sequence_description";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string StepNo { get; } = "step_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element { get; } = "element";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestType { get; } = "test_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestRevs { get; } = "test_revs";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TimeRev { get; } = "time_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReversePower { get; } = "reverse_power";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVoltage { get; } = "test_voltage";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestCurrent { get; } = "test_current";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle { get; } = "phase_angle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestService { get; } = "test_service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMode { get; } = "energy_mode";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency { get; } = "frequency";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Optics { get; } = "optics";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit { get; } = "upper_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit { get; } = "lower_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DesiredAccuracy { get; } = "desired_accuracy";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Commands { get; } = "commands";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HarmonicConfiguration { get; } = "harmonic_configuration";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HarmonicRevision { get; } = "harmonic_revision";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Kh { get; } = "kh";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime { get; } = "test_time";
	
	public string RealTableName
	{
		get { return "tmeter_test_sequence".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
#pragma warning restore 1591
