// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ReadSetTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ReadSet { get; } = "read_set";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string RegisterId { get; } = "register_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Annunciator { get; } = "annunciator";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadLabel { get; } = "read_label";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadFormat { get; } = "read_format";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsBilling { get; } = "is_billing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PriReadFlag { get; } = "pri_read_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RequiredField { get; } = "required_field";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "mod_by";
	
	public string RealTableName
	{
		get { return "TREAD_SET".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
}
#pragma warning restore 1591
