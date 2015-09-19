using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SiteHistImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Site = "site";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteDate = "site_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteDescription = "site_description";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteAddress = "site_address";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteAddress2 = "site_address2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteCity = "site_city";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteState = "site_state";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteZipcode = "site_zipcode";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteCountry = "site_country";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountName = "account_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountNo = "account_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PremiseNo = "premise_no";
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
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionId = "transaction_id";
	
	public string RealTableName
	{
		get { return "TSITE_HIST".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"SiteDate", new ColumnInformation() { DataType = "DateTime", ModelName = "SiteDate", ColumnName = "site_date"}},
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"SiteDescription", new ColumnInformation() { DataType = "string", ModelName = "SiteDescription", ColumnName = "site_description"}},
				{"SiteAddress", new ColumnInformation() { DataType = "string", ModelName = "SiteAddress", ColumnName = "site_address"}},
				{"SiteAddress2", new ColumnInformation() { DataType = "string", ModelName = "SiteAddress2", ColumnName = "site_address2"}},
				{"SiteCity", new ColumnInformation() { DataType = "string", ModelName = "SiteCity", ColumnName = "site_city"}},
				{"SiteState", new ColumnInformation() { DataType = "string", ModelName = "SiteState", ColumnName = "site_state"}},
				{"SiteZipcode", new ColumnInformation() { DataType = "string", ModelName = "SiteZipcode", ColumnName = "site_zipcode"}},
				{"SiteCountry", new ColumnInformation() { DataType = "string", ModelName = "SiteCountry", ColumnName = "site_country"}},
				{"AccountName", new ColumnInformation() { DataType = "string", ModelName = "AccountName", ColumnName = "account_name"}},
				{"AccountNo", new ColumnInformation() { DataType = "string", ModelName = "AccountNo", ColumnName = "account_no"}},
				{"PremiseNo", new ColumnInformation() { DataType = "string", ModelName = "PremiseNo", ColumnName = "premise_no"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
			};

	public override string ToString() 
	{
		return "wndba.tsite_hist";
	}
}
}
