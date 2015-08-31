using System.Collections.Generic;
public class HarmonicConfigDataImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Name = "name";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Revision = "revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Harmonic = "harmonic";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Magnitude = "magnitude";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase = "phase";
	
	public string RealTableName
	{
		get { return "tharmonic_config_data".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Name", new ColumnInformation() { DataType = "string", ModelName = "Name", ColumnName = "name"}},
				{"Revision", new ColumnInformation() { DataType = "int", ModelName = "Revision", ColumnName = "revision"}},
				{"Harmonic", new ColumnInformation() { DataType = "int", ModelName = "Harmonic", ColumnName = "harmonic"}},
				{"Magnitude", new ColumnInformation() { DataType = "decimal", ModelName = "Magnitude", ColumnName = "magnitude"}},
				{"Phase", new ColumnInformation() { DataType = "decimal", ModelName = "Phase", ColumnName = "phase"}},
			};

	public override string ToString() 
	{
		return "wndba.tharmonic_config_data";
	}
}
