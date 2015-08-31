using System.Collections.Generic;
public class EventTriggersImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Workstation = "workstation";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string WorkstationEvent = "workstation_event";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EventTrigger1 = "event_trigger_1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EventTrigger2 = "event_trigger_2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EventTrigger3 = "event_trigger_3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
	
	public string RealTableName
	{
		get { return "tevent_triggers".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"Workstation", new ColumnInformation() { DataType = "string", ModelName = "Workstation", ColumnName = "workstation"}},
				{"WorkstationEvent", new ColumnInformation() { DataType = "string", ModelName = "WorkstationEvent", ColumnName = "workstation_event"}},
				{"EventTrigger1", new ColumnInformation() { DataType = "string", ModelName = "EventTrigger1", ColumnName = "event_trigger_1"}},
				{"EventTrigger2", new ColumnInformation() { DataType = "string", ModelName = "EventTrigger2", ColumnName = "event_trigger_2"}},
				{"EventTrigger3", new ColumnInformation() { DataType = "string", ModelName = "EventTrigger3", ColumnName = "event_trigger_3"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
			};

	public override string ToString() 
	{
		return "wndba.tevent_triggers";
	}
}
