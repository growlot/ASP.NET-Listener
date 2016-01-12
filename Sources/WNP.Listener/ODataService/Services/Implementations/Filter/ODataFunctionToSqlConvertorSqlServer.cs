// <copyright file="ODataFunctionToSqlConvertorSqlServer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.OData.Core.UriParser.Semantic;
    using Utilities;

    /// <summary>
    /// Extends <see cref="ODataFunctionToSqlConvertorGeneric"/> with specific implementation for MS SQL server
    /// </summary>
    public class ODataFunctionToSqlConvertorSqlServer : ODataFunctionToSqlConvertorGeneric
    {
        /// <inheritdoc/>
        public override string IndexOf(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments) =>
            GenericFuncCall("CHARINDEX", genericBinder, arguments);

        /// <inheritdoc/>
        // TODO: check what version will be most effective
        // TODO: do we need to split the implementation for each version of SQL Server?
        // $"DATEADD(dd, 0, DATEDIFF(dd, 0, {genericBinder(arguments[0])}))";
        public override string Date(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"CONVERT(VARCHAR(8),{genericBinder(arguments[0])},101)");
        }

        /// <inheritdoc/>
        // TODO: see above
        public override string Time(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"CONVERT(VARCHAR(8),{genericBinder(arguments[0])},108)");
        }

        /// <inheritdoc/>
        public override string Now(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments) =>
            "GETUTCDATE()";

        /// <inheritdoc/>
        // according to https://msdn.microsoft.com/en-us/library/ms187819.aspx
        // maximum value of DateTime datatype is 9999-12-31 23:59:59.997
        public override string MaxDateTime(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments) =>
            "'9999-12-31 23:59:59.997'";

        /// <inheritdoc/>
        // according to https://msdn.microsoft.com/en-us/library/ms187819.aspx
        // minimum value of DateTime datatype is 1753-01-01 00:00:00
        public override string MinDateTime(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments) =>
            "'1753-01-01 00:00:00'";

        /// <inheritdoc/>
        public override string Month(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"DATEPART(MONTH,{genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Day(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"DATEPART(DAY,{genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Hour(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"DATEPART(HOUR,{genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Minute(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"DATEPART(MINUTE,{genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Second(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"DATEPART(SECOND,{genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Length(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 1)
            {
                throw new ArgumentException("Contains function needs at least one argument.", nameof(arguments));
            }

            return StringUtilities.Invariant($"LEN({genericBinder(arguments[0])})");
        }

        /// <inheritdoc/>
        public override string Substring(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments)
        {
            if (genericBinder == null)
            {
                throw new ArgumentNullException(nameof(genericBinder), "Generic binder must be specified.");
            }

            if (arguments == null || arguments.Count() < 2)
            {
                throw new ArgumentException("Contains function needs at least two arguments.", nameof(arguments));
            }

            return StringUtilities.Invariant($"SUBSTRING({genericBinder(arguments[0])},{genericBinder(arguments[1])})");
        }
    }
}