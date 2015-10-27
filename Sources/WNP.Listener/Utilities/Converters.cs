// <copyright file="Converters.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Serilog;

    /// <summary>
    /// Various data and type converters
    /// </summary>
    public static class Converters
    {
        private static readonly Dictionary<Type, Func<object, object>> Conversions = new Dictionary<Type, Func<object, object>>
        {
            { typeof(decimal), v => System.Convert.ToDecimal(v, CultureInfo.InvariantCulture) },
            { typeof(float), v => System.Convert.ToSingle(v, CultureInfo.InvariantCulture) },
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

                    throw new NotImplementedException(StringUtilities.Invariant($"Conversion of {v.GetType()} to char is not implemented."));
                }
            },
            { typeof(double), v => System.Convert.ToDouble(v, CultureInfo.InvariantCulture) },
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

                    throw new NotImplementedException(StringUtilities.Invariant($"Conversion of {v.GetType()} to DateTimeOffset is not implemented."));
                }
            }
        };

        /// <summary>
        /// Converts the specified data to target type.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>The data as target type.</returns>
        public static object Convert(object data, Type targetType)
        {
            if (Conversions.ContainsKey(targetType))
            {
                return Conversions[targetType](data);
            }
            else
            {
                try
                {
                    return System.Convert.ChangeType(data, targetType, CultureInfo.InvariantCulture);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Invalid cast from {RawDataType} to {TargetType}", data?.GetType(), targetType);
                    throw;
                }
            }
        }
    }
}