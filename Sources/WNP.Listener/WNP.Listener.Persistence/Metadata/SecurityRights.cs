using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SecurityRightsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ProgramName = "PROGRAM_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ControlName = "CONTROL_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string GroupName = "GROUP_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AllowAccess = "ALLOW_ACCESS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AllowUpdate = "ALLOW_UPDATE";
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
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "TSECURITY_RIGHTS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"ProgramName", new ColumnInformation() { DataType = "string", ModelName = "ProgramName", ColumnName = "PROGRAM_NAME"}},
				{"ControlName", new ColumnInformation() { DataType = "string", ModelName = "ControlName", ColumnName = "CONTROL_NAME"}},
				{"GroupName", new ColumnInformation() { DataType = "string", ModelName = "GroupName", ColumnName = "GROUP_NAME"}},
				{"AllowAccess", new ColumnInformation() { DataType = "string", ModelName = "AllowAccess", ColumnName = "ALLOW_ACCESS"}},
				{"AllowUpdate", new ColumnInformation() { DataType = "string", ModelName = "AllowUpdate", ColumnName = "ALLOW_UPDATE"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tsecurity_rights";
	}
}
}
