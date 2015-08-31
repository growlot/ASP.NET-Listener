using System.Collections.Generic;
public class VersionInfoImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: long
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Version = "Version";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AppliedOn = "AppliedOn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description = "Description";
	
	public string RealTableName
	{
		get { return "VersionInfo".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Version", new ColumnInformation() { DataType = "long", ModelName = "Version", ColumnName = "Version"}},
				{"AppliedOn", new ColumnInformation() { DataType = "DateTime", ModelName = "AppliedOn", ColumnName = "AppliedOn"}},
				{"Description", new ColumnInformation() { DataType = "string", ModelName = "Description", ColumnName = "Description"}},
			};

	public override string ToString() 
	{
		return "dbo.versioninfo";
	}
}
