using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class SysControlImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string TableIndex = "table_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DisableLogin = "disable_login";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SecurityEnabled = "security_enabled";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SecurityUserid = "security_userid";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SecurityLevel2nopw = "security_level2nopw";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PasswordChangeInterval = "password_change_interval";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextBoxNo = "next_box_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextPalletNo = "next_pallet_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextUnknownNo = "next_unknown_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AutoCreateEquipment = "auto_create_equipment";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseTrackingAtTesting = "use_tracking_at_testing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UseWattnetBasic = "use_wattnet_basic";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextBatchNo = "NEXT_BATCH_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UsesMfrSerialNoFlag = "USES_MFR_SERIAL_NO_FLAG";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnableAutoDbUpdates = "enable_auto_db_updates";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AuditLogNextIndex = "audit_log_next_index";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AuditLogLimit = "audit_log_limit";
	
	public string RealTableName
	{
		get { return "TSYS_CONTROL".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"TableIndex", new ColumnInformation() { DataType = "string", ModelName = "TableIndex", ColumnName = "table_index"}},
				{"DisableLogin", new ColumnInformation() { DataType = "string", ModelName = "DisableLogin", ColumnName = "disable_login"}},
				{"SecurityEnabled", new ColumnInformation() { DataType = "string", ModelName = "SecurityEnabled", ColumnName = "security_enabled"}},
				{"SecurityUserid", new ColumnInformation() { DataType = "string", ModelName = "SecurityUserid", ColumnName = "security_userid"}},
				{"SecurityLevel2nopw", new ColumnInformation() { DataType = "string", ModelName = "SecurityLevel2nopw", ColumnName = "security_level2nopw"}},
				{"PasswordChangeInterval", new ColumnInformation() { DataType = "int", ModelName = "PasswordChangeInterval", ColumnName = "password_change_interval"}},
				{"NextBoxNo", new ColumnInformation() { DataType = "int", ModelName = "NextBoxNo", ColumnName = "next_box_no"}},
				{"NextPalletNo", new ColumnInformation() { DataType = "int", ModelName = "NextPalletNo", ColumnName = "next_pallet_no"}},
				{"NextUnknownNo", new ColumnInformation() { DataType = "int", ModelName = "NextUnknownNo", ColumnName = "next_unknown_no"}},
				{"AutoCreateEquipment", new ColumnInformation() { DataType = "string", ModelName = "AutoCreateEquipment", ColumnName = "auto_create_equipment"}},
				{"UseTrackingAtTesting", new ColumnInformation() { DataType = "string", ModelName = "UseTrackingAtTesting", ColumnName = "use_tracking_at_testing"}},
				{"UseWattnetBasic", new ColumnInformation() { DataType = "string", ModelName = "UseWattnetBasic", ColumnName = "use_wattnet_basic"}},
				{"NextBatchNo", new ColumnInformation() { DataType = "int", ModelName = "NextBatchNo", ColumnName = "NEXT_BATCH_NO"}},
				{"UsesMfrSerialNoFlag", new ColumnInformation() { DataType = "string", ModelName = "UsesMfrSerialNoFlag", ColumnName = "USES_MFR_SERIAL_NO_FLAG"}},
				{"EnableAutoDbUpdates", new ColumnInformation() { DataType = "string", ModelName = "EnableAutoDbUpdates", ColumnName = "enable_auto_db_updates"}},
				{"AuditLogNextIndex", new ColumnInformation() { DataType = "int", ModelName = "AuditLogNextIndex", ColumnName = "audit_log_next_index"}},
				{"AuditLogLimit", new ColumnInformation() { DataType = "int", ModelName = "AuditLogLimit", ColumnName = "audit_log_limit"}},
			};

	public override string ToString() 
	{
		return "wndba.tsys_control";
	}
}
}
