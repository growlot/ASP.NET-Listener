// <auto-generated>
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class VehicleImpl: ITableInformation {
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
	public string VehicleId = "vehicle_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VehicleDescription = "vehicle_description";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "location";
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
	
	public string RealTableName
	{
		get { return "tvehicle".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"VehicleId", new ColumnInformation() { DataType = "string", ModelName = "VehicleId", ColumnName = "vehicle_id"}},
				{"VehicleDescription", new ColumnInformation() { DataType = "string", ModelName = "VehicleDescription", ColumnName = "vehicle_description"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "location"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
			};

	public override string ToString() 
	{
		return "wndba.tvehicle";
	}
}
}
