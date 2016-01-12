// <copyright file="ISupportedODataFunctionToSql.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Filter
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OData.Core.UriParser.Semantic;

    /// <summary>
    /// Interface defining OData functions that can be converted to SQL.
    /// </summary>
    public interface ISupportedODataFunctionToSql
    {
        /// <summary>
        /// To the lower.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string ToLower(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// To the upper.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string ToUpper(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Concatenate.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Concat(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Ceiling.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Ceiling(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Floor.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Floor(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Round.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Round(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Contains.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Contains(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Ends with.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string EndsWith(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Starts with.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string StartsWith(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Indes of.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string IndexOf(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Date.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Date", Justification = "Reflection is used to resolve metod names based on OData functions, so it can not be changed.")]
        string Date(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Time.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Time(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Total offset in minutes.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string TotalOffsetMinutes(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Now.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Now(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Max date time.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string MaxDateTime(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Min date time.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string MinDateTime(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Total seconds.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string TotalSeconds(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Length.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Length(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Trim.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Trim(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Month.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Month(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Day.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Day(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Hour.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Hour(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Minute.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Minute(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Second.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Second(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Substring.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Substring(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        // TODO: most possibly we do not need to implment the following methods

        /// <summary>
        /// Fractional seconds.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string FractionalSeconds(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Is of.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string IsOf(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Cast.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string Cast(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// GeoDistance.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string GeoDistance(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Geo intersects.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string GeoIntersects(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);

        /// <summary>
        /// Geo length.
        /// </summary>
        /// <param name="genericBinder">The generic binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The SQL function as a string.</returns>
        string GeoLength(Func<QueryNode, string> genericBinder, IList<QueryNode> arguments);
    }
}