// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class TamperTable: ITableInformation {
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
	public string TamperIndex { get; } = "TAMPER_INDEX";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TamperFound { get; } = "TAMPER_FOUND";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER5 { get; } = "TAMPER5";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER2 { get; } = "TAMPER2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER1 { get; } = "TAMPER1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER3 { get; } = "TAMPER3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER4 { get; } = "TAMPER4";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TAMPER6 { get; } = "TAMPER6";
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
	public string ReleaseDate { get; } = "RELEASE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReleaseBy { get; } = "RELEASE_BY";
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
	
	public string RealTableName
	{
		get { return "TTAMPER".ToUpperInvariant(); }		
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
				{"TamperIndex", new ColumnInformation() { DataType = "int", ModelName = "TamperIndex", ColumnName = "TAMPER_INDEX"}},
				{"TamperFound", new ColumnInformation() { DataType = "string", ModelName = "TamperFound", ColumnName = "TAMPER_FOUND"}},
				{"TAMPER5", new ColumnInformation() { DataType = "string", ModelName = "TAMPER5", ColumnName = "TAMPER5"}},
				{"TAMPER2", new ColumnInformation() { DataType = "string", ModelName = "TAMPER2", ColumnName = "TAMPER2"}},
				{"TAMPER1", new ColumnInformation() { DataType = "string", ModelName = "TAMPER1", ColumnName = "TAMPER1"}},
				{"TAMPER3", new ColumnInformation() { DataType = "string", ModelName = "TAMPER3", ColumnName = "TAMPER3"}},
				{"TAMPER4", new ColumnInformation() { DataType = "string", ModelName = "TAMPER4", ColumnName = "TAMPER4"}},
				{"TAMPER6", new ColumnInformation() { DataType = "string", ModelName = "TAMPER6", ColumnName = "TAMPER6"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"ReleaseDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ReleaseDate", ColumnName = "RELEASE_DATE"}},
				{"ReleaseBy", new ColumnInformation() { DataType = "string", ModelName = "ReleaseBy", ColumnName = "RELEASE_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
			};

	public override string ToString() 
	{
		return "wndba.ttamper";
	}
}
}
#pragma warning restore 1591
