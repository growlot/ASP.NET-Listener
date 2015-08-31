using System.Collections.Generic;
public class ReadSetImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ReadSet = "read_set";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string RegisterId = "register_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Annunciator = "annunciator";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadLabel = "read_label";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadFormat = "read_format";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsBilling = "is_billing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PriReadFlag = "pri_read_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RequiredField = "required_field";
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
		get { return "TREAD_SET".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"ReadSet", new ColumnInformation() { DataType = "string", ModelName = "ReadSet", ColumnName = "read_set"}},
				{"RegisterId", new ColumnInformation() { DataType = "string", ModelName = "RegisterId", ColumnName = "register_id"}},
				{"Annunciator", new ColumnInformation() { DataType = "string", ModelName = "Annunciator", ColumnName = "annunciator"}},
				{"ReadLabel", new ColumnInformation() { DataType = "string", ModelName = "ReadLabel", ColumnName = "read_label"}},
				{"ReadFormat", new ColumnInformation() { DataType = "string", ModelName = "ReadFormat", ColumnName = "read_format"}},
				{"IsBilling", new ColumnInformation() { DataType = "string", ModelName = "IsBilling", ColumnName = "is_billing"}},
				{"PriReadFlag", new ColumnInformation() { DataType = "string", ModelName = "PriReadFlag", ColumnName = "pri_read_flag"}},
				{"RequiredField", new ColumnInformation() { DataType = "string", ModelName = "RequiredField", ColumnName = "required_field"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tread_set";
	}
}
