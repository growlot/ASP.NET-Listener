using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class OwnerImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OwnerDesc = "OWNER_DESC";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: byte[]
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CompanyLogo = "COMPANY_LOGO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaFormula1p = "wa_formula_1p";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WaFormula3p = "wa_formula_3p";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BalanceAf = "balance_af";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BalanceAl = "balance_al";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseAcceptanceTesting = "use_acceptance_testing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceListSource = "acceptance_list_source";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterCodeIsLookup = "meter_code_is_lookup";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseRmaAcceptTesting = "use_rma_accept_testing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaAcceptListSource = "rma_accept_list_source";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
	
	public string RealTableName
	{
		get { return "TOWNER".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"OwnerDesc", new ColumnInformation() { DataType = "string", ModelName = "OwnerDesc", ColumnName = "OWNER_DESC"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"CompanyLogo", new ColumnInformation() { DataType = "byte[]", ModelName = "CompanyLogo", ColumnName = "COMPANY_LOGO"}},
				{"WaFormula1p", new ColumnInformation() { DataType = "string", ModelName = "WaFormula1p", ColumnName = "wa_formula_1p"}},
				{"WaFormula3p", new ColumnInformation() { DataType = "string", ModelName = "WaFormula3p", ColumnName = "wa_formula_3p"}},
				{"BalanceAf", new ColumnInformation() { DataType = "string", ModelName = "BalanceAf", ColumnName = "balance_af"}},
				{"BalanceAl", new ColumnInformation() { DataType = "string", ModelName = "BalanceAl", ColumnName = "balance_al"}},
				{"UseAcceptanceTesting", new ColumnInformation() { DataType = "string", ModelName = "UseAcceptanceTesting", ColumnName = "use_acceptance_testing"}},
				{"AcceptanceListSource", new ColumnInformation() { DataType = "string", ModelName = "AcceptanceListSource", ColumnName = "acceptance_list_source"}},
				{"MeterCodeIsLookup", new ColumnInformation() { DataType = "string", ModelName = "MeterCodeIsLookup", ColumnName = "meter_code_is_lookup"}},
				{"UseRmaAcceptTesting", new ColumnInformation() { DataType = "string", ModelName = "UseRmaAcceptTesting", ColumnName = "use_rma_accept_testing"}},
				{"RmaAcceptListSource", new ColumnInformation() { DataType = "string", ModelName = "RmaAcceptListSource", ColumnName = "rma_accept_list_source"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
			};

	public override string ToString() 
	{
		return "wndba.towner";
	}
}
}
