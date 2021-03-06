// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class ReferenceTestResultsTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo { get; } = "eqp_no";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDateStart { get; } = "test_date_start";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestStep { get; } = "test_step";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Element { get; } = "element";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVolts { get; } = "test_volts";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestAmps { get; } = "test_amps";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseAngle { get; } = "phase_angle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyDirection { get; } = "energy_direction";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceType { get; } = "service_type";
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
	public string StandardMode { get; } = "standard_mode";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestTime { get; } = "test_time";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Accuracy { get; } = "accuracy";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit { get; } = "upper_limit";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit { get; } = "lower_limit";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string XferStdOwner { get; } = "xfer_std_owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string XferStdEqpNo { get; } = "xfer_std_eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestEqpNo { get; } = "test_eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StationId { get; } = "station_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TesterId { get; } = "tester_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestType { get; } = "test_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PositionId { get; } = "position_id";
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
	public string ExternalRefFlag { get; } = "external_ref_flag";
	
	public string RealTableName
	{
		get { return "TREFERENCE_TEST_RESULTS".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "WNDBA.TREFERENCE_TEST_RESULTS";
	}
}
}
#pragma warning restore 1591
