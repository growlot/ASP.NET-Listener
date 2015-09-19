using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm.Library;

namespace AMSLLC.Listener.Utilities
{
    public static class EdmExtensions
    {
        public static object ToKnownClrType(this ConstantNode constNode)
        {
            if (constNode.Value is Date)
            {
                var edmDate = ((Date)constNode.Value);
                return new DateTime(edmDate.Year, edmDate.Month, edmDate.Day);
            }

            return constNode.Value;
        }

        public static string ToSqlOperator(this BinaryOperatorKind binaryOpertor)
        {
            switch (binaryOpertor)
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