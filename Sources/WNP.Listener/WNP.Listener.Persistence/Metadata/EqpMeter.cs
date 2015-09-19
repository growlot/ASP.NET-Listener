using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.Metadata {
public class EqpMeterImpl: ITableInformation {
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
	public string AmiId1 = "AMI_ID1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AmiId2 = "AMI_ID2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AmiId3 = "AMI_ID3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr = "MFR";
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
	public string MeterCode = "METER_CODE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AuxCode = "AUX_CODE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Form = "FORM";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Base = "BASE";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVolts = "TEST_VOLTS";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestAmps = "TEST_AMPS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Kh = "KH";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RegisterRatio = "REGISTER_RATIO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase = "PHASE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Wire = "WIRE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AepCode = "aep_code";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string KwhDials = "KWH_DIALS";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string KwDials = "KW_DIALS";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdValDiv = "DMD_VAL_DIV";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdFullScale = "DMD_FULL_SCALE";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Interval = "INTERVAL";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev01 = "FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev02 = "FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev04 = "FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev03 = "FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser01 = "METER_USER01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser02 = "METER_USER02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser03 = "METER_USER03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser04 = "METER_USER04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser05 = "METER_USER05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser06 = "METER_USER06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser07 = "METER_USER07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser08 = "METER_USER08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser09 = "METER_USER09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser10 = "METER_USER10";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser11 = "METER_USER11";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser12 = "METER_USER12";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser13 = "METER_USER13";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser14 = "METER_USER14";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser15 = "METER_USER15";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser16 = "METER_USER16";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser17 = "METER_USER17";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser18 = "METER_USER18";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser19 = "METER_USER19";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser20 = "METER_USER20";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser21 = "METER_USER21";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser22 = "METER_USER22";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser23 = "METER_USER23";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser24 = "METER_USER24";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser25 = "METER_USER25";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser26 = "METER_USER26";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser27 = "METER_USER27";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser28 = "METER_USER28";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser29 = "METER_USER29";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser30 = "METER_USER30";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser31 = "METER_USER31";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser32 = "METER_USER32";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser33 = "METER_USER33";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser34 = "METER_USER34";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser35 = "METER_USER35";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser36 = "METER_USER36";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser37 = "METER_USER37";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser38 = "METER_USER38";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser39 = "METER_USER39";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser40 = "METER_USER40";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser41 = "METER_USER41";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser42 = "METER_USER42";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser43 = "METER_USER43";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser44 = "METER_USER44";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser45 = "METER_USER45";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser46 = "METER_USER46";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser47 = "METER_USER47";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser48 = "METER_USER48";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser49 = "METER_USER49";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser50 = "METER_USER50";
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
	public string ReadSet = "READ_SET";
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
	public string BillingFlag = "BILLING_FLAG";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMult = "ENERGY_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DemandMult = "DEMAND_MULT";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Scalar = "SCALAR";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrimaryKh = "PRIMARY_KH";
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
	public string ProgramId = "program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev05 = "FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev06 = "FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev07 = "FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev08 = "FIRMWARE_REV08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrefTestSequence = "pref_test_sequence";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string Id = "ID";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterClass = "METER_CLASS";
	
	public string RealTableName
	{
		get { return "TEQP_METER".ToLowerInvariant(); }		
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
				{"AmiId1", new ColumnInformation() { DataType = "string", ModelName = "AmiId1", ColumnName = "AMI_ID1"}},
				{"AmiId2", new ColumnInformation() { DataType = "string", ModelName = "AmiId2", ColumnName = "AMI_ID2"}},
				{"AmiId3", new ColumnInformation() { DataType = "string", ModelName = "AmiId3", ColumnName = "AMI_ID3"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "MFR"}},
				{"ModelNo", new ColumnInformation() { DataType = "string", ModelName = "ModelNo", ColumnName = "MODEL_NO"}},
				{"MeterCode", new ColumnInformation() { DataType = "string", ModelName = "MeterCode", ColumnName = "METER_CODE"}},
				{"AuxCode", new ColumnInformation() { DataType = "string", ModelName = "AuxCode", ColumnName = "AUX_CODE"}},
				{"Form", new ColumnInformation() { DataType = "string", ModelName = "Form", ColumnName = "FORM"}},
				{"Base", new ColumnInformation() { DataType = "string", ModelName = "Base", ColumnName = "BASE"}},
				{"TestVolts", new ColumnInformation() { DataType = "double", ModelName = "TestVolts", ColumnName = "TEST_VOLTS"}},
				{"TestAmps", new ColumnInformation() { DataType = "double", ModelName = "TestAmps", ColumnName = "TEST_AMPS"}},
				{"Kh", new ColumnInformation() { DataType = "string", ModelName = "Kh", ColumnName = "KH"}},
				{"RegisterRatio", new ColumnInformation() { DataType = "string", ModelName = "RegisterRatio", ColumnName = "REGISTER_RATIO"}},
				{"Phase", new ColumnInformation() { DataType = "string", ModelName = "Phase", ColumnName = "PHASE"}},
				{"Wire", new ColumnInformation() { DataType = "string", ModelName = "Wire", ColumnName = "WIRE"}},
				{"AepCode", new ColumnInformation() { DataType = "string", ModelName = "AepCode", ColumnName = "aep_code"}},
				{"KwhDials", new ColumnInformation() { DataType = "int", ModelName = "KwhDials", ColumnName = "KWH_DIALS"}},
				{"KwDials", new ColumnInformation() { DataType = "int", ModelName = "KwDials", ColumnName = "KW_DIALS"}},
				{"DmdValDiv", new ColumnInformation() { DataType = "double", ModelName = "DmdValDiv", ColumnName = "DMD_VAL_DIV"}},
				{"DmdFullScale", new ColumnInformation() { DataType = "double", ModelName = "DmdFullScale", ColumnName = "DMD_FULL_SCALE"}},
				{"Interval", new ColumnInformation() { DataType = "string", ModelName = "Interval", ColumnName = "INTERVAL"}},
				{"FirmwareRev01", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev01", ColumnName = "FIRMWARE_REV01"}},
				{"FirmwareRev02", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev02", ColumnName = "FIRMWARE_REV02"}},
				{"FirmwareRev04", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev04", ColumnName = "FIRMWARE_REV04"}},
				{"FirmwareRev03", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev03", ColumnName = "FIRMWARE_REV03"}},
				{"MeterUser01", new ColumnInformation() { DataType = "string", ModelName = "MeterUser01", ColumnName = "METER_USER01"}},
				{"MeterUser02", new ColumnInformation() { DataType = "string", ModelName = "MeterUser02", ColumnName = "METER_USER02"}},
				{"MeterUser03", new ColumnInformation() { DataType = "string", ModelName = "MeterUser03", ColumnName = "METER_USER03"}},
				{"MeterUser04", new ColumnInformation() { DataType = "string", ModelName = "MeterUser04", ColumnName = "METER_USER04"}},
				{"MeterUser05", new ColumnInformation() { DataType = "string", ModelName = "MeterUser05", ColumnName = "METER_USER05"}},
				{"MeterUser06", new ColumnInformation() { DataType = "string", ModelName = "MeterUser06", ColumnName = "METER_USER06"}},
				{"MeterUser07", new ColumnInformation() { DataType = "string", ModelName = "MeterUser07", ColumnName = "METER_USER07"}},
				{"MeterUser08", new ColumnInformation() { DataType = "string", ModelName = "MeterUser08", ColumnName = "METER_USER08"}},
				{"MeterUser09", new ColumnInformation() { DataType = "string", ModelName = "MeterUser09", ColumnName = "METER_USER09"}},
				{"MeterUser10", new ColumnInformation() { DataType = "string", ModelName = "MeterUser10", ColumnName = "METER_USER10"}},
				{"MeterUser11", new ColumnInformation() { DataType = "string", ModelName = "MeterUser11", ColumnName = "METER_USER11"}},
				{"MeterUser12", new ColumnInformation() { DataType = "string", ModelName = "MeterUser12", ColumnName = "METER_USER12"}},
				{"MeterUser13", new ColumnInformation() { DataType = "string", ModelName = "MeterUser13", ColumnName = "METER_USER13"}},
				{"MeterUser14", new ColumnInformation() { DataType = "string", ModelName = "MeterUser14", ColumnName = "METER_USER14"}},
				{"MeterUser15", new ColumnInformation() { DataType = "string", ModelName = "MeterUser15", ColumnName = "METER_USER15"}},
				{"MeterUser16", new ColumnInformation() { DataType = "string", ModelName = "MeterUser16", ColumnName = "METER_USER16"}},
				{"MeterUser17", new ColumnInformation() { DataType = "string", ModelName = "MeterUser17", ColumnName = "METER_USER17"}},
				{"MeterUser18", new ColumnInformation() { DataType = "string", ModelName = "MeterUser18", ColumnName = "METER_USER18"}},
				{"MeterUser19", new ColumnInformation() { DataType = "string", ModelName = "MeterUser19", ColumnName = "METER_USER19"}},
				{"MeterUser20", new ColumnInformation() { DataType = "string", ModelName = "MeterUser20", ColumnName = "METER_USER20"}},
				{"MeterUser21", new ColumnInformation() { DataType = "string", ModelName = "MeterUser21", ColumnName = "METER_USER21"}},
				{"MeterUser22", new ColumnInformation() { DataType = "string", ModelName = "MeterUser22", ColumnName = "METER_USER22"}},
				{"MeterUser23", new ColumnInformation() { DataType = "string", ModelName = "MeterUser23", ColumnName = "METER_USER23"}},
				{"MeterUser24", new ColumnInformation() { DataType = "string", ModelName = "MeterUser24", ColumnName = "METER_USER24"}},
				{"MeterUser25", new ColumnInformation() { DataType = "string", ModelName = "MeterUser25", ColumnName = "METER_USER25"}},
				{"MeterUser26", new ColumnInformation() { DataType = "string", ModelName = "MeterUser26", ColumnName = "METER_USER26"}},
				{"MeterUser27", new ColumnInformation() { DataType = "string", ModelName = "MeterUser27", ColumnName = "METER_USER27"}},
				{"MeterUser28", new ColumnInformation() { DataType = "string", ModelName = "MeterUser28", ColumnName = "METER_USER28"}},
				{"MeterUser29", new ColumnInformation() { DataType = "string", ModelName = "MeterUser29", ColumnName = "METER_USER29"}},
				{"MeterUser30", new ColumnInformation() { DataType = "string", ModelName = "MeterUser30", ColumnName = "METER_USER30"}},
				{"MeterUser31", new ColumnInformation() { DataType = "string", ModelName = "MeterUser31", ColumnName = "METER_USER31"}},
				{"MeterUser32", new ColumnInformation() { DataType = "string", ModelName = "MeterUser32", ColumnName = "METER_USER32"}},
				{"MeterUser33", new ColumnInformation() { DataType = "string", ModelName = "MeterUser33", ColumnName = "METER_USER33"}},
				{"MeterUser34", new ColumnInformation() { DataType = "string", ModelName = "MeterUser34", ColumnName = "METER_USER34"}},
				{"MeterUser35", new ColumnInformation() { DataType = "string", ModelName = "MeterUser35", ColumnName = "METER_USER35"}},
				{"MeterUser36", new ColumnInformation() { DataType = "string", ModelName = "MeterUser36", ColumnName = "METER_USER36"}},
				{"MeterUser37", new ColumnInformation() { DataType = "string", ModelName = "MeterUser37", ColumnName = "METER_USER37"}},
				{"MeterUser38", new ColumnInformation() { DataType = "string", ModelName = "MeterUser38", ColumnName = "METER_USER38"}},
				{"MeterUser39", new ColumnInformation() { DataType = "string", ModelName = "MeterUser39", ColumnName = "METER_USER39"}},
				{"MeterUser40", new ColumnInformation() { DataType = "string", ModelName = "MeterUser40", ColumnName = "METER_USER40"}},
				{"MeterUser41", new ColumnInformation() { DataType = "string", ModelName = "MeterUser41", ColumnName = "METER_USER41"}},
				{"MeterUser42", new ColumnInformation() { DataType = "string", ModelName = "MeterUser42", ColumnName = "METER_USER42"}},
				{"MeterUser43", new ColumnInformation() { DataType = "string", ModelName = "MeterUser43", ColumnName = "METER_USER43"}},
				{"MeterUser44", new ColumnInformation() { DataType = "string", ModelName = "MeterUser44", ColumnName = "METER_USER44"}},
				{"MeterUser45", new ColumnInformation() { DataType = "string", ModelName = "MeterUser45", ColumnName = "METER_USER45"}},
				{"MeterUser46", new ColumnInformation() { DataType = "string", ModelName = "MeterUser46", ColumnName = "METER_USER46"}},
				{"MeterUser47", new ColumnInformation() { DataType = "string", ModelName = "MeterUser47", ColumnName = "METER_USER47"}},
				{"MeterUser48", new ColumnInformation() { DataType = "string", ModelName = "MeterUser48", ColumnName = "METER_USER48"}},
				{"MeterUser49", new ColumnInformation() { DataType = "string", ModelName = "MeterUser49", ColumnName = "METER_USER49"}},
				{"MeterUser50", new ColumnInformation() { DataType = "string", ModelName = "MeterUser50", ColumnName = "METER_USER50"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "CREATE_DATE"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "CREATE_BY"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "MOD_DATE"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "MOD_BY"}},
				{"ReadSet", new ColumnInformation() { DataType = "string", ModelName = "ReadSet", ColumnName = "READ_SET"}},
				{"SelectionProgram", new ColumnInformation() { DataType = "string", ModelName = "SelectionProgram", ColumnName = "SELECTION_PROGRAM"}},
				{"AltEqpNo", new ColumnInformation() { DataType = "string", ModelName = "AltEqpNo", ColumnName = "ALT_EQP_NO"}},
				{"BillingFlag", new ColumnInformation() { DataType = "string", ModelName = "BillingFlag", ColumnName = "BILLING_FLAG"}},
				{"EnergyMult", new ColumnInformation() { DataType = "decimal", ModelName = "EnergyMult", ColumnName = "ENERGY_MULT"}},
				{"DemandMult", new ColumnInformation() { DataType = "decimal", ModelName = "DemandMult", ColumnName = "DEMAND_MULT"}},
				{"Scalar", new ColumnInformation() { DataType = "decimal", ModelName = "Scalar", ColumnName = "SCALAR"}},
				{"PrimaryKh", new ColumnInformation() { DataType = "decimal", ModelName = "PrimaryKh", ColumnName = "PRIMARY_KH"}},
				{"MfrBuildDate", new ColumnInformation() { DataType = "DateTime", ModelName = "MfrBuildDate", ColumnName = "MFR_BUILD_DATE"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"ProgramId", new ColumnInformation() { DataType = "string", ModelName = "ProgramId", ColumnName = "program_id"}},
				{"FirmwareRev05", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev05", ColumnName = "FIRMWARE_REV05"}},
				{"FirmwareRev06", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev06", ColumnName = "FIRMWARE_REV06"}},
				{"FirmwareRev07", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev07", ColumnName = "FIRMWARE_REV07"}},
				{"FirmwareRev08", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev08", ColumnName = "FIRMWARE_REV08"}},
				{"PrefTestSequence", new ColumnInformation() { DataType = "string", ModelName = "PrefTestSequence", ColumnName = "pref_test_sequence"}},
				{"Id", new ColumnInformation() { DataType = "int", ModelName = "Id", ColumnName = "ID"}},
				{"MeterClass", new ColumnInformation() { DataType = "int", ModelName = "MeterClass", ColumnName = "METER_CLASS"}},
			};

	public override string ToString() 
	{
		return "wndba.teqp_meter";
	}
}
}
