using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class CircuitImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site = "SITE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "CIRCUIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CircuitDesc = "CIRCUIT_DESC";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Longitude = "LONGITUDE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Latitude = "LATITUDE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "MOD_DATE";
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
		get { return "TCIRCUIT".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
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
			};

	public override string ToString() 
	{
		return "wndba.tcircuit";
	}
}
}
