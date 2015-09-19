using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class MultimediaImpl: ITableInformation {
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
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
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
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
	
	public string RealTableName
	{
		get { return "TMULTIMEDIA".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"FileIndex", new ColumnInformation() { DataType = "int", ModelName = "FileIndex", ColumnName = "FILE_INDEX"}},
				{"FileName", new ColumnInformation() { DataType = "string", ModelName = "FileName", ColumnName = "FILE_NAME"}},
				{"FileType", new ColumnInformation() { DataType = "string", ModelName = "FileType", ColumnName = "FILE_TYPE"}},
				{"FileDesc", new ColumnInformation() { DataType = "string", ModelName = "FileDesc", ColumnName = "FILE_DESC"}},
				{"FileContents", new ColumnInformation() { DataType = "byte[]", ModelName = "FileContents", ColumnName = "FILE_CONTENTS"}},
				{"LinkId", new ColumnInformation() { DataType = "string", ModelName = "LinkId", ColumnName = "LINK_ID"}},
				{"LinkIndex", new ColumnInformation() { DataType = "int", ModelName = "LinkIndex", ColumnName = "LINK_INDEX"}},
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
			};

	public override string ToString() 
	{
		return "wndba.tmultimedia";
	}
}
}
