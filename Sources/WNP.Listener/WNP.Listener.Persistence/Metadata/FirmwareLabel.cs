using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class FirmwareLabelImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "owner";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev01 = "label_firmware_rev01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev02 = "label_firmware_rev02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev03 = "label_firmware_rev03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev04 = "label_firmware_rev04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev05 = "label_firmware_rev05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev06 = "label_firmware_rev06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev07 = "label_firmware_rev07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string LabelFirmwareRev08 = "label_firmware_rev08";
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
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev01 = "process_tag_rev01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev02 = "process_tag_rev02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev03 = "process_tag_rev03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev04 = "process_tag_rev04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev05 = "process_tag_rev05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev06 = "process_tag_rev06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev07 = "process_tag_rev07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProcessTagRev08 = "process_tag_rev08";
	
	public string RealTableName
	{
		get { return "tfirmware_label".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"LabelFirmwareRev01", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev01", ColumnName = "label_firmware_rev01"}},
				{"LabelFirmwareRev02", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev02", ColumnName = "label_firmware_rev02"}},
				{"LabelFirmwareRev03", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev03", ColumnName = "label_firmware_rev03"}},
				{"LabelFirmwareRev04", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev04", ColumnName = "label_firmware_rev04"}},
				{"LabelFirmwareRev05", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev05", ColumnName = "label_firmware_rev05"}},
				{"LabelFirmwareRev06", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev06", ColumnName = "label_firmware_rev06"}},
				{"LabelFirmwareRev07", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev07", ColumnName = "label_firmware_rev07"}},
				{"LabelFirmwareRev08", new ColumnInformation() { DataType = "string", ModelName = "LabelFirmwareRev08", ColumnName = "label_firmware_rev08"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"ProcessTagRev01", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev01", ColumnName = "process_tag_rev01"}},
				{"ProcessTagRev02", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev02", ColumnName = "process_tag_rev02"}},
				{"ProcessTagRev03", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev03", ColumnName = "process_tag_rev03"}},
				{"ProcessTagRev04", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev04", ColumnName = "process_tag_rev04"}},
				{"ProcessTagRev05", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev05", ColumnName = "process_tag_rev05"}},
				{"ProcessTagRev06", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev06", ColumnName = "process_tag_rev06"}},
				{"ProcessTagRev07", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev07", ColumnName = "process_tag_rev07"}},
				{"ProcessTagRev08", new ColumnInformation() { DataType = "string", ModelName = "ProcessTagRev08", ColumnName = "process_tag_rev08"}},
			};

	public override string ToString() 
	{
		return "wndba.tfirmware_label";
	}
}
}
