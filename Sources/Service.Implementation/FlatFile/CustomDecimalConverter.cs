//-----------------------------------------------------------------------
// <copyright file="CustomDecimalConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.FlatFile
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// FileHelpers converter that converts decimal to string using specified separator, format and null value
    /// </summary>
    [CLSCompliant(false)]
    public class CustomDecimalConverter : ConverterBase
    {
        /// <summary>
        /// Culture information based on the separator
        /// </summary>
        private CultureInfo culture;

        /// <summary>
        /// The format used to convert decimal to string.
        /// </summary>
        private string format;

        /// <summary>
        /// The null value used instead of null
        /// </summary>
        private string nullValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDecimalConverter"/> class.
        /// </summary>
        public CustomDecimalConverter()
        {
            this.culture = CreateCulture(".");
            this.format = "G";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDecimalConverter" /> class.
        /// </summary>
        /// <param name="decimalSeparator">The decimal separator.</param>
        public CustomDecimalConverter(string decimalSeparator)
        {
            this.culture = CreateCulture(decimalSeparator);
            this.format = "G";
            this.nullValue = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDecimalConverter" /> class.
        /// </summary>
        /// <param name="decimalSeparator">The decimal separator.</param>
        /// <param name="format">The decimal format.</param>
        public CustomDecimalConverter(string decimalSeparator, string format)
        {
            this.culture = CreateCulture(decimalSeparator);
            this.format = format;
            this.nullValue = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomDecimalConverter" /> class.
        /// </summary>
        /// <param name="decimalSeparator">The decimal separator.</param>
        /// <param name="format">The decimal format.</param>
        /// <param name="nullValue">The null value.</param>
        public CustomDecimalConverter(string decimalSeparator, string format, string nullValue)
        {
            this.culture = CreateCulture(decimalSeparator);
            this.format = format;
            this.nullValue = nullValue;
        }

        /// <summary>
        /// Converts decimal field to string.
        /// </summary>
        /// <param name="from">The decimal field object.</param>
        /// <returns>
        /// The string representing the field value.
        /// </returns>
        public override string FieldToString(object from)
        {
            if (from == null)
            {
                return this.nullValue;
            }

            return ((decimal)from).ToString(this.format, this.culture);
        }

        /// <summary>
        /// Convert a string in the file to a field value.
        /// </summary>
        /// <param name="from">The string to convert.</param>
        /// <returns>
        /// The Field value.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">from;Can not convert string to decimal if string is not provided.</exception>
        /// <exception cref="FileHelpers.ConvertException"></exception>
        public override object StringToField(string from)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from", "Can not convert string to decimal if string is not provided.");
            }

            decimal res;
            if (!decimal.TryParse(from.Trim(), NumberStyles.Number, this.culture, out res))
            {
                throw new ConvertException(from, typeof(decimal));
            }

            return res;
        }

        /// <summary>
        /// Return culture information for with comma decimal separator or comma decimal separator
        /// </summary>
        /// <param name="decimalSep">Decimal separator string</param>
        /// <returns>
        /// Cultural information based on separator
        /// </returns>
        private static CultureInfo CreateCulture(string decimalSep)
        {
            CultureInfo cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.LCID);
            if (decimalSep == ".")
            {
                cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
                cultureInfo.NumberFormat.NumberGroupSeparator = ",";
            }
            else
            {
                if (!(decimalSep == ","))
                {
                    throw new ArgumentException("You can only use '.' or ',' as decimal or group separators");
                }

                cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
                cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            }

            return cultureInfo;
        }
    }
}
