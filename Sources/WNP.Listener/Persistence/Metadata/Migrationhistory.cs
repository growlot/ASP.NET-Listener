using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MigrationhistoryImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string MigrationId = "MigrationId";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ContextKey = "ContextKey";
		/// <summary>
	/// <para />Database Type: byte[]
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Model = "Model";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProductVersion = "ProductVersion";
	
	public string RealTableName
	{
		get { return "__MigrationHistory".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"MigrationId", new ColumnInformation() { DataType = "string", ModelName = "MigrationId", ColumnName = "MigrationId"}},
				{"ContextKey", new ColumnInformation() { DataType = "string", ModelName = "ContextKey", ColumnName = "ContextKey"}},
				{"Model", new ColumnInformation() { DataType = "byte[]", ModelName = "Model", ColumnName = "Model"}},
				{"ProductVersion", new ColumnInformation() { DataType = "string", ModelName = "ProductVersion", ColumnName = "ProductVersion"}},
			};

	public override string ToString() 
	{
		return "dbo.__migrationhistory";
	}
}
}
