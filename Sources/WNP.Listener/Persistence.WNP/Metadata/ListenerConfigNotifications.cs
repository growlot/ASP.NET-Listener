// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class ListenerConfigNotificationsTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string StateNotificationId { get; } = "state_notification_id";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionState { get; } = "transaction_state";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NotificationEmail { get; } = "notification_email";
	
	public string RealTableName
	{
		get { return "tlistener_config_notifications".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"StateNotificationId", new ColumnInformation() { DataType = "int", ModelName = "StateNotificationId", ColumnName = "state_notification_id"}},
				{"TransactionState", new ColumnInformation() { DataType = "int", ModelName = "TransactionState", ColumnName = "transaction_state"}},
				{"NotificationEmail", new ColumnInformation() { DataType = "string", ModelName = "NotificationEmail", ColumnName = "notification_email"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TLISTENER_CONFIG_NOTIFICATIONS";
	}
}
}
#pragma warning restore 1591
