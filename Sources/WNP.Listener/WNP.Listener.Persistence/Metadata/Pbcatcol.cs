using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class PbcatcolImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcTnam = "pbc_tnam";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcTid = "pbc_tid";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcOwnr = "pbc_ownr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcCnam = "pbc_cnam";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcCid = "pbc_cid";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcLabl = "pbc_labl";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcLpos = "pbc_lpos";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcHdr = "pbc_hdr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcHpos = "pbc_hpos";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcJtfy = "pbc_jtfy";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcMask = "pbc_mask";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcCase = "pbc_case";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcHght = "pbc_hght";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcWdth = "pbc_wdth";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcPtrn = "pbc_ptrn";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcBmap = "pbc_bmap";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcInit = "pbc_init";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcCmnt = "pbc_cmnt";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcEdit = "pbc_edit";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbcTag = "pbc_tag";
	
	public string RealTableName
	{
		get { return "pbcatcol".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbcTnam", new ColumnInformation() { DataType = "string", ModelName = "PbcTnam", ColumnName = "pbc_tnam"}},
				{"PbcTid", new ColumnInformation() { DataType = "int", ModelName = "PbcTid", ColumnName = "pbc_tid"}},
				{"PbcOwnr", new ColumnInformation() { DataType = "string", ModelName = "PbcOwnr", ColumnName = "pbc_ownr"}},
				{"PbcCnam", new ColumnInformation() { DataType = "string", ModelName = "PbcCnam", ColumnName = "pbc_cnam"}},
				{"PbcCid", new ColumnInformation() { DataType = "short", ModelName = "PbcCid", ColumnName = "pbc_cid"}},
				{"PbcLabl", new ColumnInformation() { DataType = "string", ModelName = "PbcLabl", ColumnName = "pbc_labl"}},
				{"PbcLpos", new ColumnInformation() { DataType = "short", ModelName = "PbcLpos", ColumnName = "pbc_lpos"}},
				{"PbcHdr", new ColumnInformation() { DataType = "string", ModelName = "PbcHdr", ColumnName = "pbc_hdr"}},
				{"PbcHpos", new ColumnInformation() { DataType = "short", ModelName = "PbcHpos", ColumnName = "pbc_hpos"}},
				{"PbcJtfy", new ColumnInformation() { DataType = "short", ModelName = "PbcJtfy", ColumnName = "pbc_jtfy"}},
				{"PbcMask", new ColumnInformation() { DataType = "string", ModelName = "PbcMask", ColumnName = "pbc_mask"}},
				{"PbcCase", new ColumnInformation() { DataType = "short", ModelName = "PbcCase", ColumnName = "pbc_case"}},
				{"PbcHght", new ColumnInformation() { DataType = "short", ModelName = "PbcHght", ColumnName = "pbc_hght"}},
				{"PbcWdth", new ColumnInformation() { DataType = "short", ModelName = "PbcWdth", ColumnName = "pbc_wdth"}},
				{"PbcPtrn", new ColumnInformation() { DataType = "string", ModelName = "PbcPtrn", ColumnName = "pbc_ptrn"}},
				{"PbcBmap", new ColumnInformation() { DataType = "string", ModelName = "PbcBmap", ColumnName = "pbc_bmap"}},
				{"PbcInit", new ColumnInformation() { DataType = "string", ModelName = "PbcInit", ColumnName = "pbc_init"}},
				{"PbcCmnt", new ColumnInformation() { DataType = "string", ModelName = "PbcCmnt", ColumnName = "pbc_cmnt"}},
				{"PbcEdit", new ColumnInformation() { DataType = "string", ModelName = "PbcEdit", ColumnName = "pbc_edit"}},
				{"PbcTag", new ColumnInformation() { DataType = "string", ModelName = "PbcTag", ColumnName = "pbc_tag"}},
			};

	public override string ToString() 
	{
		return "wndba.pbcatcol";
	}
}
}
