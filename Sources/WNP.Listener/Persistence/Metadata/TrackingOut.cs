using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TrackingOutImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestProgram = "TEST_PROGRAM";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Workstation = "WORKSTATION";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutStatusIndex = "OUT_STATUS_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutShopStatus = "OUT_SHOP_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutEqpStatus = "OUT_EQP_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutLocation = "OUT_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeToTestProg = "CHANGE_TO_TEST_PROG";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ButtonAction = "BUTTON_ACTION";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ButtonOrder = "BUTTON_ORDER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AutoSave = "AUTO_SAVE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ConfirmSave = "CONFIRM_SAVE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IncrementCycle = "INCREMENT_CYCLE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
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
	public string ClearBox = "clear_box";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ClearPallet = "clear_pallet";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutFunctTrigger1 = "out_funct_trigger_1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutFunctTrigger2 = "out_funct_trigger_2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OutFunctTrigger3 = "out_funct_trigger_3";
	
	public string RealTableName
	{
		get { return "TTRACKING_OUT".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"TestProgram", new ColumnInformation() { DataType = "string", ModelName = "TestProgram", ColumnName = "TEST_PROGRAM"}},
				{"Workstation", new ColumnInformation() { DataType = "string", ModelName = "Workstation", ColumnName = "WORKSTATION"}},
				{"OutStatusIndex", new ColumnInformation() { DataType = "int", ModelName = "OutStatusIndex", ColumnName = "OUT_STATUS_INDEX"}},
				{"OutShopStatus", new ColumnInformation() { DataType = "string", ModelName = "OutShopStatus", ColumnName = "OUT_SHOP_STATUS"}},
				{"OutEqpStatus", new ColumnInformation() { DataType = "string", ModelName = "OutEqpStatus", ColumnName = "OUT_EQP_STATUS"}},
				{"OutLocation", new ColumnInformation() { DataType = "string", ModelName = "OutLocation", ColumnName = "OUT_LOCATION"}},
				{"ChangeToTestProg", new ColumnInformation() { DataType = "string", ModelName = "ChangeToTestProg", ColumnName = "CHANGE_TO_TEST_PROG"}},
				{"ButtonAction", new ColumnInformation() { DataType = "string", ModelName = "ButtonAction", ColumnName = "BUTTON_ACTION"}},
				{"ButtonOrder", new ColumnInformation() { DataType = "int", ModelName = "ButtonOrder", ColumnName = "BUTTON_ORDER"}},
				{"AutoSave", new ColumnInformation() { DataType = "string", ModelName = "AutoSave", ColumnName = "AUTO_SAVE"}},
				{"ConfirmSave", new ColumnInformation() { DataType = "string", ModelName = "ConfirmSave", ColumnName = "CONFIRM_SAVE"}},
				{"IncrementCycle", new ColumnInformation() { DataType = "string", ModelName = "IncrementCycle", ColumnName = "INCREMENT_CYCLE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ClearBox", new ColumnInformation() { DataType = "string", ModelName = "ClearBox", ColumnName = "clear_box"}},
				{"ClearPallet", new ColumnInformation() { DataType = "string", ModelName = "ClearPallet", ColumnName = "clear_pallet"}},
				{"OutFunctTrigger1", new ColumnInformation() { DataType = "string", ModelName = "OutFunctTrigger1", ColumnName = "out_funct_trigger_1"}},
				{"OutFunctTrigger2", new ColumnInformation() { DataType = "string", ModelName = "OutFunctTrigger2", ColumnName = "out_funct_trigger_2"}},
				{"OutFunctTrigger3", new ColumnInformation() { DataType = "string", ModelName = "OutFunctTrigger3", ColumnName = "out_funct_trigger_3"}},
			};

	public override string ToString() 
	{
		return "wndba.ttracking_out";
	}
}
}
