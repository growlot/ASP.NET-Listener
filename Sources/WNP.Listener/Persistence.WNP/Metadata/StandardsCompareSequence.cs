// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class StandardsCompareSequenceTable: ITableInformation {
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
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element { get; } = "element";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DeliveredReceived { get; } = "delivered_received";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVoltage { get; } = "test_voltage";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestCurrent { get; } = "test_current";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle { get; } = "phase_angle";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Frequency { get; } = "frequency";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestService { get; } = "test_service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMode { get; } = "energy_mode";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit { get; } = "upper_limit";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit { get; } = "lower_limit";
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
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime { get; } = "test_time";
	
	public string RealTableName
	{
		get { return "tstandards_compare_sequence".ToUpperInvariant(); }		
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
		return "WNDBA.TSTANDARDS_COMPARE_SEQUENCE";
	}
}
}
#pragma warning restore 1591
