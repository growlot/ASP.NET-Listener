using System.Collections.Generic;
public class PbcatedtImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeName = "pbe_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeEdit = "pbe_edit";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeType = "pbe_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeCntr = "pbe_cntr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeSeqn = "pbe_seqn";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeFlag = "pbe_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeWork = "pbe_work";
	
	public string RealTableName
	{
		get { return "pbcatedt".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbeName", new ColumnInformation() { DataType = "string", ModelName = "PbeName", ColumnName = "pbe_name"}},
				{"PbeEdit", new ColumnInformation() { DataType = "string", ModelName = "PbeEdit", ColumnName = "pbe_edit"}},
				{"PbeType", new ColumnInformation() { DataType = "short", ModelName = "PbeType", ColumnName = "pbe_type"}},
				{"PbeCntr", new ColumnInformation() { DataType = "int", ModelName = "PbeCntr", ColumnName = "pbe_cntr"}},
				{"PbeSeqn", new ColumnInformation() { DataType = "short", ModelName = "PbeSeqn", ColumnName = "pbe_seqn"}},
				{"PbeFlag", new ColumnInformation() { DataType = "int", ModelName = "PbeFlag", ColumnName = "pbe_flag"}},
				{"PbeWork", new ColumnInformation() { DataType = "string", ModelName = "PbeWork", ColumnName = "pbe_work"}},
			};

	public override string ToString() 
	{
		return "wndba.pbcatedt";
	}
}