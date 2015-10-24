// <copyright file="AutoConvertorImpl.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl
{
    using System;
    using System.Collections.Generic;
    using Serilog;

    public class AutoConvertorImpl : IAutoConvertor
    {
        private static readonly Dictionary<Type, Func<object, object>> Conversions = new Dictionary<Type, Func<object, object>>
        {
            { typeof(decimal), v => System.Convert.ToDecimal(v) },
            { typeof(float), v => System.Convert.ToSingle(v) },
            { typeof(char), v =>
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
            { typeof(double), v => System.Convert.ToDouble(v) },
            { typeof(DateTimeOffset), v =>
                {
                    if (v == null)
                    {
                        return null;
                    }

                    if (v is DateTime)
                    {
                        DateTime localTime = (DateTime)v;
                        DateTimeOffset result = new DateTimeOffset(localTime.ToUniversalTime());
                        return result;
                    }

                    throw new NotImplementedException($"Conversion of {v.GetType()} to DateTimeOffset is not implemented.");
                }
            }
        };

        /// <inheritdoc/>
        public object Convert(object rawData, Type targetType)
        {
            if (Conversions.ContainsKey(targetType))
            {
                return Conversions[targetType](rawData);
            }
            else
            {
                try
                {
                    return System.Convert.ChangeType(rawData, targetType);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Invalid cast from {RawDataType} to {TargetType}", rawData?.GetType(), targetType);
                }
            }

            return rawData;
        }
    }
}