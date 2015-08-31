using System.Collections.Generic;
public class SocketMappingImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string FormNo = "form_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Base = "base";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Service = "service";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BaseStyle = "base_style";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FormDescription = "form_description";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseA = "phase_a";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseB = "phase_b";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PhaseC = "phase_c";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Neutral = "neutral";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Kyz = "kyz";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PlcLeftTerminal = "plc_left_terminal";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PlcRightTerminal = "plc_right_terminal";
	
	public string RealTableName
	{
		get { return "tsocket_mapping".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"FormNo", new ColumnInformation() { DataType = "string", ModelName = "FormNo", ColumnName = "form_no"}},
				{"Base", new ColumnInformation() { DataType = "string", ModelName = "Base", ColumnName = "base"}},
				{"Service", new ColumnInformation() { DataType = "string", ModelName = "Service", ColumnName = "service"}},
				{"BaseStyle", new ColumnInformation() { DataType = "string", ModelName = "BaseStyle", ColumnName = "base_style"}},
				{"FormDescription", new ColumnInformation() { DataType = "string", ModelName = "FormDescription", ColumnName = "form_description"}},
				{"PhaseA", new ColumnInformation() { DataType = "int", ModelName = "PhaseA", ColumnName = "phase_a"}},
				{"PhaseB", new ColumnInformation() { DataType = "int", ModelName = "PhaseB", ColumnName = "phase_b"}},
				{"PhaseC", new ColumnInformation() { DataType = "int", ModelName = "PhaseC", ColumnName = "phase_c"}},
				{"Neutral", new ColumnInformation() { DataType = "int", ModelName = "Neutral", ColumnName = "neutral"}},
				{"Kyz", new ColumnInformation() { DataType = "int", ModelName = "Kyz", ColumnName = "kyz"}},
				{"PlcLeftTerminal", new ColumnInformation() { DataType = "int", ModelName = "PlcLeftTerminal", ColumnName = "plc_left_terminal"}},
				{"PlcRightTerminal", new ColumnInformation() { DataType = "int", ModelName = "PlcRightTerminal", ColumnName = "plc_right_terminal"}},
			};

	public override string ToString() 
	{
		return "wndba.tsocket_mapping";
	}
}
