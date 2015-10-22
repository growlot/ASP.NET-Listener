using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class LotPercentDefImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SampleSize = "SAMPLE_SIZE";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string QualityIndex = "QUALITY_INDEX";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PercentDef = "PERCENT_DEF";
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
	
	public string RealTableName
	{
		get { return "TLOT_PERCENT_DEF".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"SampleSize", new ColumnInformation() { DataType = "int", ModelName = "SampleSize", ColumnName = "SAMPLE_SIZE"}},
				{"QualityIndex", new ColumnInformation() { DataType = "decimal", ModelName = "QualityIndex", ColumnName = "QUALITY_INDEX"}},
				{"PercentDef", new ColumnInformation() { DataType = "decimal", ModelName = "PercentDef", ColumnName = "PERCENT_DEF"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
			};

	public override string ToString() 
	{
		return "wndba.tlot_percent_def";
	}
}
}
