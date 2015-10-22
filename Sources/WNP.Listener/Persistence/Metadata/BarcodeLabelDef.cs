using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class BarcodeLabelDefImpl: ITableInformation {
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
	public string FaceplateCol = "FACEPLATE_COL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string BarcodeLabel = "BARCODE_LABEL";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string StartPos = "start_pos";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Length = "LENGTH";
	
	public string RealTableName
	{
		get { return "TBARCODE_LABEL_DEF".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"FaceplateCol", new ColumnInformation() { DataType = "string", ModelName = "FaceplateCol", ColumnName = "FACEPLATE_COL"}},
				{"BarcodeLabel", new ColumnInformation() { DataType = "string", ModelName = "BarcodeLabel", ColumnName = "BARCODE_LABEL"}},
				{"StartPos", new ColumnInformation() { DataType = "int", ModelName = "StartPos", ColumnName = "start_pos"}},
				{"Length", new ColumnInformation() { DataType = "int", ModelName = "Length", ColumnName = "LENGTH"}},
			};

	public override string ToString() 
	{
		return "wndba.tbarcode_label_def";
	}
}
}
