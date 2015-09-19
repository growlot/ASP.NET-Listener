using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SysValidationImpl: ITableInformation {
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
	public string ValColumn = "val_column";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValCode = "val_code";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValDesc = "val_desc";
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
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FilterCode = "filter_code";
	
	public string RealTableName
	{
		get { return "TSYS_VALIDATION".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"ValColumn", new ColumnInformation() { DataType = "string", ModelName = "ValColumn", ColumnName = "val_column"}},
				{"ValCode", new ColumnInformation() { DataType = "string", ModelName = "ValCode", ColumnName = "val_code"}},
				{"ValDesc", new ColumnInformation() { DataType = "string", ModelName = "ValDesc", ColumnName = "val_desc"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"FilterCode", new ColumnInformation() { DataType = "string", ModelName = "FilterCode", ColumnName = "filter_code"}},
			};

	public override string ToString() 
	{
		return "wndba.tsys_validation";
	}
}
}
