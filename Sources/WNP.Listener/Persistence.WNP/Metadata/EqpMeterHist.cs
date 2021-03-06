// <auto-generated>
#pragma warning disable 1591
using System.Collections.Generic;
namespace AMSLLC.Listener.Persistence.WNP.Metadata {
public class EqpMeterHistTable: ITableInformation {
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
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: False
	/// <para />Is Primary Key: False
	/// </summary>
	public string SiteDate { get; } = "site_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Site { get; } = "site";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Circuit { get; } = "circuit";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string CatalogNo { get; } = "catalog_no";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PurchaseDate { get; } = "purchase_date";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string WarrantyPeriod { get; } = "warranty_period";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PoRef { get; } = "po_ref";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireCode { get; } = "retire_code";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireDate { get; } = "retire_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireBy { get; } = "retire_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RetireLocation { get; } = "retire_location";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EqpStatus { get; } = "eqp_status";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StatusDate { get; } = "status_date";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopStatus { get; } = "shop_status";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ShopCycle { get; } = "shop_cycle";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Location { get; } = "location";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Shelf { get; } = "shelf";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BoxNo { get; } = "box_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PalletNo { get; } = "pallet_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NewBatchNo { get; } = "NEW_BATCH_NO";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string StrataName { get; } = "strata_name";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SelectionProgram { get; } = "selection_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestProgram { get; } = "test_program";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string NextTestDue { get; } = "next_test_due";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestPeriod { get; } = "test_period";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string SerialNo { get; } = "serial_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AmiId1 { get; } = "ami_id1";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AmiId2 { get; } = "ami_id2";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AmiId3 { get; } = "ami_id3";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Mfr { get; } = "mfr";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ModelNo { get; } = "model_no";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterCode { get; } = "meter_code";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AuxCode { get; } = "aux_code";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Form { get; } = "form";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Base { get; } = "base";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestVolts { get; } = "test_volts";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TestAmps { get; } = "test_amps";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Kh { get; } = "kh";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string RegisterRatio { get; } = "register_ratio";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Phase { get; } = "phase";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Wire { get; } = "wire";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AepCode { get; } = "aep_code";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string KwhDials { get; } = "kwh_dials";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string KwDials { get; } = "kw_dials";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReadSet { get; } = "read_set";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdValDiv { get; } = "dmd_val_div";
		/// <summary>
	/// <para />Database Type: double
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DmdFullScale { get; } = "dmd_full_scale";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Interval { get; } = "interval";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string BillingFlag { get; } = "billing_flag";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string EnergyMult { get; } = "energy_mult";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string DemandMult { get; } = "demand_mult";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrimaryKh { get; } = "primary_kh";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev01 { get; } = "FIRMWARE_REV01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev02 { get; } = "FIRMWARE_REV02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev04 { get; } = "FIRMWARE_REV04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev03 { get; } = "FIRMWARE_REV03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser01 { get; } = "meter_user01";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser02 { get; } = "meter_user02";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser03 { get; } = "meter_user03";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser04 { get; } = "meter_user04";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser05 { get; } = "meter_user05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser06 { get; } = "meter_user06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser07 { get; } = "meter_user07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser08 { get; } = "meter_user08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser09 { get; } = "meter_user09";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser10 { get; } = "meter_user10";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser11 { get; } = "meter_user11";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser12 { get; } = "meter_user12";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser13 { get; } = "meter_user13";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser14 { get; } = "meter_user14";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser15 { get; } = "meter_user15";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser16 { get; } = "meter_user16";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser17 { get; } = "meter_user17";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser18 { get; } = "meter_user18";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser19 { get; } = "meter_user19";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser20 { get; } = "meter_user20";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser21 { get; } = "meter_user21";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser22 { get; } = "meter_user22";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser23 { get; } = "meter_user23";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser24 { get; } = "meter_user24";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser25 { get; } = "meter_user25";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser26 { get; } = "meter_user26";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser27 { get; } = "meter_user27";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser28 { get; } = "meter_user28";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser29 { get; } = "meter_user29";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser30 { get; } = "meter_user30";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser31 { get; } = "meter_user31";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser32 { get; } = "meter_user32";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser33 { get; } = "meter_user33";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser34 { get; } = "meter_user34";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser35 { get; } = "meter_user35";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser36 { get; } = "meter_user36";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser37 { get; } = "meter_user37";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser38 { get; } = "meter_user38";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser39 { get; } = "meter_user39";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser40 { get; } = "meter_user40";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser41 { get; } = "meter_user41";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser42 { get; } = "meter_user42";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser43 { get; } = "meter_user43";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser44 { get; } = "meter_user44";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser45 { get; } = "meter_user45";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser46 { get; } = "meter_user46";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser47 { get; } = "meter_user47";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser48 { get; } = "meter_user48";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser49 { get; } = "meter_user49";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterUser50 { get; } = "meter_user50";
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
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string AltEqpNo { get; } = "ALT_EQP_NO";
		/// <summary>
	/// <para />Database Type: decimal
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string Scalar { get; } = "SCALAR";
		/// <summary>
	/// <para />Database Type: DateTime
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MfrBuildDate { get; } = "MFR_BUILD_DATE";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string TransactionId { get; } = "transaction_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ProgramId { get; } = "program_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev05 { get; } = "FIRMWARE_REV05";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev06 { get; } = "FIRMWARE_REV06";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev07 { get; } = "FIRMWARE_REV07";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string FirmwareRev08 { get; } = "FIRMWARE_REV08";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string PrefTestSequence { get; } = "pref_test_sequence";
		/// <summary>
	/// <para />Database Type: int
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string MeterClass { get; } = "METER_CLASS";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string VehicleId { get; } = "vehicle_id";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string ReceivedBy { get; } = "received_by";
		/// <summary>
	/// <para />Database Type: string
	/// <para />Is Nullable: True
	/// <para />Is Primary Key: False
	/// </summary>
	public string IsFirstArticle { get; } = "is_first_article";
	
