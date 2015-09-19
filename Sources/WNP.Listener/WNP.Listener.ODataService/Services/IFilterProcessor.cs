using System.Web.OData.Query;

namespace WNP.Listener.ODataService.Services
{
    public interface IFilterProcessor
    {
        void Process(FilterQueryOption filterQueryOption);
    }
}