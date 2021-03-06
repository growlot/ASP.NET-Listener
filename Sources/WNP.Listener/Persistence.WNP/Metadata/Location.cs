// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class LocationTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location { get; } = "LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LocnDesc { get; } = "LOCN_DESC";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsLabLocn { get; } = "IS_LAB_LOCN";
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
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LocationType { get; } = "location_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AreaName { get; } = "area_name";
	
	public string RealTableName
	{
		get { return "TLOCATION".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "LOCATION"}},
				{"LocnDesc", new ColumnInformation() { DataType = "string", ModelName = "LocnDesc", ColumnName = "LOCN_DESC"}},
				{"IsLabLocn", new ColumnInformation() { DataType = "string", ModelName = "IsLabLocn", ColumnName = "IS_LAB_LOCN"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"LocationType", new ColumnInformation() { DataType = "string", ModelName = "LocationType", ColumnName = "location_type"}},
				{"AreaName", new ColumnInformation() { DataType = "string", ModelName = "AreaName", ColumnName = "area_name"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TLOCATION";
	}
}
}
#pragma warning restore 1591
