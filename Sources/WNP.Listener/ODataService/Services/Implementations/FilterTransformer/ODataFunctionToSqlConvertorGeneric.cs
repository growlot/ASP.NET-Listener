// <copyright file="ODataFunctionToSqlConvertorGeneric.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.FilterTransformer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.OData.Core.UriParser.Semantic;
    using Services.FilterTransformer;

    /// <summary>
    /// Implements convertion from OData functions to SQL for all database implementations.
    /// Specific database implementation derives from this class and overrides specific functions if needed.
    /// </summary>
    public abstract class ODataFunctionToSqlConvertorGeneric : IODataFunctionToSqlConvertor, ISupportedODataFunctionToSql
    {
        /// <inheritdoc/>
        public bool IsSupported(string functionName) =>
            this.GetMappedMethod(functionName) != null;

        /// <inheritdoc/>
        public string Convert(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            var method = this.GetMappedMethod(functionName);

            // should never happen because IsSupported check should be made
            // prior to each Convert call
            if (method == null)
            {
                throw new ArgumentException($"{functionName} not found in implementation class.");
            }

            return (string)method.Invoke(this, new object[] { genericBinder, arguments });
        }

        /// <inheritdoc/>
        public string ToLower(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("LOWER", genericBinder, arguments);

        /// <inheritdoc/>
        public string ToUpper(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("UPPER", genericBinder, arguments);

        /// <inheritdoc/>
        public string Concat(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("CONCAT", genericBinder, arguments);

        /// <inheritdoc/>
        public string Ceiling(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("CEILING", genericBinder, arguments);

        /// <inheritdoc/>
        public string Floor(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("FLOOR", genericBinder, arguments);

        /// <inheritdoc/>
        public string Round(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            this.GenericFuncCall("ROUND", genericBinder, arguments);

        /// <inheritdoc/>
        public string Trim(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            $"RTRIM(LTRIM({genericBinder(arguments[0])}))";

        /// <inheritdoc/>
        public string Contains(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode("%" + ((ConstantNode)arguments[1]).Value + "%");
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        /// <inheritdoc/>
        public string EndsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode("%" + ((ConstantNode)arguments[1]).Value);
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        /// <inheritdoc/>
        public string StartsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // the arguments[1] will always be a ConstantNode as per OData spec,
            // so no need to check here
            var sqlVal = new ConstantNode(((ConstantNode)arguments[1]).Value + "%");
            return $"{genericBinder(arguments[0])} LIKE {genericBinder(sqlVal)}";
        }

        /// <inheritdoc/>
        public abstract string IndexOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Date(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Time(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Now(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string MaxDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string MinDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Length(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Month(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Day(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Hour(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Minute(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Second(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public abstract string Substring(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        /// <inheritdoc/>
        public string TotalOffsetMinutes(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // TODO: not exactly sure how to implement this and it seems not really relevant for now. Alexei.
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string TotalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            // TODO: the OData spec currently is ambiguous on this function use. Alexei.
            // Part 1 does not even have definition for it, so skipping for now
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public string FractionalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("FractionalSeconds is not yet implemented");
        }

        /// <inheritdoc/>
        public string IsOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("IsOf is not yet implemented");
        }

        /// <inheritdoc/>
        public string Cast(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("Cast is not yet implemented");
        }

        /// <inheritdoc/>
        public string GeoDistance(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoDistance is not yet implemented");
        }

        /// <inheritdoc/>
        public string GeoIntersects(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoIntersects is not yet implemented");
        }

        /// <inheritdoc/>
        public string GeoLength(Func<QueryNode, string> genericBinder, List<QueryNode> arguments)
        {
            throw new NotImplementedException("GeoLength is not yet implemented");
        }

        /// <summary>
        /// Generics the function call.
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The result string</returns>
        protected string GenericFuncCall(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
                    $"{functionName}({arguments.Select(genericBinder).Aggregate((a, b) => a + ',' + b)})";

        private MethodInfo GetMappedMethod(string functionName) =>
            this.GetType()
                .GetMethod(functionName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
    }
}