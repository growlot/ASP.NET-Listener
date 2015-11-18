// <copyright file="Converters.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Core;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800: Do not cast unnecessarily", Justification = "This is method to Convert, we need those casts.")]
        public static object Convert(object data, Type targetType)
        {
            object result = null;

            // If the type is nullable and the result should be null, set a null value.
            if (targetType.IsNullable() && (data == null || data == DBNull.Value))
            {
                result = targetType.GetDefault();
                return result;
            }

            // Convert.ChangeType fails on Nullable<T> types.  We want to try to cast to the underlying type anyway.
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (Conversions.ContainsKey(underlyingType))
            {
                return Conversions[underlyingType](data);
            }

            try
            {
                // Just one edge case we might want to handle.
                if (underlyingType == typeof(Guid))
                {
                    if (data is string)
                    {
                        data = new Guid((string)data);
                    }

                    if (data is byte[])
                    {
                        data = new Guid((byte[])data);
                    }

                    result = System.Convert.ChangeType(data, underlyingType, CultureInfo.InvariantCulture);
                }

                result = System.Convert.ChangeType(data, underlyingType, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Invalid cast from {RawDataType} to {TargetType}", data?.GetType(), targetType);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Converts the specified string to the target type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>System.Object.</returns>
        public static object ConvertFromString(string value, string targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType), "Target type needs to be specified for conversion.");
            }

            switch (targetType.ToUpperInvariant())
            {
                case "INTEGER":
                    return int.Parse(value, CultureInfo.InvariantCulture);
                case "BOOLEAN":
                    return !string.IsNullOrWhiteSpace(value) && (value.ToUpper(CultureInfo.InvariantCulture) == bool.TrueString.ToUpperInvariant() || value == "1");
                case "FLOAT":
                    return float.Parse(value, CultureInfo.InvariantCulture);
                case "STRING":
                    return value;
                default:
                    var converter = ApplicationIntegration.DependencyResolver.ResolveNamed<IStringToTypeConverter>(
                        StringUtilities.Invariant($"convert-value-{targetType}"));
                    return converter.Convert(value);
            }
        }
    }
}