using Microsoft.OData.Edm;

namespace AMSLLC.Listener.ODataService.Services
{
    public interface IEdmModelGenerator
    {
        IEdmModel GenerateODataModel();
    }
}