using System.Collections.Generic;
public class EqpCtImpl: ITableInformation {
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: True
	/// </summary>
	public string Owner = "OWNER";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpNo = "EQP_NO";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site = "SITE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit = "CIRCUIT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CatalogNo = "CATALOG_NO";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PurchaseDate = "PURCHASE_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WarrantyPeriod = "WARRANTY_PERIOD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PoRef = "PO_REF";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireCode = "RETIRE_CODE";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireDate = "RETIRE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireBy = "RETIRE_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireLocation = "RETIRE_LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpStatus = "EQP_STATUS";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StatusDate = "STATUS_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopStatus = "SHOP_STATUS";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle = "SHOP_CYCLE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location = "LOCATION";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Shelf = "SHELF";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BoxNo = "BOX_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PalletNo = "PALLET_NO";
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
	public string StrataName = "STRATA_NAME";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestProgram = "TEST_PROGRAM";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextTestDue = "NEXT_TEST_DUE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestPeriod = "TEST_PERIOD";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SerialNo = "SERIAL_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModelNo = "MODEL_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NameplateRatio = "NAMEPLATE_RATIO";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectedMult = "SELECTED_MULT";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AuxMult = "AUX_MULT";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser01 = "CT_USER01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser02 = "CT_USER02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser03 = "CT_USER03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser04 = "CT_USER04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser05 = "CT_USER05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser06 = "CT_USER06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser07 = "CT_USER07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser08 = "CT_USER08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser09 = "CT_USER09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CtUser10 = "CT_USER10";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateDate = "CREATE_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CreateBy = "CREATE_BY";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModDate = "MOD_DATE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModBy = "MOD_BY";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionProgram = "SELECTION_PROGRAM";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AltEqpNo = "ALT_EQP_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr = "MFR";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccuracyClass = "ACCURACY_CLASS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectedRatio = "SELECTED_RATIO";
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
	public string TransformerCode = "TRANSFORMER_CODE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BurdenValue = "burden_value";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CaseStyle = "case_style";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MountingType = "mounting_type";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Environment = "environment";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string InsulationType = "insulation_type";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VoltageClass = "voltage_class";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BasicImpulseLevel = "basic_impulse_level";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatingFactor30c = "rating_factor_30c";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AccuracyClass2 = "accuracy_class2";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BurdenValue2 = "burden_value2";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RatingFactor55c = "rating_factor_55c";
	
	public string RealTableName
	{
		get { return "TEQP_CT".ToLowerInvariant(); }		
	}

	public Dictionary<string, ColumnInformation> ColumnsLookup { get { return _columnsLookup; } }

