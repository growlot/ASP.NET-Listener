using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class HarmonicConfigurationImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string ConfigurationName = "configuration_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Va = "va";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Vb = "vb";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Vc = "vc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ia = "ia";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ib = "ib";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Ic = "ic";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Revision = "revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VaRevision = "va_revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VbRevision = "vb_revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VcRevision = "vc_revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IaRevision = "ia_revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IbRevision = "ib_revision";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IcRevision = "ic_revision";
	
	public string RealTableName
	{
		get { return "tharmonic_configuration".ToLowerInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"ConfigurationName", new ColumnInformation() { DataType = "string", ModelName = "ConfigurationName", ColumnName = "configuration_name"}},
				{"Va", new ColumnInformation() { DataType = "string", ModelName = "Va", ColumnName = "va"}},
				{"Vb", new ColumnInformation() { DataType = "string", ModelName = "Vb", ColumnName = "vb"}},
				{"Vc", new ColumnInformation() { DataType = "string", ModelName = "Vc", ColumnName = "vc"}},
				{"Ia", new ColumnInformation() { DataType = "string", ModelName = "Ia", ColumnName = "ia"}},
				{"Ib", new ColumnInformation() { DataType = "string", ModelName = "Ib", ColumnName = "ib"}},
				{"Ic", new ColumnInformation() { DataType = "string", ModelName = "Ic", ColumnName = "ic"}},
				{"Revision", new ColumnInformation() { DataType = "int", ModelName = "Revision", ColumnName = "revision"}},
				{"VaRevision", new ColumnInformation() { DataType = "int", ModelName = "VaRevision", ColumnName = "va_revision"}},
				{"VbRevision", new ColumnInformation() { DataType = "int", ModelName = "VbRevision", ColumnName = "vb_revision"}},
				{"VcRevision", new ColumnInformation() { DataType = "int", ModelName = "VcRevision", ColumnName = "vc_revision"}},
				{"IaRevision", new ColumnInformation() { DataType = "int", ModelName = "IaRevision", ColumnName = "ia_revision"}},
				{"IbRevision", new ColumnInformation() { DataType = "int", ModelName = "IbRevision", ColumnName = "ib_revision"}},
				{"IcRevision", new ColumnInformation() { DataType = "int", ModelName = "IcRevision", ColumnName = "ic_revision"}},
			};

	public override string ToString() 
	{
		return "wndba.tharmonic_configuration";
	}
}
}
