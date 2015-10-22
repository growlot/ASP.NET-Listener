using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ManufacturercodeImpl: ITableInformation {
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
	public string Mfr = "MFR";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Description = "DESCRIPTION";
	
	public string RealTableName
	{
		get { return "TMANUFACTURERCODE".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "MFR"}},
				{"Description", new ColumnInformation() { DataType = "string", ModelName = "Description", ColumnName = "DESCRIPTION"}},
			};

	public override string ToString() 
	{
		return "wndba.tmanufacturercode";
	}
}
}
