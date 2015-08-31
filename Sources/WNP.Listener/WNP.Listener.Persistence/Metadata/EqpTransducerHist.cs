using System.Collections.Generic;
public class EqpTransducerHistImpl: ITableInformation {
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
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteDate = "site_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site = "site";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "circuit";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CatalogNo = "catalog_no";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PurchaseDate = "purchase_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WarrantyPeriod = "warranty_period";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PoRef = "po_ref";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireCode = "retire_code";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireDate = "retire_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireBy = "retire_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireLocation = "retire_location";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpStatus = "eqp_status";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StatusDate = "status_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopStatus = "shop_status";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "shop_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "location";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Shelf = "shelf";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BoxNo = "box_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PalletNo = "pallet_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewBatchNo = "NEW_BATCH_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StrataName = "strata_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionProgram = "selection_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestProgram = "test_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextTestDue = "next_test_due";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestPeriod = "test_period";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SerialNo = "serial_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr = "mfr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModelNo = "model_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Type = "type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Output = "output";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Scale = "scale";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestSequenceDesc = "test_sequence_desc";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser01 = "trans_user01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser02 = "trans_user02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser03 = "trans_user03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser04 = "trans_user04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser05 = "trans_user05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser06 = "trans_user06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser07 = "trans_user07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser08 = "trans_user08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser09 = "trans_user09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransUser10 = "trans_user10";
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
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MfrBuildDate = "MFR_BUILD_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionId = "transaction_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AltEqpNo = "alt_eqp_no";
	
	public string RealTableName
	{
		get { return "TEQP_TRANSDUCER_HIST".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "owner"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "eqp_no"}},
				{"SiteDate", new ColumnInformation() { DataType = "DateTime", ModelName = "SiteDate", ColumnName = "site_date"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "site"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "circuit"}},
				{"CatalogNo", new ColumnInformation() { DataType = "string", ModelName = "CatalogNo", ColumnName = "catalog_no"}},
				{"PurchaseDate", new ColumnInformation() { DataType = "DateTime", ModelName = "PurchaseDate", ColumnName = "purchase_date"}},
				{"WarrantyPeriod", new ColumnInformation() { DataType = "int", ModelName = "WarrantyPeriod", ColumnName = "warranty_period"}},
				{"PoRef", new ColumnInformation() { DataType = "string", ModelName = "PoRef", ColumnName = "po_ref"}},
				{"RetireCode", new ColumnInformation() { DataType = "string", ModelName = "RetireCode", ColumnName = "retire_code"}},
				{"RetireDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RetireDate", ColumnName = "retire_date"}},
				{"RetireBy", new ColumnInformation() { DataType = "string", ModelName = "RetireBy", ColumnName = "retire_by"}},
				{"RetireLocation", new ColumnInformation() { DataType = "string", ModelName = "RetireLocation", ColumnName = "retire_location"}},
				{"EqpStatus", new ColumnInformation() { DataType = "string", ModelName = "EqpStatus", ColumnName = "eqp_status"}},
				{"StatusDate", new ColumnInformation() { DataType = "DateTime", ModelName = "StatusDate", ColumnName = "status_date"}},
				{"ShopStatus", new ColumnInformation() { DataType = "string", ModelName = "ShopStatus", ColumnName = "shop_status"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "shop_cycle"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "location"}},
				{"Shelf", new ColumnInformation() { DataType = "string", ModelName = "Shelf", ColumnName = "shelf"}},
				{"BoxNo", new ColumnInformation() { DataType = "string", ModelName = "BoxNo", ColumnName = "box_no"}},
				{"PalletNo", new ColumnInformation() { DataType = "string", ModelName = "PalletNo", ColumnName = "pallet_no"}},
				{"NewBatchNo", new ColumnInformation() { DataType = "string", ModelName = "NewBatchNo", ColumnName = "NEW_BATCH_NO"}},
				{"StrataName", new ColumnInformation() { DataType = "string", ModelName = "StrataName", ColumnName = "strata_name"}},
				{"SelectionProgram", new ColumnInformation() { DataType = "string", ModelName = "SelectionProgram", ColumnName = "selection_program"}},
				{"TestProgram", new ColumnInformation() { DataType = "string", ModelName = "TestProgram", ColumnName = "test_program"}},
				{"NextTestDue", new ColumnInformation() { DataType = "string", ModelName = "NextTestDue", ColumnName = "next_test_due"}},
				{"TestPeriod", new ColumnInformation() { DataType = "string", ModelName = "TestPeriod", ColumnName = "test_period"}},
				{"SerialNo", new ColumnInformation() { DataType = "string", ModelName = "SerialNo", ColumnName = "serial_no"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "mfr"}},
				{"ModelNo", new ColumnInformation() { DataType = "string", ModelName = "ModelNo", ColumnName = "model_no"}},
				{"Type", new ColumnInformation() { DataType = "string", ModelName = "Type", ColumnName = "type"}},
				{"Output", new ColumnInformation() { DataType = "string", ModelName = "Output", ColumnName = "output"}},
				{"Scale", new ColumnInformation() { DataType = "string", ModelName = "Scale", ColumnName = "scale"}},
				{"TestSequenceDesc", new ColumnInformation() { DataType = "string", ModelName = "TestSequenceDesc", ColumnName = "test_sequence_desc"}},
				{"TransUser01", new ColumnInformation() { DataType = "string", ModelName = "TransUser01", ColumnName = "trans_user01"}},
				{"TransUser02", new ColumnInformation() { DataType = "string", ModelName = "TransUser02", ColumnName = "trans_user02"}},
				{"TransUser03", new ColumnInformation() { DataType = "string", ModelName = "TransUser03", ColumnName = "trans_user03"}},
				{"TransUser04", new ColumnInformation() { DataType = "string", ModelName = "TransUser04", ColumnName = "trans_user04"}},
				{"TransUser05", new ColumnInformation() { DataType = "string", ModelName = "TransUser05", ColumnName = "trans_user05"}},
				{"TransUser06", new ColumnInformation() { DataType = "string", ModelName = "TransUser06", ColumnName = "trans_user06"}},
				{"TransUser07", new ColumnInformation() { DataType = "string", ModelName = "TransUser07", ColumnName = "trans_user07"}},
				{"TransUser08", new ColumnInformation() { DataType = "string", ModelName = "TransUser08", ColumnName = "trans_user08"}},
				{"TransUser09", new ColumnInformation() { DataType = "string", ModelName = "TransUser09", ColumnName = "trans_user09"}},
				{"TransUser10", new ColumnInformation() { DataType = "string", ModelName = "TransUser10", ColumnName = "trans_user10"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"MfrBuildDate", new ColumnInformation() { DataType = "DateTime", ModelName = "MfrBuildDate", ColumnName = "MFR_BUILD_DATE"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"AltEqpNo", new ColumnInformation() { DataType = "string", ModelName = "AltEqpNo", ColumnName = "alt_eqp_no"}},
			};

	public override string ToString() 
	{
		return "wndba.teqp_transducer_hist";
	}
}
