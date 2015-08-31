using System.Collections.Generic;
public class ValidationImpl: ITableInformation {
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
	public string ValColumn = "VAL_COLUMN";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValCode = "VAL_CODE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValDesc = "VAL_DESC";
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
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FilterCode = "FILTER_CODE";
	
	public string RealTableName
	{
		get { return "TVALIDATION".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"ValColumn", new ColumnInformation() { DataType = "string", ModelName = "ValColumn", ColumnName = "VAL_COLUMN"}},
				{"ValCode", new ColumnInformation() { DataType = "string", ModelName = "ValCode", ColumnName = "VAL_CODE"}},
				{"ValDesc", new ColumnInformation() { DataType = "string", ModelName = "ValDesc", ColumnName = "VAL_DESC"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"FilterCode", new ColumnInformation() { DataType = "string", ModelName = "FilterCode", ColumnName = "FILTER_CODE"}},
			};

	public override string ToString() 
	{
		return "wndba.tvalidation";
	}
}
