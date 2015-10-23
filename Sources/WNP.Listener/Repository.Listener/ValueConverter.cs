// <copyright file="ValueConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository.Listener
{
    using System.Globalization;
    using Core;
    using Utilities;

    /// <summary>
    /// Provides various value converters
    /// </summary>
    internal static class ValueConverter
    {
        /// <summary>
        /// Converts the specified string to the target type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>System.Object.</returns>
        public static object Convert(string value, string targetType)
        {
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
