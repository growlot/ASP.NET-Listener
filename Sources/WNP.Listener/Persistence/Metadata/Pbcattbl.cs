// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class PbcattblTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbtTnam { get; } = "pbt_tnam";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbtTid { get; } = "pbt_tid";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbtOwnr { get; } = "pbt_ownr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFhgt { get; } = "pbd_fhgt";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFwgt { get; } = "pbd_fwgt";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFitl { get; } = "pbd_fitl";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFunl { get; } = "pbd_funl";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFchr { get; } = "pbd_fchr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFptc { get; } = "pbd_fptc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbdFfce { get; } = "pbd_ffce";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFhgt { get; } = "pbh_fhgt";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFwgt { get; } = "pbh_fwgt";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFitl { get; } = "pbh_fitl";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFunl { get; } = "pbh_funl";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFchr { get; } = "pbh_fchr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFptc { get; } = "pbh_fptc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbhFfce { get; } = "pbh_ffce";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFhgt { get; } = "pbl_fhgt";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFwgt { get; } = "pbl_fwgt";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFitl { get; } = "pbl_fitl";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFunl { get; } = "pbl_funl";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFchr { get; } = "pbl_fchr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFptc { get; } = "pbl_fptc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PblFfce { get; } = "pbl_ffce";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbtCmnt { get; } = "pbt_cmnt";
	
	public string RealTableName
	{
		get { return "pbcattbl".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbtTnam", new ColumnInformation() { DataType = "string", ModelName = "PbtTnam", ColumnName = "pbt_tnam"}},
				{"PbtTid", new ColumnInformation() { DataType = "int", ModelName = "PbtTid", ColumnName = "pbt_tid"}},
				{"PbtOwnr", new ColumnInformation() { DataType = "string", ModelName = "PbtOwnr", ColumnName = "pbt_ownr"}},
				{"PbdFhgt", new ColumnInformation() { DataType = "short", ModelName = "PbdFhgt", ColumnName = "pbd_fhgt"}},
				{"PbdFwgt", new ColumnInformation() { DataType = "short", ModelName = "PbdFwgt", ColumnName = "pbd_fwgt"}},
				{"PbdFitl", new ColumnInformation() { DataType = "string", ModelName = "PbdFitl", ColumnName = "pbd_fitl"}},
				{"PbdFunl", new ColumnInformation() { DataType = "string", ModelName = "PbdFunl", ColumnName = "pbd_funl"}},
				{"PbdFchr", new ColumnInformation() { DataType = "short", ModelName = "PbdFchr", ColumnName = "pbd_fchr"}},
				{"PbdFptc", new ColumnInformation() { DataType = "short", ModelName = "PbdFptc", ColumnName = "pbd_fptc"}},
				{"PbdFfce", new ColumnInformation() { DataType = "string", ModelName = "PbdFfce", ColumnName = "pbd_ffce"}},
				{"PbhFhgt", new ColumnInformation() { DataType = "short", ModelName = "PbhFhgt", ColumnName = "pbh_fhgt"}},
				{"PbhFwgt", new ColumnInformation() { DataType = "short", ModelName = "PbhFwgt", ColumnName = "pbh_fwgt"}},
				{"PbhFitl", new ColumnInformation() { DataType = "string", ModelName = "PbhFitl", ColumnName = "pbh_fitl"}},
				{"PbhFunl", new ColumnInformation() { DataType = "string", ModelName = "PbhFunl", ColumnName = "pbh_funl"}},
				{"PbhFchr", new ColumnInformation() { DataType = "short", ModelName = "PbhFchr", ColumnName = "pbh_fchr"}},
				{"PbhFptc", new ColumnInformation() { DataType = "short", ModelName = "PbhFptc", ColumnName = "pbh_fptc"}},
				{"PbhFfce", new ColumnInformation() { DataType = "string", ModelName = "PbhFfce", ColumnName = "pbh_ffce"}},
				{"PblFhgt", new ColumnInformation() { DataType = "short", ModelName = "PblFhgt", ColumnName = "pbl_fhgt"}},
				{"PblFwgt", new ColumnInformation() { DataType = "short", ModelName = "PblFwgt", ColumnName = "pbl_fwgt"}},
				{"PblFitl", new ColumnInformation() { DataType = "string", ModelName = "PblFitl", ColumnName = "pbl_fitl"}},
				{"PblFunl", new ColumnInformation() { DataType = "string", ModelName = "PblFunl", ColumnName = "pbl_funl"}},
				{"PblFchr", new ColumnInformation() { DataType = "short", ModelName = "PblFchr", ColumnName = "pbl_fchr"}},
				{"PblFptc", new ColumnInformation() { DataType = "short", ModelName = "PblFptc", ColumnName = "pbl_fptc"}},
				{"PblFfce", new ColumnInformation() { DataType = "string", ModelName = "PblFfce", ColumnName = "pbl_ffce"}},
				{"PbtCmnt", new ColumnInformation() { DataType = "string", ModelName = "PbtCmnt", ColumnName = "pbt_cmnt"}},
			};

	public override string ToString() 
	{
		return "wndba.pbcattbl";
	}
}
}
#pragma warning restore 1591
