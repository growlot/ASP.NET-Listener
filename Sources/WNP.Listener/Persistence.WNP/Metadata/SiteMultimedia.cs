// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class SiteMultimediaTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "OWNER";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site { get; } = "SITE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileIndex { get; } = "FILE_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileName { get; } = "FILE_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileType { get; } = "FILE_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileDesc { get; } = "FILE_DESC";
		/// <summary>
	/// <para />Database Type: byte[]
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FileContents { get; } = "FILE_CONTENTS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LinkId { get; } = "LINK_ID";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LinkIndex { get; } = "LINK_INDEX";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OWNER2 { get; } = "OWNER2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo { get; } = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType { get; } = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit { get; } = "CIRCUIT";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id { get; } = "ID";
	
	public string RealTableName
	{
		get { return "TSITE_MULTIMEDIA".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TSITE_MULTIMEDIA";
	}
}
}
#pragma warning restore 1591