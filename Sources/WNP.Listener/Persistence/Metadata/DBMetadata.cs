
namespace AMSLLC.Listener.Persistence.Metadata {
using System.Collections.Generic;

public static class DBMetadata {
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionLogState
	/// <para />Class Name: TransactionLogState
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionLogStateImpl TransactionLogState = new TransactionLogStateImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: VersionInfo
	/// <para />Class Name: VersionInfo
	/// <para />Is View: False
	/// </summary>
	public static readonly VersionInfoImpl VersionInfo = new VersionInfoImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionType
	/// <para />Class Name: TransactionType
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionTypeImpl TransactionType = new TransactionTypeImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionStatus
	/// <para />Class Name: TransactionStatus
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionStatusImpl TransactionStatus = new TransactionStatusImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionState
	/// <para />Class Name: TransactionState
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionStateImpl TransactionState = new TransactionStateImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionSource
	/// <para />Class Name: TransactionSource
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionSourceImpl TransactionSource = new TransactionSourceImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: ServiceType
	/// <para />Class Name: ServiceType
	/// <para />Is View: False
	/// </summary>
	public static readonly ServiceTypeImpl ServiceType = new ServiceTypeImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: Company
	/// <para />Class Name: Company
	/// <para />Is View: False
	/// </summary>
	public static readonly CompanyImpl Company = new CompanyImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: Device
	/// <para />Class Name: Device
	/// <para />Is View: False
	/// </summary>
	public static readonly DeviceImpl Device = new DeviceImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: DeviceTest
	/// <para />Class Name: DeviceTest
	/// <para />Is View: False
	/// </summary>
	public static readonly DeviceTestImpl DeviceTest = new DeviceTestImpl();	
	/// <summary>
	/// <para />Schema: dbo
	/// <para />Table Name: TransactionLog
	/// <para />Class Name: TransactionLog
	/// <para />Is View: False
	/// </summary>
	public static readonly TransactionLogImpl TransactionLog = new TransactionLogImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TLOT_PERCENT_DEF
	/// <para />Class Name: TLOT_PERCENT_DEF
	/// <para />Is View: False
	/// </summary>
	public static readonly LotPercentDefImpl LotPercentDef = new LotPercentDefImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TLOT_ACCEPT_QUALITY_LEVEL
	/// <para />Class Name: TLOT_ACCEPT_QUALITY_LEVEL
	/// <para />Is View: False
	/// </summary>
	public static readonly LotAcceptQualityLevelImpl LotAcceptQualityLevel = new LotAcceptQualityLevelImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TIMPORT_EXPORT_OPTIONS
	/// <para />Class Name: TIMPORT_EXPORT_OPTION
	/// <para />Is View: False
	/// </summary>
	public static readonly ImportExportOptionsImpl ImportExportOptions = new ImportExportOptionsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TNEW_BATCH
	/// <para />Class Name: TNEW_BATCH
	/// <para />Is View: False
	/// </summary>
	public static readonly NewBatchImpl NewBatch = new NewBatchImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_TRANSDUCER_HIST
	/// <para />Class Name: TEQP_TRANSDUCER_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpTransducerHistImpl EqpTransducerHist = new EqpTransducerHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TLOT_SIZE_LOOKUP
	/// <para />Class Name: TLOT_SIZE_LOOKUP
	/// <para />Is View: False
	/// </summary>
	public static readonly LotSizeLookupImpl LotSizeLookup = new LotSizeLookupImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TIMPORT_EXPORT_DEFINITIONS
	/// <para />Class Name: TIMPORT_EXPORT_DEFINITION
	/// <para />Is View: False
	/// </summary>
	public static readonly ImportExportDefinitionsImpl ImportExportDefinitions = new ImportExportDefinitionsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: pbcatcol
	/// <para />Class Name: pbcatcol
	/// <para />Is View: False
	/// </summary>
	public static readonly PbcatcolImpl Pbcatcol = new PbcatcolImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_TOTALIZER_HIST
	/// <para />Class Name: TEQP_TOTALIZER_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpTotalizerHistImpl EqpTotalizerHist = new EqpTotalizerHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: pbcatfmt
	/// <para />Class Name: pbcatfmt
	/// <para />Is View: False
	/// </summary>
	public static readonly PbcatfmtImpl Pbcatfmt = new PbcatfmtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: pbcatvld
	/// <para />Class Name: pbcatvld
	/// <para />Is View: False
	/// </summary>
	public static readonly PbcatvldImpl Pbcatvld = new PbcatvldImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_TRANSDUCER
	/// <para />Class Name: TEQP_TRANSDUCER
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpTransducerImpl EqpTransducer = new EqpTransducerImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: pbcatedt
	/// <para />Class Name: pbcatedt
	/// <para />Is View: False
	/// </summary>
	public static readonly PbcatedtImpl Pbcatedt = new PbcatedtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_TOTALIZER
	/// <para />Class Name: TEQP_TOTALIZER
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpTotalizerImpl EqpTotalizer = new EqpTotalizerImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: pbcattbl
	/// <para />Class Name: pbcattbl
	/// <para />Is View: False
	/// </summary>
	public static readonly PbcattblImpl Pbcattbl = new PbcattblImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_RECORDER_HIST
	/// <para />Class Name: TEQP_RECORDER_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpRecorderHistImpl EqpRecorderHist = new EqpRecorderHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_RECORDER
	/// <para />Class Name: TEQP_RECORDER
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpRecorderImpl EqpRecorder = new EqpRecorderImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_PT_HIST
	/// <para />Class Name: TEQP_PT_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpPtHistImpl EqpPtHist = new EqpPtHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_PT
	/// <para />Class Name: TEQP_PT
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpPtImpl EqpPt = new EqpPtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_METER_HIST
	/// <para />Class Name: TEQP_METER_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpMeterHistImpl EqpMeterHist = new EqpMeterHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TLOCATION
	/// <para />Class Name: TLOCATION
	/// <para />Is View: False
	/// </summary>
	public static readonly LocationImpl Location = new LocationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTAMPER
	/// <para />Class Name: TTAMPER
	/// <para />Is View: False
	/// </summary>
	public static readonly TamperImpl Tamper = new TamperImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMANUFACTURERCODE
	/// <para />Class Name: TMANUFACTURERCODE
	/// <para />Is View: False
	/// </summary>
	public static readonly ManufacturercodeImpl Manufacturercode = new ManufacturercodeImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMETER_CODE
	/// <para />Class Name: TMETER_CODE
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterCodeImpl MeterCode = new MeterCodeImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tevent_triggers
	/// <para />Class Name: tevent_trigger
	/// <para />Is View: False
	/// </summary>
	public static readonly EventTriggersImpl EventTriggers = new EventTriggersImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tmeter_emulation
	/// <para />Class Name: tmeter_emulation
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterEmulationImpl MeterEmulation = new MeterEmulationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTRANSFORMER_CODE_CT
	/// <para />Class Name: TTRANSFORMER_CODE_CT
	/// <para />Is View: False
	/// </summary>
	public static readonly TransformerCodeCtImpl TransformerCodeCt = new TransformerCodeCtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMETER_POWER_SETUP
	/// <para />Class Name: TMETER_POWER_SETUP
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterPowerSetupImpl MeterPowerSetup = new MeterPowerSetupImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTRANSFORMER_CODE_PT
	/// <para />Class Name: TTRANSFORMER_CODE_PT
	/// <para />Is View: False
	/// </summary>
	public static readonly TransformerCodePtImpl TransformerCodePt = new TransformerCodePtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMETER_TEST_RESULTS
	/// <para />Class Name: TMETER_TEST_RESULT
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterTestResultsImpl MeterTestResults = new MeterTestResultsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tsite_metrics
	/// <para />Class Name: tsite_metric
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteMetricsImpl SiteMetrics = new SiteMetricsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tmeter_test_sequence
	/// <para />Class Name: tmeter_test_sequence
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterTestSequenceImpl MeterTestSequence = new MeterTestSequenceImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tsite_wiring_check
	/// <para />Class Name: tsite_wiring_check
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteWiringCheckImpl SiteWiringCheck = new SiteWiringCheckImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMFR
	/// <para />Class Name: TMFR
	/// <para />Is View: False
	/// </summary>
	public static readonly MfrImpl Mfr = new MfrImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMAP_WINBOARD2
	/// <para />Class Name: TMAP_WINBOARD2
	/// <para />Is View: False
	/// </summary>
	public static readonly MapWinboard2Impl MapWinboard2 = new MapWinboard2Impl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: trma_batch_out
	/// <para />Class Name: trma_batch_out
	/// <para />Is View: False
	/// </summary>
	public static readonly RmaBatchOutImpl RmaBatchOut = new RmaBatchOutImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TMULTIMEDIA
	/// <para />Class Name: TMULTIMEDIUM
	/// <para />Is View: False
	/// </summary>
	public static readonly MultimediaImpl Multimedia = new MultimediaImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: trma_eqp
	/// <para />Class Name: trma_eqp
	/// <para />Is View: False
	/// </summary>
	public static readonly RmaEqpImpl RmaEqp = new RmaEqpImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TREPAIR
	/// <para />Class Name: TREPAIR
	/// <para />Is View: False
	/// </summary>
	public static readonly RepairImpl Repair = new RepairImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TOWNER
	/// <para />Class Name: TOWNER
	/// <para />Is View: False
	/// </summary>
	public static readonly OwnerImpl Owner = new OwnerImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tmetadata
	/// <para />Class Name: tmetadatum
	/// <para />Is View: False
	/// </summary>
	public static readonly MetadataImpl Metadata = new MetadataImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TPROCESS_FLOW
	/// <para />Class Name: TPROCESS_FLOW
	/// <para />Is View: False
	/// </summary>
	public static readonly ProcessFlowImpl ProcessFlow = new ProcessFlowImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TPT_TEST_RESULTS
	/// <para />Class Name: TPT_TEST_RESULT
	/// <para />Is View: False
	/// </summary>
	public static readonly PtTestResultsImpl PtTestResults = new PtTestResultsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TREAD_SET
	/// <para />Class Name: TREAD_SET
	/// <para />Is View: False
	/// </summary>
	public static readonly ReadSetImpl ReadSet = new ReadSetImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TREADING
	/// <para />Class Name: TREADING
	/// <para />Is View: False
	/// </summary>
	public static readonly ReadingImpl Reading = new ReadingImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSAMPLE_LIST
	/// <para />Class Name: TSAMPLE_LIST
	/// <para />Is View: False
	/// </summary>
	public static readonly SampleListImpl SampleList = new SampleListImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSAMPLE_LIST_ARCH
	/// <para />Class Name: TSAMPLE_LIST_ARCH
	/// <para />Is View: False
	/// </summary>
	public static readonly SampleListArchImpl SampleListArch = new SampleListArchImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tdefault_values
	/// <para />Class Name: tdefault_value
	/// <para />Is View: False
	/// </summary>
	public static readonly DefaultValuesImpl DefaultValues = new DefaultValuesImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSECURITY_GROUPS
	/// <para />Class Name: TSECURITY_GROUP
	/// <para />Is View: False
	/// </summary>
	public static readonly SecurityGroupsImpl SecurityGroups = new SecurityGroupsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSECURITY_RIGHTS
	/// <para />Class Name: TSECURITY_RIGHT
	/// <para />Is View: False
	/// </summary>
	public static readonly SecurityRightsImpl SecurityRights = new SecurityRightsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSECURITY_USERS
	/// <para />Class Name: TSECURITY_USER
	/// <para />Is View: False
	/// </summary>
	public static readonly SecurityUsersImpl SecurityUsers = new SecurityUsersImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tred_tag
	/// <para />Class Name: tred_tag
	/// <para />Is View: False
	/// </summary>
	public static readonly RedTagImpl RedTag = new RedTagImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSELECTION_PROGRAM
	/// <para />Class Name: TSELECTION_PROGRAM
	/// <para />Is View: False
	/// </summary>
	public static readonly SelectionProgramImpl SelectionProgram = new SelectionProgramImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tred_tag_control
	/// <para />Class Name: tred_tag_control
	/// <para />Is View: False
	/// </summary>
	public static readonly RedTagControlImpl RedTagControl = new RedTagControlImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSELECTION_PROGRAM_GROUP
	/// <para />Class Name: TSELECTION_PROGRAM_GROUP
	/// <para />Is View: False
	/// </summary>
	public static readonly SelectionProgramGroupImpl SelectionProgramGroup = new SelectionProgramGroupImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSHOP_GOALS
	/// <para />Class Name: TSHOP_GOAL
	/// <para />Is View: False
	/// </summary>
	public static readonly ShopGoalsImpl ShopGoals = new ShopGoalsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_state
	/// <para />Class Name: tlistener_transaction_state
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionStateImpl ListenerTransactionState = new ListenerTransactionStateImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSITE
	/// <para />Class Name: TSITE
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteImpl Site = new SiteImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_statistics
	/// <para />Class Name: tlistener_transaction_statistic
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionStatisticsImpl ListenerTransactionStatistics = new ListenerTransactionStatisticsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSITE_COMMENTS
	/// <para />Class Name: TSITE_COMMENT
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteCommentsImpl SiteComments = new SiteCommentsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_status
	/// <para />Class Name: tlistener_transaction_status
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionStatusImpl ListenerTransactionStatus = new ListenerTransactionStatusImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSITE_HIST
	/// <para />Class Name: TSITE_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteHistImpl SiteHist = new SiteHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_type
	/// <para />Class Name: tlistener_transaction_type
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionTypeImpl ListenerTransactionType = new ListenerTransactionTypeImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSITE_MULTIMEDIA
	/// <para />Class Name: TSITE_MULTIMEDIUM
	/// <para />Is View: False
	/// </summary>
	public static readonly SiteMultimediaImpl SiteMultimedia = new SiteMultimediaImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tsocket_mapping
	/// <para />Class Name: tsocket_mapping
	/// <para />Is View: False
	/// </summary>
	public static readonly SocketMappingImpl SocketMapping = new SocketMappingImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSTATUS
	/// <para />Class Name: TSTATUS
	/// <para />Is View: False
	/// </summary>
	public static readonly StatusImpl Status = new StatusImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_config_notifications
	/// <para />Class Name: tlistener_config_notification
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerConfigNotificationsImpl ListenerConfigNotifications = new ListenerConfigNotificationsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSYS_CONTROL
	/// <para />Class Name: TSYS_CONTROL
	/// <para />Is View: False
	/// </summary>
	public static readonly SysControlImpl SysControl = new SysControlImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TSYS_VALIDATION
	/// <para />Class Name: TSYS_VALIDATION
	/// <para />Is View: False
	/// </summary>
	public static readonly SysValidationImpl SysValidation = new SysValidationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tfirmware_label
	/// <para />Class Name: tfirmware_label
	/// <para />Is View: False
	/// </summary>
	public static readonly FirmwareLabelImpl FirmwareLabel = new FirmwareLabelImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_config
	/// <para />Class Name: tlistener_config
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerConfigImpl ListenerConfig = new ListenerConfigImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tbarcode_ct
	/// <para />Class Name: tbarcode_ct
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodeCtImpl BarcodeCt = new BarcodeCtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTRACKING
	/// <para />Class Name: TTRACKING
	/// <para />Is View: False
	/// </summary>
	public static readonly TrackingImpl Tracking = new TrackingImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tbarcode_pt
	/// <para />Class Name: tbarcode_pt
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodePtImpl BarcodePt = new BarcodePtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTRACKING_IN
	/// <para />Class Name: TTRACKING_IN
	/// <para />Is View: False
	/// </summary>
	public static readonly TrackingInImpl TrackingIn = new TrackingInImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_log
	/// <para />Class Name: tlistener_transaction_log
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionLogImpl ListenerTransactionLog = new ListenerTransactionLogImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTRACKING_OUT
	/// <para />Class Name: TTRACKING_OUT
	/// <para />Is View: False
	/// </summary>
	public static readonly TrackingOutImpl TrackingOut = new TrackingOutImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tlistener_transaction_source
	/// <para />Class Name: tlistener_transaction_source
	/// <para />Is View: False
	/// </summary>
	public static readonly ListenerTransactionSourceImpl ListenerTransactionSource = new ListenerTransactionSourceImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: ttransducer_test_results
	/// <para />Class Name: ttransducer_test_result
	/// <para />Is View: False
	/// </summary>
	public static readonly TransducerTestResultsImpl TransducerTestResults = new TransducerTestResultsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TBARCODE
	/// <para />Class Name: TBARCODE
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodeImpl Barcode = new BarcodeImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tmeter_limit_set
	/// <para />Class Name: tmeter_limit_set
	/// <para />Is View: False
	/// </summary>
	public static readonly MeterLimitSetImpl MeterLimitSet = new MeterLimitSetImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TVALIDATION
	/// <para />Class Name: TVALIDATION
	/// <para />Is View: False
	/// </summary>
	public static readonly ValidationImpl Validation = new ValidationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TBARCODE_CONTROL
	/// <para />Class Name: TBARCODE_CONTROL
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodeControlImpl BarcodeControl = new BarcodeControlImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: taudit_log
	/// <para />Class Name: taudit_log
	/// <para />Is View: False
	/// </summary>
	public static readonly AuditLogImpl AuditLog = new AuditLogImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TVENDOR
	/// <para />Class Name: TVENDOR
	/// <para />Is View: False
	/// </summary>
	public static readonly VendorImpl Vendor = new VendorImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tkyz
	/// <para />Class Name: tkyz
	/// <para />Is View: False
	/// </summary>
	public static readonly KyzImpl Kyz = new KyzImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TVENDOR_CONTACT
	/// <para />Class Name: TVENDOR_CONTACT
	/// <para />Is View: False
	/// </summary>
	public static readonly VendorContactImpl VendorContact = new VendorContactImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: trma_batch_in
	/// <para />Class Name: trma_batch_in
	/// <para />Is View: False
	/// </summary>
	public static readonly RmaBatchInImpl RmaBatchIn = new RmaBatchInImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tharmonic_configuration
	/// <para />Class Name: tharmonic_configuration
	/// <para />Is View: False
	/// </summary>
	public static readonly HarmonicConfigurationImpl HarmonicConfiguration = new HarmonicConfigurationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TBARCODE_LABEL
	/// <para />Class Name: TBARCODE_LABEL
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodeLabelImpl BarcodeLabel = new BarcodeLabelImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tversion
	/// <para />Class Name: tversion
	/// <para />Is View: False
	/// </summary>
	public static readonly VersionImpl Version = new VersionImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TBARCODE_LABEL_DEF
	/// <para />Class Name: TBARCODE_LABEL_DEF
	/// <para />Is View: False
	/// </summary>
	public static readonly BarcodeLabelDefImpl BarcodeLabelDef = new BarcodeLabelDefImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tharmonic_config_data
	/// <para />Class Name: tharmonic_config_datum
	/// <para />Is View: False
	/// </summary>
	public static readonly HarmonicConfigDataImpl HarmonicConfigData = new HarmonicConfigDataImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TVERSION_HISTORY
	/// <para />Class Name: TVERSION_HISTORY
	/// <para />Is View: False
	/// </summary>
	public static readonly VersionHistoryImpl VersionHistory = new VersionHistoryImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TWORKSTATION
	/// <para />Class Name: TWORKSTATION
	/// <para />Is View: False
	/// </summary>
	public static readonly WorkstationImpl Workstation = new WorkstationImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TCIRCUIT
	/// <para />Class Name: TCIRCUIT
	/// <para />Is View: False
	/// </summary>
	public static readonly CircuitImpl Circuit = new CircuitImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TTEST_CARD
	/// <para />Class Name: TTEST_CARD
	/// <para />Is View: False
	/// </summary>
	public static readonly TestCardImpl TestCard = new TestCardImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TRATIO
	/// <para />Class Name: TRATIO
	/// <para />Is View: False
	/// </summary>
	public static readonly RatioImpl Ratio = new RatioImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_REFERENCE
	/// <para />Class Name: TEQP_REFERENCE
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpReferenceImpl EqpReference = new EqpReferenceImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TCIRCUIT_HIST
	/// <para />Class Name: TCIRCUIT_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly CircuitHistImpl CircuitHist = new CircuitHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_TESTBOARD
	/// <para />Class Name: TEQP_TESTBOARD
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpTestboardImpl EqpTestboard = new EqpTestboardImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TCOMMENT
	/// <para />Class Name: TCOMMENT
	/// <para />Is View: False
	/// </summary>
	public static readonly CommentImpl Comment = new CommentImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TCT_TEST_RESULTS
	/// <para />Class Name: TCT_TEST_RESULT
	/// <para />Is View: False
	/// </summary>
	public static readonly CtTestResultsImpl CtTestResults = new CtTestResultsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TREFERENCE_INSTALL_HISTORY
	/// <para />Class Name: TREFERENCE_INSTALL_HISTORY
	/// <para />Is View: False
	/// </summary>
	public static readonly ReferenceInstallHistoryImpl ReferenceInstallHistory = new ReferenceInstallHistoryImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TCUSTOM_FIELD_DESCRIPTIONS
	/// <para />Class Name: TCUSTOM_FIELD_DESCRIPTION
	/// <para />Is View: False
	/// </summary>
	public static readonly CustomFieldDescriptionsImpl CustomFieldDescriptions = new CustomFieldDescriptionsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TREFERENCE_TEST_RESULTS
	/// <para />Class Name: TREFERENCE_TEST_RESULT
	/// <para />Is View: False
	/// </summary>
	public static readonly ReferenceTestResultsImpl ReferenceTestResults = new ReferenceTestResultsImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_CT
	/// <para />Class Name: TEQP_CT
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpCtImpl EqpCt = new EqpCtImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: tstandards_compare_sequence
	/// <para />Class Name: tstandards_compare_sequence
	/// <para />Is View: False
	/// </summary>
	public static readonly StandardsCompareSequenceImpl StandardsCompareSequence = new StandardsCompareSequenceImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_CT_HIST
	/// <para />Class Name: TEQP_CT_HIST
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpCtHistImpl EqpCtHist = new EqpCtHistImpl();	
	/// <summary>
	/// <para />Schema: wndba
	/// <para />Table Name: TEQP_METER
	/// <para />Class Name: TEQP_METER
	/// <para />Is View: False
	/// </summary>
	public static readonly EqpMeterImpl EqpMeter = new EqpMeterImpl();	

