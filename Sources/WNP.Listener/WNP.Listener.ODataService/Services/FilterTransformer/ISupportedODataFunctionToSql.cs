using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;

namespace WNP.Listener.ODataService.Services.FilterTransformer
{
    public interface ISupportedODataFunctionToSql
    {
        string ToLower(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string ToUpper(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Concat(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Ceiling(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Floor(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Round(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Contains(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string EndsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string StartsWith(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string IndexOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Date(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Time(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string TotalOffsetMinutes(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Now(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string MaxDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string MinDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string TotalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Length(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Trim(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Month(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Day(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Hour(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Minute(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Second(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Substring(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);

        // TODO: most possibly we do not need to implment the following methods
        string FractionalSeconds(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string IsOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string Cast(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string GeoDistance(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string GeoIntersects(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
        string GeoLength(Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
    }
}