using System.Collections.Generic;
public class ListenerConfigImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ConfigId = "config_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConfigName = "config_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConfigValue = "config_value";
	
	public string RealTableName
	{
		get { return "tlistener_config".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"ConfigId", new ColumnInformation() { DataType = "int", ModelName = "ConfigId", ColumnName = "config_id"}},
				{"ConfigName", new ColumnInformation() { DataType = "string", ModelName = "ConfigName", ColumnName = "config_name"}},
				{"ConfigValue", new ColumnInformation() { DataType = "string", ModelName = "ConfigValue", ColumnName = "config_value"}},
			};

	public override string ToString() 
	{
		return "wndba.tlistener_config";
	}
}
