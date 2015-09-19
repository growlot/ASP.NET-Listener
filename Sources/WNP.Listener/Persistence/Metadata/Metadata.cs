using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MetadataImpl: ITableInformation {
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
	public string TableName = "table_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ColumnName = "column_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataType = "data_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataLength = "data_length";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataPrecision = "data_precision";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsUsed = "is_used";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsRequiredOnNew = "is_required_on_new";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsRequiredAlways = "is_required_always";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsPrimaryKey = "is_primary_key";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsForeignKey = "is_foreign_key";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsIdentity = "is_identity";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NullIfBlank = "null_if_blank";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InitIfNull = "init_if_null";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValueIfNull = "value_if_null";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataRegex = "data_regex";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataFormat = "data_format";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CustomerLabel = "customer_label";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataDescription = "data_description";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataExample = "data_example";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "id";
	
	public string RealTableName
	{
		get { return "tmetadata".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"TableName", new ColumnInformation() { DataType = "string", ModelName = "TableName", ColumnName = "table_name"}},
				{"ColumnName", new ColumnInformation() { DataType = "string", ModelName = "ColumnName", ColumnName = "column_name"}},
				{"DataType", new ColumnInformation() { DataType = "string", ModelName = "DataType", ColumnName = "data_type"}},
				{"DataLength", new ColumnInformation() { DataType = "int", ModelName = "DataLength", ColumnName = "data_length"}},
				{"DataPrecision", new ColumnInformation() { DataType = "int", ModelName = "DataPrecision", ColumnName = "data_precision"}},
				{"IsUsed", new ColumnInformation() { DataType = "string", ModelName = "IsUsed", ColumnName = "is_used"}},
				{"IsRequiredOnNew", new ColumnInformation() { DataType = "string", ModelName = "IsRequiredOnNew", ColumnName = "is_required_on_new"}},
				{"IsRequiredAlways", new ColumnInformation() { DataType = "string", ModelName = "IsRequiredAlways", ColumnName = "is_required_always"}},
				{"IsPrimaryKey", new ColumnInformation() { DataType = "string", ModelName = "IsPrimaryKey", ColumnName = "is_primary_key"}},
				{"IsForeignKey", new ColumnInformation() { DataType = "string", ModelName = "IsForeignKey", ColumnName = "is_foreign_key"}},
				{"IsIdentity", new ColumnInformation() { DataType = "string", ModelName = "IsIdentity", ColumnName = "is_identity"}},
				{"NullIfBlank", new ColumnInformation() { DataType = "string", ModelName = "NullIfBlank", ColumnName = "null_if_blank"}},
				{"InitIfNull", new ColumnInformation() { DataType = "string", ModelName = "InitIfNull", ColumnName = "init_if_null"}},
				{"ValueIfNull", new ColumnInformation() { DataType = "string", ModelName = "ValueIfNull", ColumnName = "value_if_null"}},
				{"DataRegex", new ColumnInformation() { DataType = "string", ModelName = "DataRegex", ColumnName = "data_regex"}},
				{"DataFormat", new ColumnInformation() { DataType = "string", ModelName = "DataFormat", ColumnName = "data_format"}},
				{"CustomerLabel", new ColumnInformation() { DataType = "string", ModelName = "CustomerLabel", ColumnName = "customer_label"}},
				{"DataDescription", new ColumnInformation() { DataType = "string", ModelName = "DataDescription", ColumnName = "data_description"}},
				{"DataExample", new ColumnInformation() { DataType = "string", ModelName = "DataExample", ColumnName = "data_example"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "id"}},
			};

	public override string ToString() 
	{
		return "wndba.tmetadata";
	}
}
}
