using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SelectionProgramImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Year = "year";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionProgram = "selection_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionMethod = "selection_method";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StartDate = "start_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EndDate = "end_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LastRefresh = "last_refresh";
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
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UpperLimit = "UPPER_LIMIT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LowerLimit = "LOWER_LIMIT";
	
	public string RealTableName
	{
		get { return "TSELECTION_PROGRAM".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"Year", new ColumnInformation() { DataType = "int", ModelName = "Year", ColumnName = "year"}},
				{"SelectionProgram", new ColumnInformation() { DataType = "string", ModelName = "SelectionProgram", ColumnName = "selection_program"}},
				{"SelectionMethod", new ColumnInformation() { DataType = "string", ModelName = "SelectionMethod", ColumnName = "selection_method"}},
				{"StartDate", new ColumnInformation() { DataType = "DateTime", ModelName = "StartDate", ColumnName = "start_date"}},
				{"EndDate", new ColumnInformation() { DataType = "DateTime", ModelName = "EndDate", ColumnName = "end_date"}},
				{"LastRefresh", new ColumnInformation() { DataType = "DateTime", ModelName = "LastRefresh", ColumnName = "last_refresh"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"UpperLimit", new ColumnInformation() { DataType = "decimal", ModelName = "UpperLimit", ColumnName = "UPPER_LIMIT"}},
				{"LowerLimit", new ColumnInformation() { DataType = "decimal", ModelName = "LowerLimit", ColumnName = "LOWER_LIMIT"}},
			};

	public override string ToString() 
	{
		return "wndba.tselection_program";
	}
}
}
