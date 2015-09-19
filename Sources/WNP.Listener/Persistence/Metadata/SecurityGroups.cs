using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SecurityGroupsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string GroupName = "GROUP_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string GroupDescription = "GROUP_DESCRIPTION";
	
	public string RealTableName
	{
		get { return "TSECURITY_GROUPS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"GroupName", new ColumnInformation() { DataType = "string", ModelName = "GroupName", ColumnName = "GROUP_NAME"}},
				{"GroupDescription", new ColumnInformation() { DataType = "string", ModelName = "GroupDescription", ColumnName = "GROUP_DESCRIPTION"}},
			};

	public override string ToString() 
	{
		return "wndba.tsecurity_groups";
	}
}
}
