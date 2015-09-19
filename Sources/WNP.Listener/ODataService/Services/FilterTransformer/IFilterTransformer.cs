using System.Web.OData.Query;

namespace AMSLLC.Listener.ODataService.Services.FilterTransformer
{
    public interface IFilterTransformer
    {
        WhereClause TransformFilterQueryOption(FilterQueryOption filterQueryOption);
    }

    public class WhereClause
    {
        public string Clause { get; set; }

        public object[] PositionalParameters { get; set; }
    }
}