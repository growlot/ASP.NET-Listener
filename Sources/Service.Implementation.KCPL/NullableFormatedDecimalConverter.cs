using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FileHelpers;

namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    [CLSCompliant(false)]
    public class NullableFormattedDecimalConverter : ConverterBase
    {
        private string format;
        private string nullValue;

        public NullableFormattedDecimalConverter()
        {
        }

        public NullableFormattedDecimalConverter(string format, string nullValue)
        {
            this.format = format;
            this.nullValue = nullValue;
        }

        /// <summary>
        /// Strings to field.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public override object StringToField(string from)
        {
            if (string.IsNullOrEmpty(from))
            {
                return (decimal)0;
            }

            decimal res;
            if (!decimal.TryParse(from.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out res))
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
            if ((decimal)from < (decimal)0.00001)
            {
                return nullValue;
            }

            return ((decimal)from).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
