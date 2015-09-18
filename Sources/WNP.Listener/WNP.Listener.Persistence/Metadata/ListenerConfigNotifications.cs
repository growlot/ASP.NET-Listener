using System.Collections.Generic;
public class ListenerConfigNotificationsImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string StateNotificationId = "state_notification_id";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionState = "transaction_state";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NotificationEmail = "notification_email";
	
	public string RealTableName
	{
		get { return "tlistener_config_notifications".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"StateNotificationId", new ColumnInformation() { DataType = "int", ModelName = "StateNotificationId", ColumnName = "state_notification_id"}},
				{"TransactionState", new ColumnInformation() { DataType = "int", ModelName = "TransactionState", ColumnName = "transaction_state"}},
				{"NotificationEmail", new ColumnInformation() { DataType = "string", ModelName = "NotificationEmail", ColumnName = "notification_email"}},
			};

	public override string ToString() 
	{
		return "wndba.tlistener_config_notifications";
	}
}