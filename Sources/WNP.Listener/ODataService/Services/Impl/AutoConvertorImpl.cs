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
            {typeof (char), v =>
                {
                    if (v == null)
                    {
                        return null;
                    }

                    if (v is char)
                    {
                        return v;
                    }

                    string stringValue = v as string;
                    if (stringValue != null)
                    {
                        if (stringValue.Length == 1)
                        {
                            return stringValue[0];
                        }
                        else if (string.IsNullOrEmpty(stringValue))
                        {
                            return null;
                        }
                        else
                        {
                            throw new InvalidOperationException("Can not convert string to char, because string is longer than one character.");
                        }
                    }

                    throw new NotImplementedException($"Conversion of {v.GetType()} to char is not implemented.");
                }
            },
            {typeof (double), v => System.Convert.ToDouble(v)},
            {typeof (DateTimeOffset), v =>
                {
                    if (v == null)
                        return null;

                    if (v is DateTime)
                        return new DateTimeOffset((DateTime) v);

                    throw new NotImplementedException($"Conversion of {v.GetType()} to DateTimeOffset is not implemented.");
                }
            }
        };

        public object Convert(object rawData, Type targetType) =>
            Conversions.ContainsKey(targetType) ? Conversions[targetType](rawData) : rawData;
    }
}