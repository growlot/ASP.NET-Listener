using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;

namespace AMSLLC.Listener.ODataService.Services.Impl.FilterTransformer
{
    public class ODataFunctionToSqlConvertorSqlServerImpl : ODataFunctionToSqlConvertorGenericImpl
    {
        public override string IndexOf(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            GenericFuncCall("CHARINDEX", genericBinder, arguments);

        // TODO: check what version will be most effective
        // TODO: do we need to split the implementation for each version of SQL Server?
        //$"DATEADD(dd, 0, DATEDIFF(dd, 0, {genericBinder(arguments[0])}))";
        public override string Date(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) =>
            $"CONVERT(VARCHAR(8),{genericBinder(arguments[0])},101)";

        // TODO: see above
        public override string Time(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"CONVERT(VARCHAR(8),{genericBinder(arguments[0])},108)";            

        public override string Now(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            "GETUTCDATE()";

        // according to https://msdn.microsoft.com/en-us/library/ms187819.aspx
        // maximum value of DateTime datatype is 9999-12-31 23:59:59.997
        public override string MaxDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            "'9999-12-31 23:59:59.997'";

        // according to https://msdn.microsoft.com/en-us/library/ms187819.aspx
        // maximum value of DateTime datatype is 1753-01-01 00:00:00
        public override string MinDateTime(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            "'1753-01-01 00:00:00'";

        public override string Month(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"DATEPART(MONTH,{genericBinder(arguments[0])})";

        public override string Day(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"DATEPART(DAY,{genericBinder(arguments[0])})";

        public override string Hour(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"DATEPART(HOUR,{genericBinder(arguments[0])})";

        public override string Minute(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"DATEPART(MINUTE,{genericBinder(arguments[0])})";

        public override string Second(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"DATEPART(SECOND,{genericBinder(arguments[0])})";

        public override string Length(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"LEN({genericBinder(arguments[0])})";

        public override string Substring(Func<QueryNode, string> genericBinder, List<QueryNode> arguments) => 
            $"SUBSTRING({genericBinder(arguments[0])},{genericBinder(arguments[1])})";
    }
}