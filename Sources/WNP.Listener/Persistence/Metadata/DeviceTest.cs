using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class DeviceTestImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string DeviceTestId = "DeviceTestId";
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
	public string DeviceId = "DeviceId";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDate = "TestDate";
	
	public string RealTableName
	{
		get { return "DeviceTest".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"DeviceTestId", new ColumnInformation() { DataType = "int", ModelName = "DeviceTestId", ColumnName = "DeviceTestId"}},
				{"ExternalId", new ColumnInformation() { DataType = "string", ModelName = "ExternalId", ColumnName = "ExternalId"}},
				{"DeviceId", new ColumnInformation() { DataType = "int", ModelName = "DeviceId", ColumnName = "DeviceId"}},
				{"TestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDate", ColumnName = "TestDate"}},
			};

	public override string ToString() 
	{
		return "dbo.devicetest";
	}
}
}
