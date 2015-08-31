using System.Collections.Generic;
public class PbcatfmtImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbfName = "pbf_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbfFrmt = "pbf_frmt";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbfType = "pbf_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbfCntr = "pbf_cntr";
	
	public string RealTableName
	{
		get { return "pbcatfmt".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbfName", new ColumnInformation() { DataType = "string", ModelName = "PbfName", ColumnName = "pbf_name"}},
				{"PbfFrmt", new ColumnInformation() { DataType = "string", ModelName = "PbfFrmt", ColumnName = "pbf_frmt"}},
				{"PbfType", new ColumnInformation() { DataType = "short", ModelName = "PbfType", ColumnName = "pbf_type"}},
				{"PbfCntr", new ColumnInformation() { DataType = "int", ModelName = "PbfCntr", ColumnName = "pbf_cntr"}},
			};

	public override string ToString() 
	{
		return "wndba.pbcatfmt";
	}
}
