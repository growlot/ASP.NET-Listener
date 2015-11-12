// <copyright file="IFilterTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.FilterTransformer
{
    using System.Web.OData.Query;

    public interface IFilterTransformer
    {
        WhereClause TransformFilterQueryOption(FilterQueryOption filterQueryOption, int positionalArgsOffset = 0);
    }

    public class WhereClause
    {
        public string Clause { get; set; }

        public object[] PositionalParameters { get; set; }
    }
}