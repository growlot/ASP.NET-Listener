using System.Collections.Generic;
public class DeviceImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string DeviceId = "DeviceId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExternalId = "ExternalId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CompanyId = "CompanyId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceTypeId = "ServiceTypeId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EquipmentNumber = "EquipmentNumber";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EquipmentType = "EquipmentType";
	
	public string RealTableName
	{
		get { return "Device".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"DeviceId", new ColumnInformation() { DataType = "int", ModelName = "DeviceId", ColumnName = "DeviceId"}},
				{"ExternalId", new ColumnInformation() { DataType = "string", ModelName = "ExternalId", ColumnName = "ExternalId"}},
				{"CompanyId", new ColumnInformation() { DataType = "int", ModelName = "CompanyId", ColumnName = "CompanyId"}},
				{"ServiceTypeId", new ColumnInformation() { DataType = "int", ModelName = "ServiceTypeId", ColumnName = "ServiceTypeId"}},
				{"EquipmentNumber", new ColumnInformation() { DataType = "string", ModelName = "EquipmentNumber", ColumnName = "EquipmentNumber"}},
				{"EquipmentType", new ColumnInformation() { DataType = "string", ModelName = "EquipmentType", ColumnName = "EquipmentType"}},
			};

	public override string ToString() 
	{
		return "dbo.device";
	}
}