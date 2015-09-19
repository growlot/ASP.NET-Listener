using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class ImportExportOptionsImpl: ITableInformation {
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
	public string DefinitionName = "DEFINITION_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EquipmentType = "EQUIPMENT_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DelimitChar = "DELIMIT_CHAR";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExportPathFile1 = "EXPORT_PATH_FILE1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExportPathFile2 = "EXPORT_PATH_FILE2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WinboardSaveExport = "WINBOARD_SAVE_EXPORT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExportAppend = "EXPORT_APPEND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ImportPathFile = "IMPORT_PATH_FILE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScheduleEnable = "SCHEDULE_ENABLE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SchedulePeriod = "SCHEDULE_PERIOD";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScheduleTime = "SCHEDULE_TIME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScheduleDay = "SCHEDULE_DAY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ScheduleFrequency = "SCHEDULE_FREQUENCY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PreExportCommand = "PRE_EXPORT_COMMAND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PostExportCommand = "POST_EXPORT_COMMAND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PreImportCommand = "PRE_IMPORT_COMMAND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PostImportCommand = "POST_IMPORT_COMMAND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DatetimeOnFilename1 = "datetime_on_filename1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DatetimeOnFilename2 = "datetime_on_filename2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ExportToWattnet = "export_to_wattnet";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Service = "SERVICE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string DataviewLayout = "DATAVIEW_LAYOUT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceUrl = "SERVICE_URL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServiceUid = "SERVICE_UID";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ServicePwd = "SERVICE_PWD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AdvancedEditing = "advanced_editing";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string HasFileHeader = "has_file_header";
	
	public string RealTableName
	{
		get { return "TIMPORT_EXPORT_OPTIONS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"DefinitionName", new ColumnInformation() { DataType = "string", ModelName = "DefinitionName", ColumnName = "DEFINITION_NAME"}},
				{"EquipmentType", new ColumnInformation() { DataType = "string", ModelName = "EquipmentType", ColumnName = "EQUIPMENT_TYPE"}},
				{"DelimitChar", new ColumnInformation() { DataType = "string", ModelName = "DelimitChar", ColumnName = "DELIMIT_CHAR"}},
				{"ExportPathFile1", new ColumnInformation() { DataType = "string", ModelName = "ExportPathFile1", ColumnName = "EXPORT_PATH_FILE1"}},
				{"ExportPathFile2", new ColumnInformation() { DataType = "string", ModelName = "ExportPathFile2", ColumnName = "EXPORT_PATH_FILE2"}},
				{"WinboardSaveExport", new ColumnInformation() { DataType = "string", ModelName = "WinboardSaveExport", ColumnName = "WINBOARD_SAVE_EXPORT"}},
				{"ExportAppend", new ColumnInformation() { DataType = "string", ModelName = "ExportAppend", ColumnName = "EXPORT_APPEND"}},
				{"ImportPathFile", new ColumnInformation() { DataType = "string", ModelName = "ImportPathFile", ColumnName = "IMPORT_PATH_FILE"}},
				{"ScheduleEnable", new ColumnInformation() { DataType = "string", ModelName = "ScheduleEnable", ColumnName = "SCHEDULE_ENABLE"}},
				{"SchedulePeriod", new ColumnInformation() { DataType = "string", ModelName = "SchedulePeriod", ColumnName = "SCHEDULE_PERIOD"}},
				{"ScheduleTime", new ColumnInformation() { DataType = "DateTime", ModelName = "ScheduleTime", ColumnName = "SCHEDULE_TIME"}},
				{"ScheduleDay", new ColumnInformation() { DataType = "string", ModelName = "ScheduleDay", ColumnName = "SCHEDULE_DAY"}},
				{"ScheduleFrequency", new ColumnInformation() { DataType = "int", ModelName = "ScheduleFrequency", ColumnName = "SCHEDULE_FREQUENCY"}},
				{"PreExportCommand", new ColumnInformation() { DataType = "string", ModelName = "PreExportCommand", ColumnName = "PRE_EXPORT_COMMAND"}},
				{"PostExportCommand", new ColumnInformation() { DataType = "string", ModelName = "PostExportCommand", ColumnName = "POST_EXPORT_COMMAND"}},
				{"PreImportCommand", new ColumnInformation() { DataType = "string", ModelName = "PreImportCommand", ColumnName = "PRE_IMPORT_COMMAND"}},
				{"PostImportCommand", new ColumnInformation() { DataType = "string", ModelName = "PostImportCommand", ColumnName = "POST_IMPORT_COMMAND"}},
				{"DatetimeOnFilename1", new ColumnInformation() { DataType = "string", ModelName = "DatetimeOnFilename1", ColumnName = "datetime_on_filename1"}},
				{"DatetimeOnFilename2", new ColumnInformation() { DataType = "string", ModelName = "DatetimeOnFilename2", ColumnName = "datetime_on_filename2"}},
				{"ExportToWattnet", new ColumnInformation() { DataType = "string", ModelName = "ExportToWattnet", ColumnName = "export_to_wattnet"}},
				{"Service", new ColumnInformation() { DataType = "string", ModelName = "Service", ColumnName = "SERVICE"}},
				{"DataviewLayout", new ColumnInformation() { DataType = "string", ModelName = "DataviewLayout", ColumnName = "DATAVIEW_LAYOUT"}},
				{"ServiceUrl", new ColumnInformation() { DataType = "string", ModelName = "ServiceUrl", ColumnName = "SERVICE_URL"}},
				{"ServiceUid", new ColumnInformation() { DataType = "string", ModelName = "ServiceUid", ColumnName = "SERVICE_UID"}},
				{"ServicePwd", new ColumnInformation() { DataType = "string", ModelName = "ServicePwd", ColumnName = "SERVICE_PWD"}},
				{"AdvancedEditing", new ColumnInformation() { DataType = "string", ModelName = "AdvancedEditing", ColumnName = "advanced_editing"}},
				{"HasFileHeader", new ColumnInformation() { DataType = "string", ModelName = "HasFileHeader", ColumnName = "has_file_header"}},
			};

	public override string ToString() 
	{
		return "wndba.timport_export_options";
	}
}
}
