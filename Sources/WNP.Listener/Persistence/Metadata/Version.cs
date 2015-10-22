using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class VersionImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Version = "version";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WnpVersion = "wnp_version";
	
	public string RealTableName
	{
		get { return "tversion".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Version", new ColumnInformation() { DataType = "int", ModelName = "Version", ColumnName = "version"}},
				{"WnpVersion", new ColumnInformation() { DataType = "string", ModelName = "WnpVersion", ColumnName = "wnp_version"}},
			};

	public override string ToString() 
	{
		return "wndba.tversion";
	}
}
}
