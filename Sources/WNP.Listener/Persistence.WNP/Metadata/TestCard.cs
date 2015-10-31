// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class TestCardTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CardName { get; } = "card_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ObjType { get; } = "obj_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description { get; } = "description";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string X { get; } = "x";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Y { get; } = "y";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Width { get; } = "width";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Height { get; } = "height";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Properties { get; } = "properties";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ObjIndex { get; } = "obj_index";
	
	public string RealTableName
	{
		get { return "TTEST_CARD".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "WNDBA.TTEST_CARD";
	}
}
}
#pragma warning restore 1591