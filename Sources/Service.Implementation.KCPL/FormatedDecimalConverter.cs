using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FileHelpers;

namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    [CLSCompliant(false)]
    public class FormattedDecimalConverter : ConverterBase
    {
        private string format;

        public FormattedDecimalConverter()
        {
        }

        public FormattedDecimalConverter(string format)
        {
            this.format = format;
        }

        /// <summary>
        /// Strings to field.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public override object StringToField(string from)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from", "Can not convert string to decimal if string is not provided.");
            }

            decimal res;
            if (
                !decimal.TryParse(from.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out res))
                throw new ConvertException(from, typeof(decimal));
            return res;
        }

        /// <summary>
        /// Fields to string.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public override string FieldToString(object from)
        {
            return ((decimal)from).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
