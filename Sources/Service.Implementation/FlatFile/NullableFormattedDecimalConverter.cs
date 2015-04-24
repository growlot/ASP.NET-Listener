//-----------------------------------------------------------------------
// <copyright file="NullableFormattedDecimalConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.FlatFile
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// FileHelpers converter that converts decimal to string using specified format 
    /// and allows to specify specific string value in case decimal is 0 (null)
    /// </summary>
    [CLSCompliant(false)]
    public class NullableFormattedDecimalConverter : ConverterBase
    {
        /// <summary>
        /// The format used to convert decimal to string.
        /// </summary>
        private string format;

        /// <summary>
        /// The string representing null value.
        /// </summary>
        private string nullValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableFormattedDecimalConverter"/> class.
        /// </summary>
        public NullableFormattedDecimalConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NullableFormattedDecimalConverter"/> class.
        /// </summary>
        /// <param name="format">The format used to convert decimal to string.</param>
        /// <param name="nullValue">The string used in case when decimal is 0 (null).</param>
        public NullableFormattedDecimalConverter(string format, string nullValue)
        {
            this.format = format;
            this.nullValue = nullValue;
        }

        /// <summary>
        /// Converts field string value to decimal.
        /// </summary>
        /// <param name="from">String value.</param>
        /// <returns>
        /// The Field value as decimal.
        /// </returns>
        /// <exception cref="FileHelpers.Events.AfterWriteEventArgs`1">See FileHelpers for exception details.</exception>
        public override object StringToField(string from)
        {
            if (string.IsNullOrEmpty(from))
            {
                return (decimal)0;
            }

            decimal res;
            if (!decimal.TryParse(from.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out res))
            {
                throw new ConvertException(from, typeof(decimal));
            }
            
            return res;
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
            if ((decimal)from < (decimal)0.00001)
            {
                return this.nullValue;
            }

            return ((decimal)from).ToString(this.format, CultureInfo.InvariantCulture);
        }
    }
}
