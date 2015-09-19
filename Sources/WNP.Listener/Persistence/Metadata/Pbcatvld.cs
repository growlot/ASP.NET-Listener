using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class PbcatvldImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbvName = "pbv_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbvVald = "pbv_vald";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbvType = "pbv_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbvCntr = "pbv_cntr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbvMsg = "pbv_msg";
	
	public string RealTableName
	{
		get { return "pbcatvld".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbvName", new ColumnInformation() { DataType = "string", ModelName = "PbvName", ColumnName = "pbv_name"}},
				{"PbvVald", new ColumnInformation() { DataType = "string", ModelName = "PbvVald", ColumnName = "pbv_vald"}},
				{"PbvType", new ColumnInformation() { DataType = "short", ModelName = "PbvType", ColumnName = "pbv_type"}},
				{"PbvCntr", new ColumnInformation() { DataType = "int", ModelName = "PbvCntr", ColumnName = "pbv_cntr"}},
				{"PbvMsg", new ColumnInformation() { DataType = "string", ModelName = "PbvMsg", ColumnName = "pbv_msg"}},
			};

	public override string ToString() 
	{
		return "wndba.pbcatvld";
	}
}
}
