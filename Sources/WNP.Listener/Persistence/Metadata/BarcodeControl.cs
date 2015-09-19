using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class BarcodeControlImpl: ITableInformation {
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
	public string BcField = "BC_FIELD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ValidationTable = "VALIDATION_TABLE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FieldDescription = "FIELD_DESCRIPTION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FilterCol = "FILTER_COL";
	
	public string RealTableName
	{
		get { return "TBARCODE_CONTROL".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"BcField", new ColumnInformation() { DataType = "string", ModelName = "BcField", ColumnName = "BC_FIELD"}},
				{"ValidationTable", new ColumnInformation() { DataType = "string", ModelName = "ValidationTable", ColumnName = "VALIDATION_TABLE"}},
				{"FieldDescription", new ColumnInformation() { DataType = "string", ModelName = "FieldDescription", ColumnName = "FIELD_DESCRIPTION"}},
				{"FilterCol", new ColumnInformation() { DataType = "string", ModelName = "FilterCol", ColumnName = "FILTER_COL"}},
			};

	public override string ToString() 
	{
		return "wndba.tbarcode_control";
	}
}
}