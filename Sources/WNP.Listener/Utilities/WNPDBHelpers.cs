// <copyright file="WNPDBHelpers.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Various helppers related to WNP database naming.
    /// </summary>
    public static class WNPDBHelpers
    {
        private static Regex rxCleanUp = new Regex(@"[^\w\d_]", RegexOptions.Compiled);

        private static string[] csKeywords =
        {
            "abstract", "event", "new", "struct", "as", "explicit", "null",
            "switch", "base", "extern", "object", "this", "bool", "false", "operator", "throw",
            "break", "finally", "out", "true", "byte", "fixed", "override", "try", "case", "float",
            "params", "typeof", "catch", "for", "private", "uint", "char", "foreach", "protected",
            "ulong", "checked", "goto", "public", "unchecked", "class", "if", "readonly", "unsafe",
            "const", "implicit", "ref", "ushort", "continue", "in", "return", "using", "decimal",
            "int", "sbyte", "virtual", "default", "interface", "sealed", "volatile", "delegate",
            "internal", "short", "void", "do", "is", "sizeof", "while", "double", "lock",
            "stackalloc", "else", "long", "static", "enum", "namespace", "string"
        };

        private static Func<string, string> cleanUp = (str) =>
        {
            str = rxCleanUp.Replace(str, "_");

            if (char.IsDigit(str[0]) || csKeywords.Contains(str))
            {
                str = "@" + str;
            }

            return str;
        };

        /// <summary>
        /// Converts database table names to C# code friendly names. (Same logic as in generated data model)
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <returns>The normalized table name.</returns>
        public static string HumanizeTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName), "Table name must be provided.");
            }

            if (tableName.StartsWith("wndba.", StringComparison.InvariantCultureIgnoreCase))
            {
                tableName = tableName.Substring(6);
            }

            if (tableName.StartsWith("T", StringComparison.InvariantCultureIgnoreCase) &&
                !tableName.StartsWith("Transaction", StringComparison.InvariantCultureIgnoreCase))
            {
                tableName = tableName.Substring(1);
            }

            return Humanize(tableName) + "Entity";
        }

        /// <summary>
        /// Converts database table field names to C# code friendly names. (Same logic as in generated data model)
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <returns>The normalized field name.</returns>
        public static string HumanizeField(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                throw new ArgumentNullException(nameof(fieldName), "Table field name must be provided.");
            }

            return Humanize(fieldName);
        }

        /// <summary>
        /// Converts database table names and column names to C# code friendly names. (Same logic as in generated data model)
        /// </summary>
        /// <param name="name">The original name of field or table.</param>
        /// <returns>The normalized table or field name.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "This is human readable name formatting function and name parts must be converted to lower case to follow Hungarian notations.")]
        private static string Humanize(string name)
        {
            if (name.IndexOf("_", StringComparison.Ordinal) != -1)
            {
                var tableWords = name.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(word => word.Substring(0, 1).ToUpperInvariant() + word.Substring(1).ToLowerInvariant());

                return cleanUp(string.Join(string.Empty, tableWords));
            }

            var firstLetter = name.Substring(0, 1).ToUpperInvariant();
            var rest = name.Substring(1);

            if (rest.All(char.IsUpper))
            {
                rest = rest.ToLowerInvariant();
            }

            name = firstLetter + rest;

            return cleanUp(name);
        }
    }
}
