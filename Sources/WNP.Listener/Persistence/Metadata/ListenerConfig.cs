// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ListenerConfigTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ConfigId { get; } = "config_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConfigName { get; } = "config_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConfigValue { get; } = "config_value";
	
	public string RealTableName
	{
		get { return "tlistener_config".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
}
#pragma warning restore 1591
