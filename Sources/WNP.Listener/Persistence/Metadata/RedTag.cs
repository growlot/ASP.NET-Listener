using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class RedTagImpl: ITableInformation {
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
	public string EqpNo = "eqp_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "eqp_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "shop_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RedTagStatus = "red_tag_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser01 = "rtag_user_01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser02 = "rtag_user_02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser03 = "rtag_user_03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser04 = "rtag_user_04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser05 = "rtag_user_05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser06 = "rtag_user_06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser07 = "rtag_user_07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser08 = "rtag_user_08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser09 = "rtag_user_09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RtagUser10 = "rtag_user_10";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
	
	public string RealTableName
	{
		get { return "tred_tag".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "shop_cycle"}},
				{"RedTagStatus", new ColumnInformation() { DataType = "string", ModelName = "RedTagStatus", ColumnName = "red_tag_status"}},
				{"RtagUser01", new ColumnInformation() { DataType = "string", ModelName = "RtagUser01", ColumnName = "rtag_user_01"}},
				{"RtagUser02", new ColumnInformation() { DataType = "string", ModelName = "RtagUser02", ColumnName = "rtag_user_02"}},
				{"RtagUser03", new ColumnInformation() { DataType = "string", ModelName = "RtagUser03", ColumnName = "rtag_user_03"}},
				{"RtagUser04", new ColumnInformation() { DataType = "string", ModelName = "RtagUser04", ColumnName = "rtag_user_04"}},
				{"RtagUser05", new ColumnInformation() { DataType = "string", ModelName = "RtagUser05", ColumnName = "rtag_user_05"}},
				{"RtagUser06", new ColumnInformation() { DataType = "string", ModelName = "RtagUser06", ColumnName = "rtag_user_06"}},
				{"RtagUser07", new ColumnInformation() { DataType = "string", ModelName = "RtagUser07", ColumnName = "rtag_user_07"}},
				{"RtagUser08", new ColumnInformation() { DataType = "string", ModelName = "RtagUser08", ColumnName = "rtag_user_08"}},
				{"RtagUser09", new ColumnInformation() { DataType = "string", ModelName = "RtagUser09", ColumnName = "rtag_user_09"}},
				{"RtagUser10", new ColumnInformation() { DataType = "string", ModelName = "RtagUser10", ColumnName = "rtag_user_10"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.tred_tag";
	}
}
}
