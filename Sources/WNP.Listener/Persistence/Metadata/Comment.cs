// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class CommentTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo { get; } = "EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType { get; } = "EQP_TYPE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string CommentIndex { get; } = "COMMENT_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE1 { get; } = "TROUBLE1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE2 { get; } = "TROUBLE2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE3 { get; } = "TROUBLE3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE4 { get; } = "TROUBLE4";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TROUBLE5 { get; } = "TROUBLE5";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CommentType { get; } = "COMMENT_TYPE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CommentSrc { get; } = "COMMENT_SRC";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Comments { get; } = "COMMENTS";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "MOD_BY";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id { get; } = "ID";
	
	public string RealTableName
	{
		get { return "TCOMMENT".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "EQP_TYPE"}},
				{"CommentIndex", new ColumnInformation() { DataType = "int", ModelName = "CommentIndex", ColumnName = "COMMENT_INDEX"}},
				{"TROUBLE1", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE1", ColumnName = "TROUBLE1"}},
				{"TROUBLE2", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE2", ColumnName = "TROUBLE2"}},
				{"TROUBLE3", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE3", ColumnName = "TROUBLE3"}},
				{"TROUBLE4", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE4", ColumnName = "TROUBLE4"}},
				{"TROUBLE5", new ColumnInformation() { DataType = "string", ModelName = "TROUBLE5", ColumnName = "TROUBLE5"}},
				{"CommentType", new ColumnInformation() { DataType = "string", ModelName = "CommentType", ColumnName = "COMMENT_TYPE"}},
				{"CommentSrc", new ColumnInformation() { DataType = "string", ModelName = "CommentSrc", ColumnName = "COMMENT_SRC"}},
				{"Comments", new ColumnInformation() { DataType = "string", ModelName = "Comments", ColumnName = "COMMENTS"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
			};

	public override string ToString() 
	{
		return "wndba.tcomment";
	}
}
}
#pragma warning restore 1591