	public string RealTableName
	{
		get { return "TEQP_METER_HIST".ToUpperInvariant(); }		
	}

	public string FullTableName
	{
		get { return ToString(); }		
	}


	private Dictionary<string, ColumnInformation> columnsLookup = new Dictionary<string, ColumnInformation>() 
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
				{"AmiId1", new ColumnInformation() { DataType = "string", ModelName = "AmiId1", ColumnName = "ami_id1"}},
				{"AmiId2", new ColumnInformation() { DataType = "string", ModelName = "AmiId2", ColumnName = "ami_id2"}},
				{"AmiId3", new ColumnInformation() { DataType = "string", ModelName = "AmiId3", ColumnName = "ami_id3"}},
				{"Mfr", new ColumnInformation() { DataType = "string", ModelName = "Mfr", ColumnName = "mfr"}},
				{"ModelNo", new ColumnInformation() { DataType = "string", ModelName = "ModelNo", ColumnName = "model_no"}},
				{"MeterCode", new ColumnInformation() { DataType = "string", ModelName = "MeterCode", ColumnName = "meter_code"}},
				{"AuxCode", new ColumnInformation() { DataType = "string", ModelName = "AuxCode", ColumnName = "aux_code"}},
				{"Form", new ColumnInformation() { DataType = "string", ModelName = "Form", ColumnName = "form"}},
				{"Base", new ColumnInformation() { DataType = "string", ModelName = "Base", ColumnName = "base"}},
				{"TestVolts", new ColumnInformation() { DataType = "double", ModelName = "TestVolts", ColumnName = "test_volts"}},
				{"TestAmps", new ColumnInformation() { DataType = "double", ModelName = "TestAmps", ColumnName = "test_amps"}},
				{"Kh", new ColumnInformation() { DataType = "string", ModelName = "Kh", ColumnName = "kh"}},
				{"RegisterRatio", new ColumnInformation() { DataType = "string", ModelName = "RegisterRatio", ColumnName = "register_ratio"}},
				{"Phase", new ColumnInformation() { DataType = "string", ModelName = "Phase", ColumnName = "phase"}},
				{"Wire", new ColumnInformation() { DataType = "string", ModelName = "Wire", ColumnName = "wire"}},
				{"AepCode", new ColumnInformation() { DataType = "string", ModelName = "AepCode", ColumnName = "aep_code"}},
				{"KwhDials", new ColumnInformation() { DataType = "int", ModelName = "KwhDials", ColumnName = "kwh_dials"}},
				{"KwDials", new ColumnInformation() { DataType = "int", ModelName = "KwDials", ColumnName = "kw_dials"}},
				{"ReadSet", new ColumnInformation() { DataType = "string", ModelName = "ReadSet", ColumnName = "read_set"}},
				{"DmdValDiv", new ColumnInformation() { DataType = "double", ModelName = "DmdValDiv", ColumnName = "dmd_val_div"}},
				{"DmdFullScale", new ColumnInformation() { DataType = "double", ModelName = "DmdFullScale", ColumnName = "dmd_full_scale"}},
				{"Interval", new ColumnInformation() { DataType = "string", ModelName = "Interval", ColumnName = "interval"}},
				{"BillingFlag", new ColumnInformation() { DataType = "string", ModelName = "BillingFlag", ColumnName = "billing_flag"}},
				{"EnergyMult", new ColumnInformation() { DataType = "decimal", ModelName = "EnergyMult", ColumnName = "energy_mult"}},
				{"DemandMult", new ColumnInformation() { DataType = "decimal", ModelName = "DemandMult", ColumnName = "demand_mult"}},
				{"PrimaryKh", new ColumnInformation() { DataType = "decimal", ModelName = "PrimaryKh", ColumnName = "primary_kh"}},
				{"FirmwareRev01", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev01", ColumnName = "FIRMWARE_REV01"}},
				{"FirmwareRev02", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev02", ColumnName = "FIRMWARE_REV02"}},
				{"FirmwareRev04", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev04", ColumnName = "FIRMWARE_REV04"}},
				{"FirmwareRev03", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev03", ColumnName = "FIRMWARE_REV03"}},
				{"MeterUser01", new ColumnInformation() { DataType = "string", ModelName = "MeterUser01", ColumnName = "meter_user01"}},
				{"MeterUser02", new ColumnInformation() { DataType = "string", ModelName = "MeterUser02", ColumnName = "meter_user02"}},
				{"MeterUser03", new ColumnInformation() { DataType = "string", ModelName = "MeterUser03", ColumnName = "meter_user03"}},
				{"MeterUser04", new ColumnInformation() { DataType = "string", ModelName = "MeterUser04", ColumnName = "meter_user04"}},
				{"MeterUser05", new ColumnInformation() { DataType = "string", ModelName = "MeterUser05", ColumnName = "meter_user05"}},
				{"MeterUser06", new ColumnInformation() { DataType = "string", ModelName = "MeterUser06", ColumnName = "meter_user06"}},
				{"MeterUser07", new ColumnInformation() { DataType = "string", ModelName = "MeterUser07", ColumnName = "meter_user07"}},
				{"MeterUser08", new ColumnInformation() { DataType = "string", ModelName = "MeterUser08", ColumnName = "meter_user08"}},
				{"MeterUser09", new ColumnInformation() { DataType = "string", ModelName = "MeterUser09", ColumnName = "meter_user09"}},
				{"MeterUser10", new ColumnInformation() { DataType = "string", ModelName = "MeterUser10", ColumnName = "meter_user10"}},
				{"MeterUser11", new ColumnInformation() { DataType = "string", ModelName = "MeterUser11", ColumnName = "meter_user11"}},
				{"MeterUser12", new ColumnInformation() { DataType = "string", ModelName = "MeterUser12", ColumnName = "meter_user12"}},
				{"MeterUser13", new ColumnInformation() { DataType = "string", ModelName = "MeterUser13", ColumnName = "meter_user13"}},
				{"MeterUser14", new ColumnInformation() { DataType = "string", ModelName = "MeterUser14", ColumnName = "meter_user14"}},
				{"MeterUser15", new ColumnInformation() { DataType = "string", ModelName = "MeterUser15", ColumnName = "meter_user15"}},
				{"MeterUser16", new ColumnInformation() { DataType = "string", ModelName = "MeterUser16", ColumnName = "meter_user16"}},
				{"MeterUser17", new ColumnInformation() { DataType = "string", ModelName = "MeterUser17", ColumnName = "meter_user17"}},
				{"MeterUser18", new ColumnInformation() { DataType = "string", ModelName = "MeterUser18", ColumnName = "meter_user18"}},
				{"MeterUser19", new ColumnInformation() { DataType = "string", ModelName = "MeterUser19", ColumnName = "meter_user19"}},
				{"MeterUser20", new ColumnInformation() { DataType = "string", ModelName = "MeterUser20", ColumnName = "meter_user20"}},
				{"MeterUser21", new ColumnInformation() { DataType = "string", ModelName = "MeterUser21", ColumnName = "meter_user21"}},
				{"MeterUser22", new ColumnInformation() { DataType = "string", ModelName = "MeterUser22", ColumnName = "meter_user22"}},
				{"MeterUser23", new ColumnInformation() { DataType = "string", ModelName = "MeterUser23", ColumnName = "meter_user23"}},
				{"MeterUser24", new ColumnInformation() { DataType = "string", ModelName = "MeterUser24", ColumnName = "meter_user24"}},
				{"MeterUser25", new ColumnInformation() { DataType = "string", ModelName = "MeterUser25", ColumnName = "meter_user25"}},
				{"MeterUser26", new ColumnInformation() { DataType = "string", ModelName = "MeterUser26", ColumnName = "meter_user26"}},
				{"MeterUser27", new ColumnInformation() { DataType = "string", ModelName = "MeterUser27", ColumnName = "meter_user27"}},
				{"MeterUser28", new ColumnInformation() { DataType = "string", ModelName = "MeterUser28", ColumnName = "meter_user28"}},
				{"MeterUser29", new ColumnInformation() { DataType = "string", ModelName = "MeterUser29", ColumnName = "meter_user29"}},
				{"MeterUser30", new ColumnInformation() { DataType = "string", ModelName = "MeterUser30", ColumnName = "meter_user30"}},
				{"MeterUser31", new ColumnInformation() { DataType = "string", ModelName = "MeterUser31", ColumnName = "meter_user31"}},
				{"MeterUser32", new ColumnInformation() { DataType = "string", ModelName = "MeterUser32", ColumnName = "meter_user32"}},
				{"MeterUser33", new ColumnInformation() { DataType = "string", ModelName = "MeterUser33", ColumnName = "meter_user33"}},
				{"MeterUser34", new ColumnInformation() { DataType = "string", ModelName = "MeterUser34", ColumnName = "meter_user34"}},
				{"MeterUser35", new ColumnInformation() { DataType = "string", ModelName = "MeterUser35", ColumnName = "meter_user35"}},
				{"MeterUser36", new ColumnInformation() { DataType = "string", ModelName = "MeterUser36", ColumnName = "meter_user36"}},
				{"MeterUser37", new ColumnInformation() { DataType = "string", ModelName = "MeterUser37", ColumnName = "meter_user37"}},
				{"MeterUser38", new ColumnInformation() { DataType = "string", ModelName = "MeterUser38", ColumnName = "meter_user38"}},
				{"MeterUser39", new ColumnInformation() { DataType = "string", ModelName = "MeterUser39", ColumnName = "meter_user39"}},
				{"MeterUser40", new ColumnInformation() { DataType = "string", ModelName = "MeterUser40", ColumnName = "meter_user40"}},
				{"MeterUser41", new ColumnInformation() { DataType = "string", ModelName = "MeterUser41", ColumnName = "meter_user41"}},
				{"MeterUser42", new ColumnInformation() { DataType = "string", ModelName = "MeterUser42", ColumnName = "meter_user42"}},
				{"MeterUser43", new ColumnInformation() { DataType = "string", ModelName = "MeterUser43", ColumnName = "meter_user43"}},
				{"MeterUser44", new ColumnInformation() { DataType = "string", ModelName = "MeterUser44", ColumnName = "meter_user44"}},
				{"MeterUser45", new ColumnInformation() { DataType = "string", ModelName = "MeterUser45", ColumnName = "meter_user45"}},
				{"MeterUser46", new ColumnInformation() { DataType = "string", ModelName = "MeterUser46", ColumnName = "meter_user46"}},
				{"MeterUser47", new ColumnInformation() { DataType = "string", ModelName = "MeterUser47", ColumnName = "meter_user47"}},
				{"MeterUser48", new ColumnInformation() { DataType = "string", ModelName = "MeterUser48", ColumnName = "meter_user48"}},
				{"MeterUser49", new ColumnInformation() { DataType = "string", ModelName = "MeterUser49", ColumnName = "meter_user49"}},
				{"MeterUser50", new ColumnInformation() { DataType = "string", ModelName = "MeterUser50", ColumnName = "meter_user50"}},
				{"CreateDate", new ColumnInformation() { DataType = "DateTime", ModelName = "CreateDate", ColumnName = "create_date"}},
				{"CreateBy", new ColumnInformation() { DataType = "string", ModelName = "CreateBy", ColumnName = "create_by"}},
				{"ModDate", new ColumnInformation() { DataType = "DateTime", ModelName = "ModDate", ColumnName = "mod_date"}},
				{"ModBy", new ColumnInformation() { DataType = "string", ModelName = "ModBy", ColumnName = "mod_by"}},
				{"AltEqpNo", new ColumnInformation() { DataType = "string", ModelName = "AltEqpNo", ColumnName = "ALT_EQP_NO"}},
				{"Scalar", new ColumnInformation() { DataType = "decimal", ModelName = "Scalar", ColumnName = "SCALAR"}},
				{"MfrBuildDate", new ColumnInformation() { DataType = "DateTime", ModelName = "MfrBuildDate", ColumnName = "MFR_BUILD_DATE"}},
				{"TransactionId", new ColumnInformation() { DataType = "int", ModelName = "TransactionId", ColumnName = "transaction_id"}},
				{"ProgramId", new ColumnInformation() { DataType = "string", ModelName = "ProgramId", ColumnName = "program_id"}},
				{"FirmwareRev05", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev05", ColumnName = "FIRMWARE_REV05"}},
				{"FirmwareRev06", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev06", ColumnName = "FIRMWARE_REV06"}},
				{"FirmwareRev07", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev07", ColumnName = "FIRMWARE_REV07"}},
				{"FirmwareRev08", new ColumnInformation() { DataType = "string", ModelName = "FirmwareRev08", ColumnName = "FIRMWARE_REV08"}},
				{"PrefTestSequence", new ColumnInformation() { DataType = "string", ModelName = "PrefTestSequence", ColumnName = "pref_test_sequence"}},
				{"MeterClass", new ColumnInformation() { DataType = "int", ModelName = "MeterClass", ColumnName = "METER_CLASS"}},
				{"VehicleId", new ColumnInformation() { DataType = "string", ModelName = "VehicleId", ColumnName = "vehicle_id"}},
				{"ReceivedBy", new ColumnInformation() { DataType = "string", ModelName = "ReceivedBy", ColumnName = "received_by"}},
				{"IsFirstArticle", new ColumnInformation() { DataType = "string", ModelName = "IsFirstArticle", ColumnName = "is_first_article"}},
			};

	public override string ToString() 
	{
		return "WNDBA.TEQP_METER_HIST";
	}
}
}
#pragma warning restore 1591
