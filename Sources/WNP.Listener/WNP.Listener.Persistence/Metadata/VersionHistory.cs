using System.Collections.Generic;
public class VersionHistoryImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "eqp_no";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string VersionIndex = "version_index";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldProgramId = "old_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev01 = "OLD_FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev02 = "OLD_FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev04 = "OLD_FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev03 = "OLD_FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewProgramId = "new_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev01 = "NEW_FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev02 = "NEW_FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev04 = "NEW_FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev03 = "NEW_FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeSrc = "change_src";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeDate = "change_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ChangeBy = "change_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev05 = "OLD_FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev06 = "OLD_FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev07 = "OLD_FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string OldFirmwareRev08 = "OLD_FIRMWARE_REV08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev05 = "NEW_FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev06 = "NEW_FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev07 = "NEW_FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewFirmwareRev08 = "NEW_FIRMWARE_REV08";
	
	public string RealTableName
	{
		get { return "TVERSION_HISTORY".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
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
			};

	public override string ToString() 
	{
		return "wndba.tversion_history";
	}
}
