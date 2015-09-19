using System;
using System.Collections.Generic;

namespace WNP.Listener.ODataService.Services.Impl
{
    public class AutoConvertorImpl : IAutoConvertor
    {
        private static readonly Dictionary<Type, Func<object, object>> Conversions = new Dictionary<Type, Func<object, object>>
        {
            {typeof (decimal), v => System.Convert.ToDecimal(v)},
            {typeof (float), v => System.Convert.ToSingle(v)},
            {typeof (double), v => System.Convert.ToDouble(v)}
        };

        public object Convert(object rawData, Type targetType) =>
            Conversions.ContainsKey(targetType) ? Conversions[targetType](rawData) : rawData;
    }
}