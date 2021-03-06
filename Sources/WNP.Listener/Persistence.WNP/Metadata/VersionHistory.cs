// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class VersionHistoryTable: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner { get; } = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo { get; } = "eqp_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string VersionIndex { get; } = "version_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldProgramId { get; } = "old_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev01 { get; } = "OLD_FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev02 { get; } = "OLD_FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev04 { get; } = "OLD_FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev03 { get; } = "OLD_FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewProgramId { get; } = "new_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev01 { get; } = "NEW_FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev02 { get; } = "NEW_FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev04 { get; } = "NEW_FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev03 { get; } = "NEW_FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeSrc { get; } = "change_src";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeDate { get; } = "change_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeBy { get; } = "change_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev05 { get; } = "OLD_FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev06 { get; } = "OLD_FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev07 { get; } = "OLD_FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev08 { get; } = "OLD_FIRMWARE_REV08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev05 { get; } = "NEW_FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev06 { get; } = "NEW_FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev07 { get; } = "NEW_FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev08 { get; } = "NEW_FIRMWARE_REV08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType { get; } = "eqp_type";
	
	public string RealTableName
	{
		get { return "TVERSION_HISTORY".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"VersionIndex", new ColumnInformation() { DataType = "int", ModelName = "VersionIndex", ColumnName = "version_index"}},
				{"OldProgramId", new ColumnInformation() { DataType = "string", ModelName = "OldProgramId", ColumnName = "old_program_id"}},
				{"OldFirmwareRev01", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev01", ColumnName = "OLD_FIRMWARE_REV01"}},
				{"OldFirmwareRev02", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev02", ColumnName = "OLD_FIRMWARE_REV02"}},
				{"OldFirmwareRev04", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev04", ColumnName = "OLD_FIRMWARE_REV04"}},
				{"OldFirmwareRev03", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev03", ColumnName = "OLD_FIRMWARE_REV03"}},
				{"NewProgramId", new ColumnInformation() { DataType = "string", ModelName = "NewProgramId", ColumnName = "new_program_id"}},
				{"NewFirmwareRev01", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev01", ColumnName = "NEW_FIRMWARE_REV01"}},
				{"NewFirmwareRev02", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev02", ColumnName = "NEW_FIRMWARE_REV02"}},
				{"NewFirmwareRev04", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev04", ColumnName = "NEW_FIRMWARE_REV04"}},
				{"NewFirmwareRev03", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev03", ColumnName = "NEW_FIRMWARE_REV03"}},
				{"ChangeSrc", new ColumnInformation() { DataType = "string", ModelName = "ChangeSrc", ColumnName = "change_src"}},
				{"ChangeDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ChangeDate", ColumnName = "change_date"}},
				{"ChangeBy", new ColumnInformation() { DataType = "string", ModelName = "ChangeBy", ColumnName = "change_by"}},
				{"OldFirmwareRev05", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev05", ColumnName = "OLD_FIRMWARE_REV05"}},
				{"OldFirmwareRev06", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev06", ColumnName = "OLD_FIRMWARE_REV06"}},
				{"OldFirmwareRev07", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev07", ColumnName = "OLD_FIRMWARE_REV07"}},
				{"OldFirmwareRev08", new ColumnInformation() { DataType = "string", ModelName = "OldFirmwareRev08", ColumnName = "OLD_FIRMWARE_REV08"}},
				{"NewFirmwareRev05", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev05", ColumnName = "NEW_FIRMWARE_REV05"}},
				{"NewFirmwareRev06", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev06", ColumnName = "NEW_FIRMWARE_REV06"}},
				{"NewFirmwareRev07", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev07", ColumnName = "NEW_FIRMWARE_REV07"}},
				{"NewFirmwareRev08", new ColumnInformation() { DataType = "string", ModelName = "NewFirmwareRev08", ColumnName = "NEW_FIRMWARE_REV08"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TVERSION_HISTORY";
	}
}
}
#pragma warning restore 1591
