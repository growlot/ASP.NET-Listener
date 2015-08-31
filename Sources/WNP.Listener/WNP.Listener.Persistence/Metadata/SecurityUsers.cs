using System.Collections.Generic;
public class SecurityUsersImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Username = "USERNAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Password = "PASSWORD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string GroupName = "GROUP_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirstName = "FIRST_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LastName = "LAST_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccountLock = "ACCOUNT_LOCK";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuestionIndex = "question_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string QuestionAnswer = "question_answer";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "mod_by";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DefaultOwner = "DEFAULT_OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DefaultLocation = "DEFAULT_LOCATION";
	
	public string RealTableName
	{
		get { return "TSECURITY_USERS".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
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
			};

	public override string ToString() 
	{
		return "wndba.tsecurity_users";
	}
}
