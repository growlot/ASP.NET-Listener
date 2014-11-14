//-----------------------------------------------------------------------
// <copyright file="GmoCisFile.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// FileHelpers definition of GMO cis export file.
    /// </summary>
    [FixedLengthRecord]
    public class GmoCisFile
    {
        /// <summary>
        /// The manufacturer
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string Manufacturer;

        /// <summary>
        /// The not used1
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed1;

        /// <summary>
        /// The meter serial number
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(17)]
        [FieldTrim(TrimMode.Right)]
        [FieldConverter(typeof(MeterSerialNumberConverter))]
        public string MeterSerialNumber;

        /// <summary>
        /// The NWH
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(3)]
        public string NWH;

        /// <summary>
        /// The date tested
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "yyMMdd")]
        public DateTime DateTested;

        /// <summary>
        /// The not used2
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(12)]
        public string NotUsed2;

        /// <summary>
        /// The tester identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Right)]
        public string TesterId;

        /// <summary>
        /// The not used3
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(10)]
        public string NotUsed3;

        /// <summary>
        /// As found read
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Right)]
        public string AsFoundRead;

        /// <summary>
        /// The not used4
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(30)]
        public string NotUsed4;

        /// <summary>
        /// The meter type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Right)]
        //// TODO: might need to move it forward by 2
        public string MeterType;

        /// <summary>
        /// The phase
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldConverter(ConverterKind.Int32)]
        public int Phase;

        /// <summary>
        /// The not used5
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(14)]
        public string NotUsed5;

        /// <summary>
        /// The wires
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        [FieldConverter(ConverterKind.Int32)]
        public int Wires;

        /// <summary>
        /// The not used6
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(14)]
        public string NotUsed6;

        /// <summary>
        /// The class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(3)]
        public string Class;

        /// <summary>
        /// The not used7
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(12)]
        public string NotUsed7;

        /// <summary>
        /// The register ratio
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Right)]
        public string RegisterRatio;

        /// <summary>
        /// The register type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        [FieldTrim(TrimMode.Right)]
        public string RegisterType;

        /// <summary>
        /// The not used8
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(65)]
        public string NotUsed8;

        /// <summary>
        /// The barcode identifier
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(10)]
        [FieldTrim(TrimMode.Right)]
        public string BarcodeId;

        /// <summary>
        /// The comments
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(30)]
        [FieldTrim(TrimMode.Right)]
        public string Comments;

        /// <summary>
        /// The repair code
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        public string RepairCode;

        /// <summary>
        /// The not used9
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(114)]
        public string NotUsed9;

        /// <summary>
        /// The test type
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string TestType;

        /// <summary>
        /// The form
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string Form;

        /// <summary>
        /// The base
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string Base;

        /// <summary>
        /// The KH
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(8)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00000")]
        public decimal KH;

        /// <summary>
        /// The volts
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(5)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.0")]
        public decimal Volts;

        /// <summary>
        /// The not used10
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed10;

        /// <summary>
        /// The amps
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(5)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal Amps;

        /// <summary>
        /// The not used11
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(139)]
        public string NotUsed11;

        /// <summary>
        /// As found full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundFullLoad;

        /// <summary>
        /// The not used12
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed12;

        /// <summary>
        /// As found power factor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundPowerFactor;

        /// <summary>
        /// The not used13
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed13;

        /// <summary>
        /// As found light load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundLightLoad;

        /// <summary>
        /// The not used14
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed14;

        /// <summary>
        /// As found a full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundAFullLoad;

        /// <summary>
        /// The not used15
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed15;

        /// <summary>
        /// As found b full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundBFullLoad;

        /// <summary>
        /// The not used16
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed16;

        /// <summary>
        /// As found c full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundCFullLoad;

        /// <summary>
        /// The not used17
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed17;

        /// <summary>
        /// As left full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftFullLoad;

        /// <summary>
        /// The not used18
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed18;

        /// <summary>
        /// As left power factor
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftPowerFactor;

        /// <summary>
        /// The not used19
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed19;

        /// <summary>
        /// As left light load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftLightLoad;

        /// <summary>
        /// The not used20
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed20;

        /// <summary>
        /// As left a full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftAFullLoad;

        /// <summary>
        /// The not used21
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed21;

        /// <summary>
        /// As left b full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftBFullLoad;

        /// <summary>
        /// The not used22
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed22;

        /// <summary>
        /// As left c full load
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftCFullLoad;

        /// <summary>
        /// The not used23
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(15)]
        public string NotUsed23;

        /// <summary>
        /// As found weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsFoundWeightedAverage;

        /// <summary>
        /// The not used24
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(1)]
        public string NotUsed24;

        /// <summary>
        /// As left weighted average
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(6)]
        [FieldConverter(typeof(FormattedDecimalConverter), "0.00")]
        public decimal AsLeftWeightedAverage;

        /// <summary>
        /// The not used25
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Justification = "Required by FileHelpers library")]
        [FieldFixedLength(121)]
        public string NotUsed25;

        /// <summary>
        /// FileHelpers converter that converts meter serial number to format specific for this export file
        /// </summary>
        internal class MeterSerialNumberConverter : ConverterBase
        {
            /// <summary>
            /// Converts meter number string to WNP format, by removing spaces and trimming 0 at the start.
            /// </summary>
            /// <param name="from">The meter number string.</param>
            /// <returns>
            /// The Field value as string.
            /// </returns>
            /// <exception cref="FileHelpers.Events.AfterWriteEventArgs`1">See FileHelpers for exception details.</exception>
            public override object StringToField(string from)
            {
                if (from == null)
                {
                    throw new ArgumentNullException("from", "Can not convert string to Meter Serial Number if string is not provided.");
                }

                from = from.Replace(" ", string.Empty);
                from = from.TrimStart('0');
                from = from.TrimEnd();
                return from;
            }

            /// <summary>
            /// Converts meter number string to correct format. Three number triplets padded with 0 on the left (xxx xxx xxx).
            /// </summary>
            /// <param name="from">The meter number string.</param>
            /// <returns>
            /// The string representing the field value.
            /// </returns>
            public override string FieldToString(object from)
            {
                string result = base.FieldToString(from);
                result = result.PadLeft(9, '0');
                result = result.Insert(3, " ");
                result = result.Insert(7, " ");
                return result;
            }
        }
    }
}
