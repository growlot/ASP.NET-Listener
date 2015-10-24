// <copyright file="IODataFunctionToSqlConvertor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.FilterTransformer
{
    using System;
    using System.Collections.Generic;
    using Microsoft.OData.Core.UriParser.Semantic;

    public interface IODataFunctionToSqlConvertor
    {
        bool IsSupported(string functionName);

        string Convert(string functionName, Func<QueryNode, string> genericBinder, List<QueryNode> arguments);
    }
}