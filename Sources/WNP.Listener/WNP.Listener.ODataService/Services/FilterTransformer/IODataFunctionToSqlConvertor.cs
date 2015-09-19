using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.Semantic;

namespace WNP.Listener.ODataService.Services.FilterTransformer
{
    public interface IODataFunctionToSqlConvertor
    {
        bool IsSupported(string functionName);
        string Convert(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
    }
}