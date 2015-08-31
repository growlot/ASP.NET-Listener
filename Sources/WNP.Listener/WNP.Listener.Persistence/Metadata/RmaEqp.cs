using System.Collections.Generic;
public class RmaEqpImpl: ITableInformation {
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
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType = "eqp_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaCycle = "rma_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaStatus = "rma_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchOutNo = "rma_batch_out_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchInNo = "rma_batch_in_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrevRepairFlag = "prev_repair_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InWarrantyFlag = "in_warranty_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrevTestProgram = "prev_test_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InitialProblem = "initial_problem";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InitialComment = "initial_comment";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UtilityDiag = "utility_diag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorDiag = "vendor_diag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId1 = "sent_ami_id1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId2 = "sent_ami_id2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId3 = "sent_ami_id3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentProgramId = "sent_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentMeterFwRev = "sent_meter_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentCommFwRev = "sent_comm_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentExtFwRev = "sent_ext_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentBootFwRev = "sent_boot_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId1 = "return_ami_id1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId2 = "return_ami_id2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId3 = "return_ami_id3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnProgramId = "return_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnMeterFwRev = "return_meter_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnCommFwRev = "return_comm_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnExtFwRev = "return_ext_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnBootFwRev = "return_boot_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairDisp = "repair_disp";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairCost = "repair_cost";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrereturnTestDate = "prereturn_test_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReceiveDate = "receive_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceTestDate = "acceptance_test_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CompleteDate = "complete_date";
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
	
	public string RealTableName
	{
		get { return "trma_eqp".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"EqpType", new ColumnInformation() { DataType = "string", ModelName = "EqpType", ColumnName = "eqp_type"}},
				{"RmaCycle", new ColumnInformation() { DataType = "int", ModelName = "RmaCycle", ColumnName = "rma_cycle"}},
				{"RmaStatus", new ColumnInformation() { DataType = "string", ModelName = "RmaStatus", ColumnName = "rma_status"}},
				{"RmaBatchOutNo", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchOutNo", ColumnName = "rma_batch_out_no"}},
				{"RmaBatchInNo", new ColumnInformation() { DataType = "string", ModelName = "RmaBatchInNo", ColumnName = "rma_batch_in_no"}},
				{"PrevRepairFlag", new ColumnInformation() { DataType = "string", ModelName = "PrevRepairFlag", ColumnName = "prev_repair_flag"}},
				{"InWarrantyFlag", new ColumnInformation() { DataType = "string", ModelName = "InWarrantyFlag", ColumnName = "in_warranty_flag"}},
				{"PrevTestProgram", new ColumnInformation() { DataType = "string", ModelName = "PrevTestProgram", ColumnName = "prev_test_program"}},
				{"InitialProblem", new ColumnInformation() { DataType = "string", ModelName = "InitialProblem", ColumnName = "initial_problem"}},
				{"InitialComment", new ColumnInformation() { DataType = "string", ModelName = "InitialComment", ColumnName = "initial_comment"}},
				{"UtilityDiag", new ColumnInformation() { DataType = "string", ModelName = "UtilityDiag", ColumnName = "utility_diag"}},
				{"VendorDiag", new ColumnInformation() { DataType = "string", ModelName = "VendorDiag", ColumnName = "vendor_diag"}},
				{"SentAmiId1", new ColumnInformation() { DataType = "string", ModelName = "SentAmiId1", ColumnName = "sent_ami_id1"}},
				{"SentAmiId2", new ColumnInformation() { DataType = "string", ModelName = "SentAmiId2", ColumnName = "sent_ami_id2"}},
				{"SentAmiId3", new ColumnInformation() { DataType = "string", ModelName = "SentAmiId3", ColumnName = "sent_ami_id3"}},
				{"SentProgramId", new ColumnInformation() { DataType = "string", ModelName = "SentProgramId", ColumnName = "sent_program_id"}},
				{"SentMeterFwRev", new ColumnInformation() { DataType = "string", ModelName = "SentMeterFwRev", ColumnName = "sent_meter_fw_rev"}},
				{"SentCommFwRev", new ColumnInformation() { DataType = "string", ModelName = "SentCommFwRev", ColumnName = "sent_comm_fw_rev"}},
				{"SentExtFwRev", new ColumnInformation() { DataType = "string", ModelName = "SentExtFwRev", ColumnName = "sent_ext_fw_rev"}},
				{"SentBootFwRev", new ColumnInformation() { DataType = "string", ModelName = "SentBootFwRev", ColumnName = "sent_boot_fw_rev"}},
				{"ReturnAmiId1", new ColumnInformation() { DataType = "string", ModelName = "ReturnAmiId1", ColumnName = "return_ami_id1"}},
				{"ReturnAmiId2", new ColumnInformation() { DataType = "string", ModelName = "ReturnAmiId2", ColumnName = "return_ami_id2"}},
				{"ReturnAmiId3", new ColumnInformation() { DataType = "string", ModelName = "ReturnAmiId3", ColumnName = "return_ami_id3"}},
				{"ReturnProgramId", new ColumnInformation() { DataType = "string", ModelName = "ReturnProgramId", ColumnName = "return_program_id"}},
				{"ReturnMeterFwRev", new ColumnInformation() { DataType = "string", ModelName = "ReturnMeterFwRev", ColumnName = "return_meter_fw_rev"}},
				{"ReturnCommFwRev", new ColumnInformation() { DataType = "string", ModelName = "ReturnCommFwRev", ColumnName = "return_comm_fw_rev"}},
				{"ReturnExtFwRev", new ColumnInformation() { DataType = "string", ModelName = "ReturnExtFwRev", ColumnName = "return_ext_fw_rev"}},
				{"ReturnBootFwRev", new ColumnInformation() { DataType = "string", ModelName = "ReturnBootFwRev", ColumnName = "return_boot_fw_rev"}},
				{"RepairDisp", new ColumnInformation() { DataType = "string", ModelName = "RepairDisp", ColumnName = "repair_disp"}},
				{"RepairCost", new ColumnInformation() { DataType = "decimal", ModelName = "RepairCost", ColumnName = "repair_cost"}},
				{"PrereturnTestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "PrereturnTestDate", ColumnName = "prereturn_test_date"}},
				{"ReceiveDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ReceiveDate", ColumnName = "receive_date"}},
				{"AcceptanceTestDate", new ColumnInformation() { DataType = "DateTime", ModelName = "AcceptanceTestDate", ColumnName = "acceptance_test_date"}},
				{"CompleteDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CompleteDate", ColumnName = "complete_date"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
			};

	public override string ToString() 
	{
		return "wndba.trma_eqp";
	}
}
