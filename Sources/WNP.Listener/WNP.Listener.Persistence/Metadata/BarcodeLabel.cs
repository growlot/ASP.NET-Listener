using System.Collections.Generic;
public class BarcodeLabelImpl: ITableInformation {
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
	public string BarcodeLabel = "BARCODE_LABEL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string BarcodeTask = "BARCODE_TASK";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string BarcodeAction = "BARCODE_ACTION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PromptForScan = "PROMPT_FOR_SCAN";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PromptText = "PROMPT_TEXT";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PromptOrder = "PROMPT_ORDER";
	
	public string RealTableName
	{
		get { return "TBARCODE_LABEL".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"BarcodeLabel", new ColumnInformation() { DataType = "string", ModelName = "BarcodeLabel", ColumnName = "BARCODE_LABEL"}},
				{"BarcodeTask", new ColumnInformation() { DataType = "string", ModelName = "BarcodeTask", ColumnName = "BARCODE_TASK"}},
				{"BarcodeAction", new ColumnInformation() { DataType = "string", ModelName = "BarcodeAction", ColumnName = "BARCODE_ACTION"}},
				{"PromptForScan", new ColumnInformation() { DataType = "string", ModelName = "PromptForScan", ColumnName = "PROMPT_FOR_SCAN"}},
				{"PromptText", new ColumnInformation() { DataType = "string", ModelName = "PromptText", ColumnName = "PROMPT_TEXT"}},
				{"PromptOrder", new ColumnInformation() { DataType = "int", ModelName = "PromptOrder", ColumnName = "PROMPT_ORDER"}},
			};

	public override string ToString() 
	{
		return "wndba.tbarcode_label";
	}
}
