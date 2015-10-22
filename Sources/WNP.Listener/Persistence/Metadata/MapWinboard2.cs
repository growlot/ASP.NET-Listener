using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MapWinboard2Impl: ITableInformation {
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
	public string Wb2TableName = "wb2_table_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Wb2ColumnName = "wb2_column_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Wb2DataType = "wb2_data_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string WnpTableName = "wnp_table_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string WnpColumnName = "wnp_column_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string WnpDataType = "wnp_data_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string MapDirection = "map_direction";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpecialProcTag1 = "special_proc_tag1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpecialProcData1 = "special_proc_data1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpecialProcTag2 = "special_proc_tag2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SpecialProcData2 = "special_proc_data2";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "TMAP_WINBOARD2".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"Wb2TableName", new ColumnInformation() { DataType = "string", ModelName = "Wb2TableName", ColumnName = "wb2_table_name"}},
				{"Wb2ColumnName", new ColumnInformation() { DataType = "string", ModelName = "Wb2ColumnName", ColumnName = "wb2_column_name"}},
				{"Wb2DataType", new ColumnInformation() { DataType = "string", ModelName = "Wb2DataType", ColumnName = "wb2_data_type"}},
				{"WnpTableName", new ColumnInformation() { DataType = "string", ModelName = "WnpTableName", ColumnName = "wnp_table_name"}},
				{"WnpColumnName", new ColumnInformation() { DataType = "string", ModelName = "WnpColumnName", ColumnName = "wnp_column_name"}},
				{"WnpDataType", new ColumnInformation() { DataType = "string", ModelName = "WnpDataType", ColumnName = "wnp_data_type"}},
				{"MapDirection", new ColumnInformation() { DataType = "string", ModelName = "MapDirection", ColumnName = "map_direction"}},
				{"SpecialProcTag1", new ColumnInformation() { DataType = "string", ModelName = "SpecialProcTag1", ColumnName = "special_proc_tag1"}},
				{"SpecialProcData1", new ColumnInformation() { DataType = "string", ModelName = "SpecialProcData1", ColumnName = "special_proc_data1"}},
				{"SpecialProcTag2", new ColumnInformation() { DataType = "string", ModelName = "SpecialProcTag2", ColumnName = "special_proc_tag2"}},
				{"SpecialProcData2", new ColumnInformation() { DataType = "string", ModelName = "SpecialProcData2", ColumnName = "special_proc_data2"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tmap_winboard2";
	}
}
}
