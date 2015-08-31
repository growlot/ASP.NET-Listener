using System.Collections.Generic;
public class SiteMultimediaImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site = "SITE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileIndex = "FILE_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileName = "FILE_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileType = "FILE_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileDesc = "FILE_DESC";
		/// <summary>
	/// <para />Database Type: byte[]
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileContents = "FILE_CONTENTS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LinkId = "LINK_ID";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LinkIndex = "LINK_INDEX";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OWNER2 = "OWNER2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "CIRCUIT";
	
	public string RealTableName
	{
		get { return "TSITE_MULTIMEDIA".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "SITE"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"FileIndex", new ColumnInformation() { DataType = "int", ModelName = "FileIndex", ColumnName = "FILE_INDEX"}},
				{"FileName", new ColumnInformation() { DataType = "string", ModelName = "FileName", ColumnName = "FILE_NAME"}},
				{"FileType", new ColumnInformation() { DataType = "string", ModelName = "FileType", ColumnName = "FILE_TYPE"}},
				{"FileDesc", new ColumnInformation() { DataType = "string", ModelName = "FileDesc", ColumnName = "FILE_DESC"}},
				{"FileContents", new ColumnInformation() { DataType = "byte[]", ModelName = "FileContents", ColumnName = "FILE_CONTENTS"}},
				{"LinkId", new ColumnInformation() { DataType = "string", ModelName = "LinkId", ColumnName = "LINK_ID"}},
				{"LinkIndex", new ColumnInformation() { DataType = "int", ModelName = "LinkIndex", ColumnName = "LINK_INDEX"}},
				{"OWNER2", new ColumnInformation() { DataType = "int", ModelName = "OWNER2", ColumnName = "OWNER2"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "CIRCUIT"}},
			};

	public override string ToString() 
	{
		return "wndba.tsite_multimedia";
	}
}
