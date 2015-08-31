using System.Collections.Generic;
public class TestCardImpl: ITableInformation {
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
	public string CardName = "card_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ObjType = "obj_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description = "description";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string X = "x";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Y = "y";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Width = "width";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Height = "height";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Properties = "properties";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ObjIndex = "obj_index";
	
	public string RealTableName
	{
		get { return "TTEST_CARD".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"CardName", new ColumnInformation() { DataType = "string", ModelName = "CardName", ColumnName = "card_name"}},
				{"ObjType", new ColumnInformation() { DataType = "string", ModelName = "ObjType", ColumnName = "obj_type"}},
				{"Description", new ColumnInformation() { DataType = "string", ModelName = "Description", ColumnName = "description"}},
				{"X", new ColumnInformation() { DataType = "int", ModelName = "X", ColumnName = "x"}},
				{"Y", new ColumnInformation() { DataType = "int", ModelName = "Y", ColumnName = "y"}},
				{"Width", new ColumnInformation() { DataType = "int", ModelName = "Width", ColumnName = "width"}},
				{"Height", new ColumnInformation() { DataType = "int", ModelName = "Height", ColumnName = "height"}},
				{"Properties", new ColumnInformation() { DataType = "string", ModelName = "Properties", ColumnName = "properties"}},
				{"ObjIndex", new ColumnInformation() { DataType = "int", ModelName = "ObjIndex", ColumnName = "obj_index"}},
			};

	public override string ToString() 
	{
		return "wndba.ttest_card";
	}
}
