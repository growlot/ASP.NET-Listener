// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class CircuitTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner { get; } = "OWNER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site { get; } = "SITE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit { get; } = "CIRCUIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CircuitDesc { get; } = "CIRCUIT_DESC";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Longitude { get; } = "LONGITUDE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Latitude { get; } = "LATITUDE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "MOD_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionId { get; } = "transaction_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceLocation { get; } = "SERVICE_LOCATION";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceVoltage { get; } = "SERVICE_VOLTAGE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceAmps { get; } = "SERVICE_AMPS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServicePhase { get; } = "SERVICE_PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceWire { get; } = "SERVICE_WIRE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireType { get; } = "WIRE_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireSize { get; } = "WIRE_SIZE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConductorsPerPhase { get; } = "CONDUCTORS_PER_PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireLocation { get; } = "WIRE_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnclosureType { get; } = "ENCLOSURE_TYPE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallDate { get; } = "INSTALL_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id { get; } = "ID";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServicePoint { get; } = "service_point";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterPoint { get; } = "meter_point";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HasBracket { get; } = "has_bracket";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflDesiredAccDel { get; } = "sfl_desired_acc_del";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllDesiredAccDel { get; } = "sll_desired_acc_del";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfDesiredAccDel { get; } = "spf_desired_acc_del";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SflDesiredAccRec { get; } = "sfl_desired_acc_rec";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SllDesiredAccRec { get; } = "sll_desired_acc_rec";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpfDesiredAccRec { get; } = "spf_desired_acc_rec";
	
	public string RealTableName
	{
		get { return "TCIRCUIT".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "SITE"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "CIRCUIT"}},
				{"CircuitDesc", new ColumnInformation() { DataType = "string", ModelName = "CircuitDesc", ColumnName = "CIRCUIT_DESC"}},
				{"Longitude", new ColumnInformation() { DataType = "double", ModelName = "Longitude", ColumnName = "LONGITUDE"}},
				{"Latitude", new ColumnInformation() { DataType = "double", ModelName = "Latitude", ColumnName = "LATITUDE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"ServiceLocation", new ColumnInformation() { DataType = "string", ModelName = "ServiceLocation", ColumnName = "SERVICE_LOCATION"}},
				{"ServiceVoltage", new ColumnInformation() { DataType = "double", ModelName = "ServiceVoltage", ColumnName = "SERVICE_VOLTAGE"}},
				{"ServiceAmps", new ColumnInformation() { DataType = "double", ModelName = "ServiceAmps", ColumnName = "SERVICE_AMPS"}},
				{"ServicePhase", new ColumnInformation() { DataType = "string", ModelName = "ServicePhase", ColumnName = "SERVICE_PHASE"}},
				{"ServiceWire", new ColumnInformation() { DataType = "string", ModelName = "ServiceWire", ColumnName = "SERVICE_WIRE"}},
				{"WireType", new ColumnInformation() { DataType = "string", ModelName = "WireType", ColumnName = "WIRE_TYPE"}},
				{"WireSize", new ColumnInformation() { DataType = "string", ModelName = "WireSize", ColumnName = "WIRE_SIZE"}},
				{"ConductorsPerPhase", new ColumnInformation() { DataType = "int", ModelName = "ConductorsPerPhase", ColumnName = "CONDUCTORS_PER_PHASE"}},
				{"WireLocation", new ColumnInformation() { DataType = "string", ModelName = "WireLocation", ColumnName = "WIRE_LOCATION"}},
				{"EnclosureType", new ColumnInformation() { DataType = "string", ModelName = "EnclosureType", ColumnName = "ENCLOSURE_TYPE"}},
				{"InstallDate", new ColumnInformation() { DataType = "DateTime", ModelName = "InstallDate", ColumnName = "INSTALL_DATE"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
				{"ServicePoint", new ColumnInformation() { DataType = "string", ModelName = "ServicePoint", ColumnName = "service_point"}},
				{"MeterPoint", new ColumnInformation() { DataType = "string", ModelName = "MeterPoint", ColumnName = "meter_point"}},
				{"HasBracket", new ColumnInformation() { DataType = "string", ModelName = "HasBracket", ColumnName = "has_bracket"}},
				{"SflDesiredAccDel", new ColumnInformation() { DataType = "decimal", ModelName = "SflDesiredAccDel", ColumnName = "sfl_desired_acc_del"}},
				{"SllDesiredAccDel", new ColumnInformation() { DataType = "decimal", ModelName = "SllDesiredAccDel", ColumnName = "sll_desired_acc_del"}},
				{"SpfDesiredAccDel", new ColumnInformation() { DataType = "decimal", ModelName = "SpfDesiredAccDel", ColumnName = "spf_desired_acc_del"}},
				{"SflDesiredAccRec", new ColumnInformation() { DataType = "decimal", ModelName = "SflDesiredAccRec", ColumnName = "sfl_desired_acc_rec"}},
				{"SllDesiredAccRec", new ColumnInformation() { DataType = "decimal", ModelName = "SllDesiredAccRec", ColumnName = "sll_desired_acc_rec"}},
				{"SpfDesiredAccRec", new ColumnInformation() { DataType = "decimal", ModelName = "SpfDesiredAccRec", ColumnName = "spf_desired_acc_rec"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TCIRCUIT";
	}
}
}
#pragma warning restore 1591
