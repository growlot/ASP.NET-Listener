// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class HarmonicConfigDataTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Name { get; } = "name";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Revision { get; } = "revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Harmonic { get; } = "harmonic";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Magnitude { get; } = "magnitude";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase { get; } = "phase";
	
	public string RealTableName
	{
		get { return "tharmonic_config_data".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
}
#pragma warning restore 1591
