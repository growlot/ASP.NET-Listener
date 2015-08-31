using Microsoft.OData.Edm;

namespace WNP.Listener.ODataService.Services
{
    public interface IEdmModelGenerator
    {
        IEdmModel GenerateODataModel();
    }
}