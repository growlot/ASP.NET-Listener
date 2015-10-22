using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class CompanyImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string CompanyId = "CompanyId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExternalId = "ExternalId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Name = "Name";
	
	public string RealTableName
	{
		get { return "Company".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"CompanyId", new ColumnInformation() { DataType = "int", ModelName = "CompanyId", ColumnName = "CompanyId"}},
				{"ExternalId", new ColumnInformation() { DataType = "string", ModelName = "ExternalId", ColumnName = "ExternalId"}},
				{"Name", new ColumnInformation() { DataType = "string", ModelName = "Name", ColumnName = "Name"}},
			};

	public override string ToString() 
	{
		return "dbo.company";
	}
}
}
