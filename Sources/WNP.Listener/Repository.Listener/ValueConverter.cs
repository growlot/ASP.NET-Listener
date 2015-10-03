﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Repository.Listener
{
    using Core;

    internal static class ValueConverter
    {
        /// <summary>
        /// Converts the specified value to the target type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>System.Object.</returns>
        public static object Convert(string value, string targetType)
        {
            switch (targetType.ToUpperInvariant())
            {
                case "integer":
                    return int.Parse(value);
                case "boolean":
                    return !string.IsNullOrWhiteSpace(value) && (value.ToUpper() == bool.TrueString.ToUpperInvariant() || value == "1");
                case "float":
                    return float.Parse(value);
                case "string":
                    return value;
                default:
                    var converter = ApplicationIntegration.DependencyResolver.ResolveNamed<IValueTypeConverter>(
                        $"convert-value-{targetType}");
                    return converter.Convert(value);
            }
        }
    }
}
