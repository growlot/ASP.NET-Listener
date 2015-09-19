using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class CircuitHistImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site = "site";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "circuit";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteDate = "site_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CircuitDesc = "circuit_desc";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Longitude = "longitude";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Latitude = "latitude";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionId = "transaction_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceLocation = "SERVICE_LOCATION";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceVoltage = "SERVICE_VOLTAGE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceAmps = "SERVICE_AMPS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServicePhase = "SERVICE_PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceWire = "SERVICE_WIRE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireType = "WIRE_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireSize = "WIRE_SIZE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConductorsPerPhase = "CONDUCTORS_PER_PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WireLocation = "WIRE_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnclosureType = "ENCLOSURE_TYPE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallDate = "INSTALL_DATE";
	
	public string RealTableName
	{
		get { return "TCIRCUIT_HIST".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "circuit"}},
				{"SiteDate", new ColumnInformation() { DataType = "DateTime", ModelName = "SiteDate", ColumnName = "site_date"}},
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"CircuitDesc", new ColumnInformation() { DataType = "string", ModelName = "CircuitDesc", ColumnName = "circuit_desc"}},
				{"Longitude", new ColumnInformation() { DataType = "double", ModelName = "Longitude", ColumnName = "longitude"}},
				{"Latitude", new ColumnInformation() { DataType = "double", ModelName = "Latitude", ColumnName = "latitude"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
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
			};

	public override string ToString() 
	{
		return "wndba.tcircuit_hist";
	}
}
}
