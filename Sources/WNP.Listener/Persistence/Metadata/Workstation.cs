using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class WorkstationImpl: ITableInformation {
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
	public string Workstation = "WORKSTATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SourceTag = "SOURCE_TAG";
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
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AllowEdit = "ALLOW_EDIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WindowName = "window_name";
	
	public string RealTableName
	{
		get { return "TWORKSTATION".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Workstation", new ColumnInformation() { DataType = "string", ModelName = "Workstation", ColumnName = "WORKSTATION"}},
				{"SourceTag", new ColumnInformation() { DataType = "string", ModelName = "SourceTag", ColumnName = "SOURCE_TAG"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"AllowEdit", new ColumnInformation() { DataType = "string", ModelName = "AllowEdit", ColumnName = "ALLOW_EDIT"}},
				{"WindowName", new ColumnInformation() { DataType = "string", ModelName = "WindowName", ColumnName = "window_name"}},
			};

	public override string ToString() 
	{
		return "wndba.tworkstation";
	}
}
}
