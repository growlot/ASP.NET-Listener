// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class SecurityUsersTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Username { get; } = "USERNAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Password { get; } = "PASSWORD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string GroupName { get; } = "GROUP_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirstName { get; } = "FIRST_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LastName { get; } = "LAST_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountLock { get; } = "ACCOUNT_LOCK";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuestionIndex { get; } = "question_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuestionAnswer { get; } = "question_answer";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "mod_by";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DefaultOwner { get; } = "DEFAULT_OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DefaultLocation { get; } = "DEFAULT_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EmployeeId { get; } = "employee_id";
	
	public string RealTableName
	{
		get { return "TSECURITY_USERS".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Username", new ColumnInformation() { DataType = "string", ModelName = "Username", ColumnName = "USERNAME"}},
				{"Password", new ColumnInformation() { DataType = "string", ModelName = "Password", ColumnName = "PASSWORD"}},
				{"GroupName", new ColumnInformation() { DataType = "string", ModelName = "GroupName", ColumnName = "GROUP_NAME"}},
				{"FirstName", new ColumnInformation() { DataType = "string", ModelName = "FirstName", ColumnName = "FIRST_NAME"}},
				{"LastName", new ColumnInformation() { DataType = "string", ModelName = "LastName", ColumnName = "LAST_NAME"}},
				{"AccountLock", new ColumnInformation() { DataType = "string", ModelName = "AccountLock", ColumnName = "ACCOUNT_LOCK"}},
				{"QuestionIndex", new ColumnInformation() { DataType = "int", ModelName = "QuestionIndex", ColumnName = "question_index"}},
				{"QuestionAnswer", new ColumnInformation() { DataType = "string", ModelName = "QuestionAnswer", ColumnName = "question_answer"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"DefaultOwner", new ColumnInformation() { DataType = "int", ModelName = "DefaultOwner", ColumnName = "DEFAULT_OWNER"}},
				{"DefaultLocation", new ColumnInformation() { DataType = "string", ModelName = "DefaultLocation", ColumnName = "DEFAULT_LOCATION"}},
				{"EmployeeId", new ColumnInformation() { DataType = "string", ModelName = "EmployeeId", ColumnName = "employee_id"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TSECURITY_USERS";
	}
}
}
#pragma warning restore 1591