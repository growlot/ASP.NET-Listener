// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class DeviceTestTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string DeviceTestId { get; } = "DeviceTestId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExternalId { get; } = "ExternalId";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DeviceId { get; } = "DeviceId";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestDate { get; } = "TestDate";
	
	public string RealTableName
	{
		get { return "DeviceTest".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"DeviceTestId", new ColumnInformation() { DataType = "int", ModelName = "DeviceTestId", ColumnName = "DeviceTestId"}},
				{"ExternalId", new ColumnInformation() { DataType = "string", ModelName = "ExternalId", ColumnName = "ExternalId"}},
				{"DeviceId", new ColumnInformation() { DataType = "int", ModelName = "DeviceId", ColumnName = "DeviceId"}},
				{"TestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "TestDate", ColumnName = "TestDate"}},
			};

	public override string ToString() 
	{
		return "DBO.DEVICETEST";
	}
}
}
#pragma warning restore 1591
