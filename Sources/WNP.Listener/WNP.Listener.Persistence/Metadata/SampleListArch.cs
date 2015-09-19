using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SampleListArchImpl: ITableInformation {
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
	public string Year = "year";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionProgram = "selection_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StrataName = "strata_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpCode = "eqp_code";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "location";
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
	public string AccountName = "account_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SerialNo = "serial_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr = "mfr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModelNo = "model_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string YrNextTest = "yr_next_test";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string YrLastTest = "yr_last_test";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string KwhDials = "kwh_dials";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PurchaseDate = "purchase_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SampleStatus = "sample_status";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Sfl = "sfl";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Sll = "sll";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Spf = "spf";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Swa = "swa";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ZeroReg = "zero_reg";
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
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccuracyStatus = "accuracy_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountNo = "account_no";
	
	public string RealTableName
	{
		get { return "TSAMPLE_LIST_ARCH".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"Year", new ColumnInformation() { DataType = "int", ModelName = "Year", ColumnName = "year"}},
				{"SelectionProgram", new ColumnInformation() { DataType = "string", ModelName = "SelectionProgram", ColumnName = "selection_program"}},
				{"StrataName", new ColumnInformation() { DataType = "string", ModelName = "StrataName", ColumnName = "strata_name"}},
				{"EqpCode", new ColumnInformation() { DataType = "string", ModelName = "EqpCode", ColumnName = "eqp_code"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "location"}},
				{"SiteAddress", new ColumnInformation() { DataType = "string", ModelName = "SiteAddress", ColumnName = "site_address"}},
				{"SiteCity", new ColumnInformation() { DataType = "string", ModelName = "SiteCity", ColumnName = "site_city"}},
				{"SiteState", new ColumnInformation() { DataType = "string", ModelName = "SiteState", ColumnName = "site_state"}},
				{"SiteZipcode", new ColumnInformation() { DataType = "string", ModelName = "SiteZipcode", ColumnName = "site_zipcode"}},
				{"AccountName", new ColumnInformation() { DataType = "string", ModelName = "AccountName", ColumnName = "account_name"}},
				{"SerialNo", new ColumnInformation() { DataType = "string", ModelName = "SerialNo", ColumnName = "serial_no"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "mfr"}},
				{"ModelNo", new ColumnInformation() { DataType = "string", ModelName = "ModelNo", ColumnName = "model_no"}},
				{"YrNextTest", new ColumnInformation() { DataType = "string", ModelName = "YrNextTest", ColumnName = "yr_next_test"}},
				{"YrLastTest", new ColumnInformation() { DataType = "string", ModelName = "YrLastTest", ColumnName = "yr_last_test"}},
				{"KwhDials", new ColumnInformation() { DataType = "int", ModelName = "KwhDials", ColumnName = "kwh_dials"}},
				{"PurchaseDate", new ColumnInformation() { DataType = "DateTime", ModelName = "PurchaseDate", ColumnName = "purchase_date"}},
				{"SampleStatus", new ColumnInformation() { DataType = "string", ModelName = "SampleStatus", ColumnName = "sample_status"}},
				{"Sfl", new ColumnInformation() { DataType = "double", ModelName = "Sfl", ColumnName = "sfl"}},
				{"Sll", new ColumnInformation() { DataType = "double", ModelName = "Sll", ColumnName = "sll"}},
				{"Spf", new ColumnInformation() { DataType = "double", ModelName = "Spf", ColumnName = "spf"}},
				{"Swa", new ColumnInformation() { DataType = "double", ModelName = "Swa", ColumnName = "swa"}},
				{"ZeroReg", new ColumnInformation() { DataType = "string", ModelName = "ZeroReg", ColumnName = "zero_reg"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"AccuracyStatus", new ColumnInformation() { DataType = "string", ModelName = "AccuracyStatus", ColumnName = "accuracy_status"}},
				{"AccountNo", new ColumnInformation() { DataType = "string", ModelName = "AccountNo", ColumnName = "account_no"}},
			};

	public override string ToString() 
	{
		return "wndba.tsample_list_arch";
	}
}
}
