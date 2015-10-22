// <copyright file="UnaryOperatorKindExtensions.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
    /// <summary>
    /// Defines custom extensions for <see cref="UnaryOperatorKind"/> type
    /// </summary>
    public static class UnaryOperatorKindExtensions
    {
        /// <summary>
        /// Converts <see cref="UnaryOperatorKind"/> from Edm operator to SQL operator.
        /// </summary>
        /// <param name="unaryOperator">The unary operator.</param>
        /// <returns>The unary operator for use in SQL construction.</returns>
        public static string ToSqlOperator(this UnaryOperatorKind unaryOperator)
        {
            switch (unaryOperator)
            {
                case UnaryOperatorKind.Negate:
                    return "!";
                case UnaryOperatorKind.Not:
                    return "NOT";
                default:
                    return null;
            }
        }
    }
}