	public Dictionary<string, ColumnInformation> _columnsLookup = new Dictionary<string, ColumnInformation>() 
	{
				{"Owner", new ColumnInformation() { DataType = "int", ModelName = "Owner", ColumnName = "OWNER"}},
				{"EqpNo", new ColumnInformation() { DataType = "string", ModelName = "EqpNo", ColumnName = "EQP_NO"}},
				{"Site", new ColumnInformation() { DataType = "int", ModelName = "Site", ColumnName = "SITE"}},
				{"Circuit", new ColumnInformation() { DataType = "int", ModelName = "Circuit", ColumnName = "CIRCUIT"}},
				{"CatalogNo", new ColumnInformation() { DataType = "string", ModelName = "CatalogNo", ColumnName = "CATALOG_NO"}},
				{"PurchaseDate", new ColumnInformation() { DataType = "DateTime", ModelName = "PurchaseDate", ColumnName = "PURCHASE_DATE"}},
				{"WarrantyPeriod", new ColumnInformation() { DataType = "int", ModelName = "WarrantyPeriod", ColumnName = "WARRANTY_PERIOD"}},
				{"PoRef", new ColumnInformation() { DataType = "string", ModelName = "PoRef", ColumnName = "PO_REF"}},
				{"RetireCode", new ColumnInformation() { DataType = "string", ModelName = "RetireCode", ColumnName = "RETIRE_CODE"}},
				{"RetireDate", new ColumnInformation() { DataType = "DateTime", ModelName = "RetireDate", ColumnName = "RETIRE_DATE"}},
				{"RetireBy", new ColumnInformation() { DataType = "string", ModelName = "RetireBy", ColumnName = "RETIRE_BY"}},
				{"RetireLocation", new ColumnInformation() { DataType = "string", ModelName = "RetireLocation", ColumnName = "RETIRE_LOCATION"}},
				{"EqpStatus", new ColumnInformation() { DataType = "string", ModelName = "EqpStatus", ColumnName = "EQP_STATUS"}},
				{"StatusDate", new ColumnInformation() { DataType = "DateTime", ModelName = "StatusDate", ColumnName = "STATUS_DATE"}},
				{"ShopStatus", new ColumnInformation() { DataType = "string", ModelName = "ShopStatus", ColumnName = "SHOP_STATUS"}},
				{"ShopCycle", new ColumnInformation() { DataType = "int", ModelName = "ShopCycle", ColumnName = "SHOP_CYCLE"}},
				{"Location", new ColumnInformation() { DataType = "string", ModelName = "Location", ColumnName = "LOCATION"}},
				{"Shelf", new ColumnInformation() { DataType = "string", ModelName = "Shelf", ColumnName = "SHELF"}},
				{"BoxNo", new ColumnInformation() { DataType = "string", ModelName = "BoxNo", ColumnName = "BOX_NO"}},
				{"PalletNo", new ColumnInformation() { DataType = "string", ModelName = "PalletNo", ColumnName = "PALLET_NO"}},
				{"NewBatchNo", new ColumnInformation() { DataType = "string", ModelName = "NewBatchNo", ColumnName = "NEW_BATCH_NO"}},
				{"StrataName", new ColumnInformation() { DataType = "string", ModelName = "StrataName", ColumnName = "STRATA_NAME"}},
				{"TestProgram", new ColumnInformation() { DataType = "string", ModelName = "TestProgram", ColumnName = "TEST_PROGRAM"}},
				{"NextTestDue", new ColumnInformation() { DataType = "string", ModelName = "NextTestDue", ColumnName = "NEXT_TEST_DUE"}},
				{"TestPeriod", new ColumnInformation() { DataType = "string", ModelName = "TestPeriod", ColumnName = "TEST_PERIOD"}},
				{"SerialNo", new ColumnInformation() { DataType = "string", ModelName = "SerialNo", ColumnName = "SERIAL_NO"}},
				{"ModelNo", new ColumnInformation() { DataType = "string", ModelName = "ModelNo", ColumnName = "MODEL_NO"}},
				{"NameplateRatio", new ColumnInformation() { DataType = "string", ModelName = "NameplateRatio", ColumnName = "NAMEPLATE_RATIO"}},
				{"SelectedMult", new ColumnInformation() { DataType = "decimal", ModelName = "SelectedMult", ColumnName = "SELECTED_MULT"}},
				{"AuxMult", new ColumnInformation() { DataType = "int", ModelName = "AuxMult", ColumnName = "AUX_MULT"}},
				{"CtUser01", new ColumnInformation() { DataType = "string", ModelName = "CtUser01", ColumnName = "CT_USER01"}},
				{"CtUser02", new ColumnInformation() { DataType = "string", ModelName = "CtUser02", ColumnName = "CT_USER02"}},
				{"CtUser03", new ColumnInformation() { DataType = "string", ModelName = "CtUser03", ColumnName = "CT_USER03"}},
				{"CtUser04", new ColumnInformation() { DataType = "string", ModelName = "CtUser04", ColumnName = "CT_USER04"}},
				{"CtUser05", new ColumnInformation() { DataType = "string", ModelName = "CtUser05", ColumnName = "CT_USER05"}},
				{"CtUser06", new ColumnInformation() { DataType = "string", ModelName = "CtUser06", ColumnName = "CT_USER06"}},
				{"CtUser07", new ColumnInformation() { DataType = "string", ModelName = "CtUser07", ColumnName = "CT_USER07"}},
				{"CtUser08", new ColumnInformation() { DataType = "string", ModelName = "CtUser08", ColumnName = "CT_USER08"}},
				{"CtUser09", new ColumnInformation() { DataType = "string", ModelName = "CtUser09", ColumnName = "CT_USER09"}},
				{"CtUser10", new ColumnInformation() { DataType = "string", ModelName = "CtUser10", ColumnName = "CT_USER10"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"SelectionProgram", new ColumnInformation() { DataType = "string", ModelName = "SelectionProgram", ColumnName = "SELECTION_PROGRAM"}},
				{"AltEqpNo", new ColumnInformation() { DataType = "string", ModelName = "AltEqpNo", ColumnName = "ALT_EQP_NO"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "MFR"}},
				{"AccuracyClass", new ColumnInformation() { DataType = "decimal", ModelName = "AccuracyClass", ColumnName = "ACCURACY_CLASS"}},
				{"SelectedRatio", new ColumnInformation() { DataType = "string", ModelName = "SelectedRatio", ColumnName = "SELECTED_RATIO"}},
				{"MfrBuildDate", new ColumnInformation() { DataType = "DateTime", ModelName = "MfrBuildDate", ColumnName = "MFR_BUILD_DATE"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"TransformerCode", new ColumnInformation() { DataType = "string", ModelName = "TransformerCode", ColumnName = "TRANSFORMER_CODE"}},
				{"BurdenValue", new ColumnInformation() { DataType = "double", ModelName = "BurdenValue", ColumnName = "burden_value"}},
				{"CaseStyle", new ColumnInformation() { DataType = "string", ModelName = "CaseStyle", ColumnName = "case_style"}},
				{"MountingType", new ColumnInformation() { DataType = "string", ModelName = "MountingType", ColumnName = "mounting_type"}},
				{"Environment", new ColumnInformation() { DataType = "string", ModelName = "Environment", ColumnName = "environment"}},
				{"InsulationType", new ColumnInformation() { DataType = "string", ModelName = "InsulationType", ColumnName = "insulation_type"}},
				{"VoltageClass", new ColumnInformation() { DataType = "decimal", ModelName = "VoltageClass", ColumnName = "voltage_class"}},
				{"BasicImpulseLevel", new ColumnInformation() { DataType = "decimal", ModelName = "BasicImpulseLevel", ColumnName = "basic_impulse_level"}},
				{"RatingFactor30c", new ColumnInformation() { DataType = "decimal", ModelName = "RatingFactor30c", ColumnName = "rating_factor_30c"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
				{"AccuracyClass2", new ColumnInformation() { DataType = "decimal", ModelName = "AccuracyClass2", ColumnName = "accuracy_class2"}},
				{"BurdenValue2", new ColumnInformation() { DataType = "double", ModelName = "BurdenValue2", ColumnName = "burden_value2"}},
				{"RatingFactor55c", new ColumnInformation() { DataType = "decimal", ModelName = "RatingFactor55c", ColumnName = "rating_factor_55c"}},
			};

	public override string ToString() 
	{
		return "wndba.teqp_ct";
	}
}
