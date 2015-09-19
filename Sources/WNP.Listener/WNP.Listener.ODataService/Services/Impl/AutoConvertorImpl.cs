using System;
using System.Collections.Generic;

namespace AMSLLC.Listener.ODataService.Services.Impl
{
    public class AutoConvertorImpl : IAutoConvertor
    {
        private static readonly Dictionary<Type, Func<object, object>> Conversions = new Dictionary<Type, Func<object, object>>
        {
            {typeof (decimal), v => System.Convert.ToDecimal(v)},
            {typeof (float), v => System.Convert.ToSingle(v)},
            {typeof (double), v => System.Convert.ToDouble(v)},
            {typeof (DateTimeOffset), v =>
            {
                if (v == null)
                    return null;

                if (v is DateTime)
                    return new DateTimeOffset((DateTime) v);

                throw new NotImplementedException($"Conversion of {v.GetType()} to DateTimeOffset is not implemented.");
            } }
        };

        public object Convert(object rawData, Type targetType) =>
            Conversions.ContainsKey(targetType) ? Conversions[targetType](rawData) : rawData;
    }
}