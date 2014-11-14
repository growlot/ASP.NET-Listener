//-----------------------------------------------------------------------
// <copyright file="FormattedDecimalConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Globalization;
    using FileHelpers;

    /// <summary>
    /// FileHelpers converter that converts decimal to string using specified format 
    /// </summary>
    [CLSCompliant(false)]
    public class FormattedDecimalConverter : ConverterBase
    {
        /// <summary>
        /// The format used to convert decimal to string.
        /// </summary>
        private string format;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedDecimalConverter"/> class.
        /// </summary>
        public FormattedDecimalConverter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedDecimalConverter"/> class.
        /// </summary>
        /// <param name="format">The format used to convert decimal to string.</param>
        public FormattedDecimalConverter(string format)
        {
            this.format = format;
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
            if (from == null)
            {
                throw new ArgumentNullException("from", "Can not convert string to decimal if string is not provided.");
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
            return ((decimal)from).ToString(this.format, CultureInfo.InvariantCulture);
        }
    }
}
