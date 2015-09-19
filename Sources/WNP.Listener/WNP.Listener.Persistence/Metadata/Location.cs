using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class LocationImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LocnDesc = "LOCN_DESC";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsLabLocn = "IS_LAB_LOCN";
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
	
	public string RealTableName
	{
		get { return "TLOCATION".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "LOCATION"}},
				{"LocnDesc", new ColumnInformation() { DataType = "string", ModelName = "LocnDesc", ColumnName = "LOCN_DESC"}},
				{"IsLabLocn", new ColumnInformation() { DataType = "string", ModelName = "IsLabLocn", ColumnName = "IS_LAB_LOCN"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
			};

	public override string ToString() 
	{
		return "wndba.tlocation";
	}
}
}
