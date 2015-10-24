// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SiteInstallHistoryTable: ITableInformation {
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
	public string EqpType { get; } = "eqp_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo { get; } = "eqp_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallCount { get; } = "install_count";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site { get; } = "site";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit { get; } = "circuit";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountNo { get; } = "account_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PremiseNo { get; } = "premise_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallStatus { get; } = "install_status";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallDate { get; } = "install_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallBy { get; } = "install_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallServiceOrderStart { get; } = "install_service_order_start";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InstallServiceOrderComplete { get; } = "install_service_order_complete";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveDate { get; } = "remove_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveBy { get; } = "remove_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveReason { get; } = "remove_reason";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveServiceOrderStart { get; } = "remove_service_order_start";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RemoveServiceOrderComplete { get; } = "remove_service_order_complete";
	
	public string RealTableName
	{
		get { return "TSITE_INSTALL_HISTORY".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"InstallCount", new ColumnInformation() { DataType = "int", ModelName = "InstallCount", ColumnName = "install_count"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "circuit"}},
				{"AccountNo", new ColumnInformation() { DataType = "string", ModelName = "AccountNo", ColumnName = "account_no"}},
				{"PremiseNo", new ColumnInformation() { DataType = "string", ModelName = "PremiseNo", ColumnName = "premise_no"}},
				{"InstallStatus", new ColumnInformation() { DataType = "string", ModelName = "InstallStatus", ColumnName = "install_status"}},
				{"InstallDate", new ColumnInformation() { DataType = "DateTime", ModelName = "InstallDate", ColumnName = "install_date"}},
				{"InstallBy", new ColumnInformation() { DataType = "string", ModelName = "InstallBy", ColumnName = "install_by"}},
				{"InstallServiceOrderStart", new ColumnInformation() { DataType = "DateTime", ModelName = "InstallServiceOrderStart", ColumnName = "install_service_order_start"}},
				{"InstallServiceOrderComplete", new ColumnInformation() { DataType = "DateTime", ModelName = "InstallServiceOrderComplete", ColumnName = "install_service_order_complete"}},
				{"RemoveDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RemoveDate", ColumnName = "remove_date"}},
				{"RemoveBy", new ColumnInformation() { DataType = "string", ModelName = "RemoveBy", ColumnName = "remove_by"}},
				{"RemoveReason", new ColumnInformation() { DataType = "string", ModelName = "RemoveReason", ColumnName = "remove_reason"}},
				{"RemoveServiceOrderStart", new ColumnInformation() { DataType = "DateTime", ModelName = "RemoveServiceOrderStart", ColumnName = "remove_service_order_start"}},
				{"RemoveServiceOrderComplete", new ColumnInformation() { DataType = "DateTime", ModelName = "RemoveServiceOrderComplete", ColumnName = "remove_service_order_complete"}},
			};

	public override string ToString() 
	{
		return "wndba.tsite_install_history";
	}
}
}
#pragma warning restore 1591
