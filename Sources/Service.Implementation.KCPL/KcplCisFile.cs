//-----------------------------------------------------------------------
// <copyright file="KcplCisFile.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// FileHelpers definition of KCPL cis export file.
    /// </summary>
    [FixedLengthRecord]
    public class KcplCisFile
    {
        /// <summary>
        /// The tester identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Left, "0")]
        [FieldAlign(AlignMode.Right)]
        public string TesterId;

        /// <summary>
        /// The test start date
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(16)]
        [FieldConverter(ConverterKind.Date, "MM/dd/yyHH:MM:ss")]
        public DateTime TestStartDate;

        /// <summary>
        /// The test end time
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "HH:MM:ss")]
        public DateTime TestEndTime;

        /// <summary>
        /// The station number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Left)]
        [FieldAlign(AlignMode.Right)]
        public string StationNumber;

        /// <summary>
        /// The not used1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        public string NotUsed1;

        /// <summary>
        /// The location
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        [FieldAlign(AlignMode.Left)]
        public string Location;

        /// <summary>
        /// The test code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        [FieldAlign(AlignMode.Left)]
        public string TestCode;

        /// <summary>
        /// The manufacturer
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string Manufacturer;

        /// <summary>
        /// The company code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(4)]
        [FieldAlign(AlignMode.Right)]
        public string CompanyCode;

        /// <summary>
        /// The meter number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        public string MeterNumber;

        /// <summary>
        /// The kw reading
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Left, "0")]
        [FieldNullValue("00000000")]
        public string KWReading;

        /// <summary>
        /// The not used2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        public string NotUsed2;

        /// <summary>
        /// The form
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Left)]
        [FieldAlign(AlignMode.Right)]
        public string Form;

        /// <summary>
        /// The base
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string Base;

        /// <summary>
        /// The volts
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(3)]
        [FieldConverter(ConverterKind.Int32)]
        public int Volts;

        /// <summary>
        /// The KH
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(5)]
        [FieldAlign(AlignMode.Right)]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal KH;

        /// <summary>
        /// As found full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsFoundFullLoad;

        /// <summary>
        /// As found light load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsFoundLightLoad;

        /// <summary>
        /// As found power factor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsFoundPowerFactor;

        /// <summary>
        /// As left full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsLeftFullLoad;

        /// <summary>
        /// As left light load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsLeftLightLoad;

        /// <summary>
        /// As left power factor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "000.00")]
        public decimal AsLeftPowerFactor;

        /// <summary>
        /// The balance
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Left)]
        public string Balance;

        /// <summary>
        /// As found demand
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "000.00", "")]
        public decimal AsFoundDemand;

        /// <summary>
        /// As left demand
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(NullableFormattedDecimalConverter), "000.00", "")]
        public decimal AsLeftDemand;

        /// <summary>
        /// As found weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldAlign(AlignMode.Left)]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal AsFoundWeightedAverage;

        /// <summary>
        /// As left weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldAlign(AlignMode.Left)]
        [FieldConverter(ConverterKind.Decimal)]
        public decimal AsLeftWeightedAverage;

        /// <summary>
        /// The meter status
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Left)]
        public string MeterStatus;

        /// <summary>
        /// The comment
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Right)]
        public string Comment;

        /// <summary>
        /// The amr module number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Right)]
        public string AmrModuleNumber;

        /// <summary>
        /// The firmware revision
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        public string FirmwareRevision;

        /// <summary>
        /// The program code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string ProgramCode;

        /// <summary>
        /// The KYZ present
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string KYZPresent;

        /// <summary>
        /// The repair code1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string RepairCode1;

        /// <summary>
        /// The repair code2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string RepairCode2;

        /// <summary>
        /// The repair code3
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string RepairCode3;

        /// <summary>
        /// The not used3
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string NotUsed3;

        /// <summary>
        /// The not used4
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string NotUsed4;

        /// <summary>
        /// The company
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        public string Company;
    }
}