	public static Dictionary<string, ITableInformation> TableLookup = new Dictionary<string, ITableInformation>() 
	{
				{TransactionLogState.RealTableName, TransactionLogState},
				{VersionInfo.RealTableName, VersionInfo},
				{TransactionType.RealTableName, TransactionType},
				{TransactionStatus.RealTableName, TransactionStatus},
				{TransactionState.RealTableName, TransactionState},
				{TransactionSource.RealTableName, TransactionSource},
				{ServiceType.RealTableName, ServiceType},
				{Company.RealTableName, Company},
				{Device.RealTableName, Device},
				{DeviceTest.RealTableName, DeviceTest},
				{TransactionLog.RealTableName, TransactionLog},
				{LotPercentDef.RealTableName, LotPercentDef},
				{LotAcceptQualityLevel.RealTableName, LotAcceptQualityLevel},
				{ImportExportOptions.RealTableName, ImportExportOptions},
				{NewBatch.RealTableName, NewBatch},
				{EqpTransducerHist.RealTableName, EqpTransducerHist},
				{LotSizeLookup.RealTableName, LotSizeLookup},
				{ImportExportDefinitions.RealTableName, ImportExportDefinitions},
				{Pbcatcol.RealTableName, Pbcatcol},
				{EqpTotalizerHist.RealTableName, EqpTotalizerHist},
				{Pbcatfmt.RealTableName, Pbcatfmt},
				{Pbcatvld.RealTableName, Pbcatvld},
				{EqpTransducer.RealTableName, EqpTransducer},
				{Pbcatedt.RealTableName, Pbcatedt},
				{EqpTotalizer.RealTableName, EqpTotalizer},
				{Pbcattbl.RealTableName, Pbcattbl},
				{EqpRecorderHist.RealTableName, EqpRecorderHist},
				{EqpRecorder.RealTableName, EqpRecorder},
				{EqpPtHist.RealTableName, EqpPtHist},
				{EqpPt.RealTableName, EqpPt},
				{EqpMeterHist.RealTableName, EqpMeterHist},
				{Location.RealTableName, Location},
				{Tamper.RealTableName, Tamper},
				{Manufacturercode.RealTableName, Manufacturercode},
				{MeterCode.RealTableName, MeterCode},
				{EventTriggers.RealTableName, EventTriggers},
				{MeterEmulation.RealTableName, MeterEmulation},
				{TransformerCodeCt.RealTableName, TransformerCodeCt},
				{MeterPowerSetup.RealTableName, MeterPowerSetup},
				{TransformerCodePt.RealTableName, TransformerCodePt},
				{MeterTestResults.RealTableName, MeterTestResults},
				{SiteMetrics.RealTableName, SiteMetrics},
				{MeterTestSequence.RealTableName, MeterTestSequence},
				{SiteWiringCheck.RealTableName, SiteWiringCheck},
				{Mfr.RealTableName, Mfr},
				{MapWinboard2.RealTableName, MapWinboard2},
				{RmaBatchOut.RealTableName, RmaBatchOut},
				{Multimedia.RealTableName, Multimedia},
				{RmaEqp.RealTableName, RmaEqp},
				{Repair.RealTableName, Repair},
				{Owner.RealTableName, Owner},
				{Metadata.RealTableName, Metadata},
				{ProcessFlow.RealTableName, ProcessFlow},
				{PtTestResults.RealTableName, PtTestResults},
				{ReadSet.RealTableName, ReadSet},
				{Reading.RealTableName, Reading},
				{SampleList.RealTableName, SampleList},
				{SampleListArch.RealTableName, SampleListArch},
				{DefaultValues.RealTableName, DefaultValues},
				{SecurityGroups.RealTableName, SecurityGroups},
				{SecurityRights.RealTableName, SecurityRights},
				{SecurityUsers.RealTableName, SecurityUsers},
				{RedTag.RealTableName, RedTag},
				{SelectionProgram.RealTableName, SelectionProgram},
				{RedTagControl.RealTableName, RedTagControl},
				{SelectionProgramGroup.RealTableName, SelectionProgramGroup},
				{ShopGoals.RealTableName, ShopGoals},
				{ListenerTransactionState.RealTableName, ListenerTransactionState},
				{Site.RealTableName, Site},
				{ListenerTransactionStatistics.RealTableName, ListenerTransactionStatistics},
				{SiteComments.RealTableName, SiteComments},
				{ListenerTransactionStatus.RealTableName, ListenerTransactionStatus},
				{SiteHist.RealTableName, SiteHist},
				{ListenerTransactionType.RealTableName, ListenerTransactionType},
				{SiteMultimedia.RealTableName, SiteMultimedia},
				{SocketMapping.RealTableName, SocketMapping},
				{Status.RealTableName, Status},
				{ListenerConfigNotifications.RealTableName, ListenerConfigNotifications},
				{SysControl.RealTableName, SysControl},
				{SysValidation.RealTableName, SysValidation},
				{FirmwareLabel.RealTableName, FirmwareLabel},
				{ListenerConfig.RealTableName, ListenerConfig},
				{BarcodeCt.RealTableName, BarcodeCt},
				{Tracking.RealTableName, Tracking},
				{BarcodePt.RealTableName, BarcodePt},
				{TrackingIn.RealTableName, TrackingIn},
				{ListenerTransactionLog.RealTableName, ListenerTransactionLog},
				{TrackingOut.RealTableName, TrackingOut},
				{ListenerTransactionSource.RealTableName, ListenerTransactionSource},
				{TransducerTestResults.RealTableName, TransducerTestResults},
				{Barcode.RealTableName, Barcode},
				{MeterLimitSet.RealTableName, MeterLimitSet},
				{Validation.RealTableName, Validation},
				{BarcodeControl.RealTableName, BarcodeControl},
				{AuditLog.RealTableName, AuditLog},
				{Vendor.RealTableName, Vendor},
				{Kyz.RealTableName, Kyz},
				{VendorContact.RealTableName, VendorContact},
				{RmaBatchIn.RealTableName, RmaBatchIn},
				{HarmonicConfiguration.RealTableName, HarmonicConfiguration},
				{BarcodeLabel.RealTableName, BarcodeLabel},
				{Version.RealTableName, Version},
				{BarcodeLabelDef.RealTableName, BarcodeLabelDef},
				{HarmonicConfigData.RealTableName, HarmonicConfigData},
				{VersionHistory.RealTableName, VersionHistory},
				{Workstation.RealTableName, Workstation},
				{Circuit.RealTableName, Circuit},
				{TestCard.RealTableName, TestCard},
				{Ratio.RealTableName, Ratio},
				{EqpReference.RealTableName, EqpReference},
				{CircuitHist.RealTableName, CircuitHist},
				{EqpTestboard.RealTableName, EqpTestboard},
				{Comment.RealTableName, Comment},
				{CtTestResults.RealTableName, CtTestResults},
				{ReferenceInstallHistory.RealTableName, ReferenceInstallHistory},
				{CustomFieldDescriptions.RealTableName, CustomFieldDescriptions},
				{ReferenceTestResults.RealTableName, ReferenceTestResults},
				{EqpCt.RealTableName, EqpCt},
				{StandardsCompareSequence.RealTableName, StandardsCompareSequence},
				{EqpCtHist.RealTableName, EqpCtHist},
				{EqpMeter.RealTableName, EqpMeter},
			};

	public static readonly string ALL = "*";

	public static string C(params string[] columns) {
		return string.Join(", ", columns);
	}
}

public interface ITableInformation 
{
	string RealTableName {get;}
	Dictionary<string, ColumnInformation> ColumnsLookup {get;}
}

public class ColumnInformation 
{
	public string DataType { get; set; }
	public string ModelName { get; set; }
	public string ColumnName { get; set; }
}
}

