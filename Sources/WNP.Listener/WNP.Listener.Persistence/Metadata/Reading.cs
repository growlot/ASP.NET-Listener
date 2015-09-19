using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ReadingImpl: ITableInformation {
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
	public string EqpNo = "eqp_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadIndex = "read_index";
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
	public string EventFlag = "event_flag";
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
	public string Reading = "reading";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadSrc = "read_src";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadDate = "read_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadBy = "read_by";
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
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
	
	public string RealTableName
	{
		get { return "TREADING".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"ReadIndex", new ColumnInformation() { DataType = "int", ModelName = "ReadIndex", ColumnName = "read_index"}},
				{"Annunciator", new ColumnInformation() { DataType = "string", ModelName = "Annunciator", ColumnName = "annunciator"}},
				{"EventFlag", new ColumnInformation() { DataType = "string", ModelName = "EventFlag", ColumnName = "event_flag"}},
				{"ReadLabel", new ColumnInformation() { DataType = "string", ModelName = "ReadLabel", ColumnName = "read_label"}},
				{"Reading", new ColumnInformation() { DataType = "string", ModelName = "Reading", ColumnName = "reading"}},
				{"ReadSrc", new ColumnInformation() { DataType = "string", ModelName = "ReadSrc", ColumnName = "read_src"}},
				{"ReadDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ReadDate", ColumnName = "read_date"}},
				{"ReadBy", new ColumnInformation() { DataType = "string", ModelName = "ReadBy", ColumnName = "read_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
			};

	public override string ToString() 
	{
		return "wndba.treading";
	}
}
}
