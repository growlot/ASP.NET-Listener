// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class PbcatedtTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeName { get; } = "pbe_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeEdit { get; } = "pbe_edit";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeType { get; } = "pbe_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeCntr { get; } = "pbe_cntr";
		/// <summary>
	/// <para />Database Type: short
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeSeqn { get; } = "pbe_seqn";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeFlag { get; } = "pbe_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PbeWork { get; } = "pbe_work";
	
	public string RealTableName
	{
		get { return "pbcatedt".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"PbeName", new ColumnInformation() { DataType = "string", ModelName = "PbeName", ColumnName = "pbe_name"}},
				{"PbeEdit", new ColumnInformation() { DataType = "string", ModelName = "PbeEdit", ColumnName = "pbe_edit"}},
				{"PbeType", new ColumnInformation() { DataType = "short", ModelName = "PbeType", ColumnName = "pbe_type"}},
				{"PbeCntr", new ColumnInformation() { DataType = "int", ModelName = "PbeCntr", ColumnName = "pbe_cntr"}},
				{"PbeSeqn", new ColumnInformation() { DataType = "short", ModelName = "PbeSeqn", ColumnName = "pbe_seqn"}},
				{"PbeFlag", new ColumnInformation() { DataType = "int", ModelName = "PbeFlag", ColumnName = "pbe_flag"}},
				{"PbeWork", new ColumnInformation() { DataType = "string", ModelName = "PbeWork", ColumnName = "pbe_work"}},
			};

	public override string ToString() 
	{
		return "WNDBA.PBCATEDT";
	}
}
}
#pragma warning restore 1591
