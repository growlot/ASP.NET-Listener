// <copyright file="BinaryOperatorKindExtensions.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
    /// <summary>
    /// Defines custom extensions for <see cref="BinaryOperatorKind"/> type
    /// </summary>
    public static class BinaryOperatorKindExtensions
    {
        /// <summary>
        /// Converts <see cref="BinaryOperatorKind"/> from Edm operator to SQL operator.
        /// </summary>
        /// <param name="binaryOperator">The binary operator.</param>
        /// <returns>The binary operator for use in SQL construction.</returns>
        public static string ToSqlOperator(this BinaryOperatorKind binaryOperator)
        {
            switch (binaryOperator)
            {
                case BinaryOperatorKind.Add:
                    return "+";
                case BinaryOperatorKind.And:
                    return "AND";
                case BinaryOperatorKind.Divide:
                    return "/";
                case BinaryOperatorKind.Equal:
                    return "=";
                case BinaryOperatorKind.GreaterThan:
                    return ">";
                case BinaryOperatorKind.GreaterThanOrEqual:
                    return ">=";
                case BinaryOperatorKind.LessThan:
                    return "<";
                case BinaryOperatorKind.LessThanOrEqual:
                    return "<=";
                case BinaryOperatorKind.Modulo:
                    return "%";
                case BinaryOperatorKind.Multiply:
                    return "*";
                case BinaryOperatorKind.NotEqual:
                    return "!=";
                case BinaryOperatorKind.Or:
                    return "OR";
                case BinaryOperatorKind.Subtract:
                    return "-";
                default:
                    return null;
            }
        }
    }
}