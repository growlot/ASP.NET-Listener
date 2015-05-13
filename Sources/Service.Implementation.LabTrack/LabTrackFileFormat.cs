//-----------------------------------------------------------------------
// <copyright file="LabTrackFileFormat.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.LabTrack
{
    using System;
    using AMSLLC.Listener.Service.Implementation.FlatFile;
    using FileHelpers;
    using FileHelpers.Dynamic;

    /// <summary>
    /// FileHelpers definition of LabTrack cis export file.
    /// </summary>
    [DelimitedRecord("\t")]
    public class LabTrackFileFormat
    {
        #region STOCKHEADER
        /// <summary>
        /// The code used to identify the company
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderCompanyCode;
        
        /// <summary>
        /// The device type code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderDeviceTypeCode;

        /// <summary>
        /// The manufacturer code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderManufacturerCode;

        /// <summary>
        /// The device identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderDeviceId;

        /// <summary>
        /// The device identifier assigned by manufacturer
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderAlternativeDeviceId;

        /// <summary>
        /// The purchase order group number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderPurchaseOrderGroupNumber;

        /// <summary>
        /// The current location code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string StockHeaderCurrentLocationCode;

        /// <summary>
        /// The status code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? StockHeaderStatusCode;

        /// <summary>
        /// The hold device flag
        /// Flag used to prevent shipment of meter 
        /// 'Y' = Hold
        /// 'N' = No Hold
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char StockHeaderHoldDevice;

        /// <summary>
        /// The limits flag 
        /// 'Y' = In limits 
        /// 'N' = Not in limits
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char StockHeaderLimits;

        /// <summary>
        /// The inventory flag 
        /// 'Y' = Device has been inventoried
        /// 'N' = Not inventoried
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char StockHeaderInventory;

        /// <summary>
        /// The change date
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime? StockHeaderChangeDate;

        /// <summary>
        /// The event trigger 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? StockHeaderEventTrigger1;

        /// <summary>
        /// The event trigger 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? StockHeaderEventTrigger2;
        #endregion

        #region EMTRHEADER
        /// <summary>
        /// The meter type/model code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderTypeCode;

        /// <summary>
        /// The code used to identify meter characteristics including form, base, test voltage, test current and disk constant, etc.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderSetupCode;

        /// <summary>
        /// The number of kwh dials on meter register
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterHeaderKwhDials;

        /// <summary>
        /// The number of kw dials on meter register
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderKWDials;

        /// <summary>
        /// The Meter billing multiplier to be applied to the KWH reading
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.0000")]
        public decimal MeterHeaderKwhMultiplier;

        /// <summary>
        /// The meter billing multiplier to be applied to the KW reading
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.0000")]
        public decimal MeterHeaderKWMultiplier;

        /// <summary>
        /// The user code 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderUserCode1;

        /// <summary>
        /// The user code 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderUserCode2;

        /// <summary>
        /// The user field 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderUserField1;

        /// <summary>
        /// The user field 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterHeaderUserField2;

        /// <summary>
        /// The user number 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterHeaderUserNumber1;

        /// <summary>
        /// The user number 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterHeaderUserNumber2;

        /// <summary>
        /// The user double 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? MeterHeaderUserDouble1;

        /// <summary>
        /// The user double 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? MeterHeaderUserDouble2;
        #endregion

        #region METERQUEUECODE
        /// <summary>
        /// The meters class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterQueueClass;

        /// <summary>
        /// The code to identify which test sequence was used to test meter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterQueueSequence;

        /// <summary>
        /// The code to identify the upper and lower limits used for testing
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterQueueLimits;

        /// <summary>
        /// The code to identify which optics type used to test meter
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterQueueOptics;

        /// <summary>
        /// The meter queue user field 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterQueueUserField1;

        /// <summary>
        /// The meter queue user field 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterQueueUserField2;
        #endregion

        #region EMTRTYPECODE
        /// <summary>
        /// The meter's manufacturer type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterTypeManufacturerMeterType;

        /// <summary>
        /// The alternate meter's manufacturer type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterTypeAlternateManufacturerMeterType;

        /// <summary>
        /// The manufacturer code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterTypeManufacturerCode;
        
        /// <summary>
        /// The user field 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterTypeUserField1;

        /// <summary>
        /// The user field 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterTypeUserField2;

        /// <summary>
        /// The user number 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterTypeUserNumber1;

        /// <summary>
        /// The user number 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterTypeUserNumber2;

        /// <summary>
        /// The user double 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? MeterTypeUserDouble1;

        /// <summary>
        /// The user double 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? MeterTypeUserDouble2;
        #endregion

        #region EMTRSETUPCODE
        /// <summary>
        /// The meter's form
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? MeterSetupForm;

        /// <summary>
        /// The meter's base
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char MeterSetupBase;

        /// <summary>
        /// The meter's test voltage
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? MeterSetupTestVoltage;

        /// <summary>
        /// The meter's test current
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? MeterSetupTestCurrent;

        /// <summary>
        /// The meter's disk constant
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.000")]
        public decimal MeterSetupDiskConstant;

        /// <summary>
        /// The meter's phase
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char MeterSetupPhase;

        /// <summary>
        /// The number of wires
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char MeterSetupWire;

        /// <summary>
        /// The meter's register ratio
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupRegisterRatio;

        /// <summary>
        /// The user field1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField1;

        /// <summary>
        /// The user field2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField2;

        /// <summary>
        /// The user field3
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField3;

        /// <summary>
        /// The user field4
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField4;

        /// <summary>
        /// The user field5
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField5;

        /// <summary>
        /// The user field6
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField6;

        /// <summary>
        /// The user field7
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField7;

        /// <summary>
        /// The user field8
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField8;

        /// <summary>
        /// The user field9
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField9;

        /// <summary>
        /// The user field10
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string MeterSetupUserField10;
        #endregion

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown1;

        #region EMTRSHOPTESTHISTORY
        /// <summary>
        /// The test is in limits flag
        /// 'Y' = In limits
        /// 'N' = Out of limits
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char? ShopTestHistoryTestInLimits;

        /// <summary>
        /// As found test date
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime? ShopTestHistoryAsFoundTestDate;

        /// <summary>
        /// As found test time
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "HH:mm:ss")]
        public DateTime? ShopTestHistoryAsFoundTestTime;

        /// <summary>
        /// As found tester identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryAsFoundTesterId;

        /// <summary>
        /// As found board number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTestHistoryAsFoundBoardNumber;

        /// <summary>
        /// As found KWH dial reading
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryAsFoundDialReading;

        /// <summary>
        /// As found series full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundSeriesFull;

        /// <summary>
        /// As found series power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundSeriesPower;

        /// <summary>
        /// As found series light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundSeriesLight;
        
        /// <summary>
        /// As found element A full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementAFull;

        /// <summary>
        /// As found element A power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementAPower;

        /// <summary>
        /// As found element A light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ALight", Justification = "False possitive")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementALight;
        
        /// <summary>
        /// As found element B full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementBFull;

        /// <summary>
        /// As found element B power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementBPower;

        /// <summary>
        /// As found element B light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BLight", Justification = "False possitive")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementBLight;
        
        /// <summary>
        /// As found element C full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementCFull;

        /// <summary>
        /// As found element C power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementCPower;

        /// <summary>
        /// As found element C light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsFoundElementCLight;
        
        /// <summary>
        /// As left test date
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime? ShopTestHistoryAsLeftTestDate;

        /// <summary>
        /// As left test time
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "HH:mm:ss")]
        public DateTime? ShopTestHistoryAsLeftTestTime;

        /// <summary>
        /// As left tester identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryAsLeftTesterId;

        /// <summary>
        /// As found board number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTestHistoryAsLeftBoardNumber;

        /// <summary>
        /// As found KWH dial reading
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryAsLeftDialReading;

        /// <summary>
        /// As left series full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftSeriesFull;

        /// <summary>
        /// As left series power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftSeriesPower;

        /// <summary>
        /// As left series light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftSeriesLight;
        
        /// <summary>
        /// As left element A full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementAFull;

        /// <summary>
        /// As left element A power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementAPower;

        /// <summary>
        /// As left element A light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ALight", Justification = "False possitive")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementALight;
        
        /// <summary>
        /// As left element B full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementBFull;

        /// <summary>
        /// As left element B power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementBPower;

        /// <summary>
        /// As left element B light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "BLight", Justification = "False possitive")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementBLight;
        
        /// <summary>
        /// As left element C full load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementCFull;

        /// <summary>
        /// As left element C power test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementCPower;

        /// <summary>
        /// As left element C light load test result
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "0.00", "")]
        public decimal? ShopTestHistoryAsLeftElementCLight;

        /// <summary>
        /// The user code 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryUserCode1;

        /// <summary>
        /// The user code 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryUserCode2;

        /// <summary>
        /// The user field 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryUserField1;

        /// <summary>
        /// The user field 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryUserField2;

        /// <summary>
        /// The user number 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTestHistoryUserNumber1;

        /// <summary>
        /// The user number 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTestHistoryUserNumber2;

        /// <summary>
        /// The comment
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryComment;

        /// <summary>
        /// The clerk (user who enters manual tests) identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTestHistoryClerkId;

        /// <summary>
        /// The flag indicating if creep test passed
        /// 'Y' = Passed
        /// 'N' = Failed
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public char ShopTestHistoryCreepPass;
        
        /// <summary>
        /// The user double 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? ShopTestHistoryUserDouble1;

        /// <summary>
        /// The user double 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? ShopTestHistoryUserDouble2;
        #endregion

        #region SHOPTRANSFERHISTORY
        /// <summary>
        /// The user identifier who received the device
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryReceiverId;

        /// <summary>
        /// The date device was received
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime? ShopTransferHistoryReceiveDate;

        /// <summary>
        /// The time device was received
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "HH:mm:ss")]
        public DateTime? ShopTransferHistoryReceiveTime;

        /// <summary>
        /// The user identifier who shipped the device
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryShipperId;

        /// <summary>
        /// The date device was shipped 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime? ShopTransferHistoryShipDate;

        /// <summary>
        /// The time device was shipped
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldConverter(ConverterKind.Date, "HH:mm:ss")]
        public DateTime? ShopTransferHistoryShipTime;

        /// <summary>
        /// The shipped to location
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryShippedToLocation;

        /// <summary>
        /// The shipping comments
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryComments;

        /// <summary>
        /// The user code 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryUserCode1;

        /// <summary>
        /// The user code 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryUserCode2;

        /// <summary>
        /// The user field 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryUserField1;

        /// <summary>
        /// The user field 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string ShopTransferHistoryUserField2;

        /// <summary>
        /// The user number 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTransferHistoryUserNumber1;

        /// <summary>
        /// The user number 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public int? ShopTransferHistoryUserNumber2;

        /// <summary>
        /// The user double 1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? ShopTransferHistoryUserDouble1;

        /// <summary>
        /// The user double 2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? ShopTransferHistoryUserDouble2;
        #endregion

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown2;

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown3;

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown4;

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown5;

        /// <summary>
        /// Unknown field used as a filler to align with data in sample export file
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public string Unknown6;

        /// <summary>
        /// As found weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? AsFoundWeightedAverage;

        /// <summary>
        /// As left weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        public decimal? AsLeftWeightedAverage;
    }
}
