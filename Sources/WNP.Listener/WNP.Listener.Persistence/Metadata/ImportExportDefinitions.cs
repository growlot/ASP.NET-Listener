using System.Collections.Generic;
public class ImportExportDefinitionsImpl: ITableInformation {
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
	public string DefinitionName = "DEFINITION_NAME";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataFieldNumber = "DATA_FIELD_NUMBER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataFieldDescription = "DATA_FIELD_DESCRIPTION";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataFieldStart = "DATA_FIELD_START";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataFieldLength = "DATA_FIELD_LENGTH";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FaceplateCol = "FACEPLATE_COL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CustomMask = "custom_mask";
	
	public string RealTableName
	{
		get { return "TIMPORT_EXPORT_DEFINITIONS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"DefinitionName", new ColumnInformation() { DataType = "string", ModelName = "DefinitionName", ColumnName = "DEFINITION_NAME"}},
				{"DataFieldNumber", new ColumnInformation() { DataType = "int", ModelName = "DataFieldNumber", ColumnName = "DATA_FIELD_NUMBER"}},
				{"DataFieldDescription", new ColumnInformation() { DataType = "string", ModelName = "DataFieldDescription", ColumnName = "DATA_FIELD_DESCRIPTION"}},
				{"DataFieldStart", new ColumnInformation() { DataType = "int", ModelName = "DataFieldStart", ColumnName = "DATA_FIELD_START"}},
				{"DataFieldLength", new ColumnInformation() { DataType = "int", ModelName = "DataFieldLength", ColumnName = "DATA_FIELD_LENGTH"}},
				{"FaceplateCol", new ColumnInformation() { DataType = "string", ModelName = "FaceplateCol", ColumnName = "FACEPLATE_COL"}},
				{"CustomMask", new ColumnInformation() { DataType = "string", ModelName = "CustomMask", ColumnName = "custom_mask"}},
			};

	public override string ToString() 
	{
		return "wndba.timport_export_definitions";
	}
}
