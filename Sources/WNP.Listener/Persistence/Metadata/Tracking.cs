using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TrackingImpl: ITableInformation {
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
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Workstation = "WORKSTATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestProgram = "TEST_PROGRAM";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpStatus = "EQP_STATUS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopStatus = "SHOP_STATUS";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "SHOP_CYCLE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReceiveCategory = "RECEIVE_CATEGORY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReceivedBy = "RECEIVED_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VehicleId = "VEHICLE_ID";
	
	public string RealTableName
	{
		get { return "TTRACKING".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"Workstation", new ColumnInformation() { DataType = "string", ModelName = "Workstation", ColumnName = "WORKSTATION"}},
				{"TestProgram", new ColumnInformation() { DataType = "string", ModelName = "TestProgram", ColumnName = "TEST_PROGRAM"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "LOCATION"}},
				{"EqpStatus", new ColumnInformation() { DataType = "string", ModelName = "EqpStatus", ColumnName = "EQP_STATUS"}},
				{"ShopStatus", new ColumnInformation() { DataType = "string", ModelName = "ShopStatus", ColumnName = "SHOP_STATUS"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "SHOP_CYCLE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"ReceiveCategory", new ColumnInformation() { DataType = "string", ModelName = "ReceiveCategory", ColumnName = "RECEIVE_CATEGORY"}},
				{"ReceivedBy", new ColumnInformation() { DataType = "string", ModelName = "ReceivedBy", ColumnName = "RECEIVED_BY"}},
				{"VehicleId", new ColumnInformation() { DataType = "string", ModelName = "VehicleId", ColumnName = "VEHICLE_ID"}},
			};

	public override string ToString() 
	{
		return "wndba.ttracking";
	}
}
}