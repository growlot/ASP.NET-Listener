using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.OData.Core.UriParser.Semantic;
using WNP.Listener.ODataService.Services.FilterTransformer;

namespace WNP.Listener.ODataService.Services.Impl.FilterTransformer
{
    public abstract class ODataFunctionToSqlConvertorGenericImpl : IODataFunctionToSqlConvertor, ISupportedODataFunctionToSql
    {
        public bool IsSupported(string functionName) =>
            GetMappedMethod(functionName) != null;

        public string Convert(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            var method = GetMappedMethod(functionName);

            // should never happen because IsSupported check should be made
            // prior to each Convert call
            if (method == null)
                throw new ArgumentException($"{functionName} not found in implementation class.");

            return (string) method.Invoke(this, new object[] {genericBinder, arguments});
        }

        private MethodInfo GetMappedMethod(string functionName) =>
            GetType()
                .GetMethod(functionName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);

        protected string GenericFuncCall(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            $"{functionName}({arguments.Select(genericBinder).Aggregate((a, b) => a + ',' + b)})";

        public string ToLower(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("LOWER", genericBinder, arguments);

        public string ToUpper(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("UPPER", genericBinder, arguments);

        public string Concat(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("CONCAT", genericBinder, arguments);

        public string Ceiling(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("CEILING", genericBinder, arguments);

        public string Floor(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("FLOOR", genericBinder, arguments);

        public string Round(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            GenericFuncCall("ROUND", genericBinder, arguments);

        public string Trim(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            $"RTRIM(LTRIM({genericBinder(arguments[0])}))";

        public string Contains(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode("%" + ((ConstantNode)arguments[1]).Value + "%");
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        public string EndsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode("%" + ((ConstantNode)arguments[1]).Value);
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        public string StartsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode(((ConstantNode)arguments[1]).Value + "%");
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        public abstract string IndexOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Date(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Time(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Now(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string MaxDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string MinDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Length(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Month(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Day(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Hour(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Minute(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Second(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        public abstract string Substring(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        // TODO: not exactly sure how to implement this and it seems not really relevant for now. Alexei.
        public string TotalOffsetMinutes(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException();
        }

        // TODO: the OData spec currently is ambiguous on this function use. Alexei.
        // Part 1 does not even have definition for it, so skipping for now
        public string TotalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException();
        }

        public string FractionalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("FractionalSeconds is not yet implemented");
        }

        public string IsOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("IsOf is not yet implemented");
        }

        public string Cast(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("Cast is not yet implemented");
        }

        public string GeoDistance(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoDistance is not yet implemented");
        }

        public string GeoIntersects(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoIntersects is not yet implemented");
        }

        public string GeoLength(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoLength is not yet implemented");
        }
    }
}