// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class RmaEqpTable: ITableInformation {
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
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpType { get; } = "eqp_type";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaCycle { get; } = "rma_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaStatus { get; } = "rma_status";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchOutNo { get; } = "rma_batch_out_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RmaBatchInNo { get; } = "rma_batch_in_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrevRepairFlag { get; } = "prev_repair_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InWarrantyFlag { get; } = "in_warranty_flag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrevTestProgram { get; } = "prev_test_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InitialProblem { get; } = "initial_problem";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InitialComment { get; } = "initial_comment";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string UtilityDiag { get; } = "utility_diag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VendorDiag { get; } = "vendor_diag";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId1 { get; } = "sent_ami_id1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId2 { get; } = "sent_ami_id2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentAmiId3 { get; } = "sent_ami_id3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentProgramId { get; } = "sent_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentMeterFwRev { get; } = "sent_meter_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentCommFwRev { get; } = "sent_comm_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentExtFwRev { get; } = "sent_ext_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SentBootFwRev { get; } = "sent_boot_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId1 { get; } = "return_ami_id1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId2 { get; } = "return_ami_id2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnAmiId3 { get; } = "return_ami_id3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnProgramId { get; } = "return_program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnMeterFwRev { get; } = "return_meter_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnCommFwRev { get; } = "return_comm_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnExtFwRev { get; } = "return_ext_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReturnBootFwRev { get; } = "return_boot_fw_rev";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairDisp { get; } = "repair_disp";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RepairCost { get; } = "repair_cost";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrereturnTestDate { get; } = "prereturn_test_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReceiveDate { get; } = "receive_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AcceptanceTestDate { get; } = "acceptance_test_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CompleteDate { get; } = "complete_date";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate { get; } = "create_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy { get; } = "create_by";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate { get; } = "mod_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy { get; } = "mod_by";
	
	public string RealTableName
	{
		get { return "trma_eqp".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
		return "WNDBA.TRMA_EQP";
	}
}
}
#pragma warning restore 1591
