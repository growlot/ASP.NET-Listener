using System.Collections.Generic;
public class TrackingInImpl: ITableInformation {
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
	public string InStatusIndex = "IN_STATUS_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsAllowed = "IS_ALLOWED";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InShopStatus = "IN_SHOP_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InEqpStatus = "IN_EQP_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InLocation = "IN_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DisplayMessage = "DISPLAY_MESSAGE";
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
	public string InFunctTrigger1 = "in_funct_trigger_1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InFunctTrigger2 = "in_funct_trigger_2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InFunctTrigger3 = "in_funct_trigger_3";
	
	public string RealTableName
	{
		get { return "TTRACKING_IN".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"TestProgram", new ColumnInformation() { DataType = "string", ModelName = "TestProgram", ColumnName = "TEST_PROGRAM"}},
				{"Workstation", new ColumnInformation() { DataType = "string", ModelName = "Workstation", ColumnName = "WORKSTATION"}},
				{"InStatusIndex", new ColumnInformation() { DataType = "int", ModelName = "InStatusIndex", ColumnName = "IN_STATUS_INDEX"}},
				{"IsAllowed", new ColumnInformation() { DataType = "string", ModelName = "IsAllowed", ColumnName = "IS_ALLOWED"}},
				{"InShopStatus", new ColumnInformation() { DataType = "string", ModelName = "InShopStatus", ColumnName = "IN_SHOP_STATUS"}},
				{"InEqpStatus", new ColumnInformation() { DataType = "string", ModelName = "InEqpStatus", ColumnName = "IN_EQP_STATUS"}},
				{"InLocation", new ColumnInformation() { DataType = "string", ModelName = "InLocation", ColumnName = "IN_LOCATION"}},
				{"DisplayMessage", new ColumnInformation() { DataType = "string", ModelName = "DisplayMessage", ColumnName = "DISPLAY_MESSAGE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"InFunctTrigger1", new ColumnInformation() { DataType = "string", ModelName = "InFunctTrigger1", ColumnName = "in_funct_trigger_1"}},
				{"InFunctTrigger2", new ColumnInformation() { DataType = "string", ModelName = "InFunctTrigger2", ColumnName = "in_funct_trigger_2"}},
				{"InFunctTrigger3", new ColumnInformation() { DataType = "string", ModelName = "InFunctTrigger3", ColumnName = "in_funct_trigger_3"}},
			};

	public override string ToString() 
	{
		return "wndba.ttracking_in";
	}
}
