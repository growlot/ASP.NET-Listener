using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using WNP.Listener.MetadataService;
using WNP.Listener.ODataService.Controllers;
using WNP.Listener.ODataService.MessageHandlers;
using WNP.Listener.ODataService.Services;

namespace WNP.Listener.ODataService
{
    public class ODataServiceConfigurator
    {
        private readonly IEdmModelGenerator _modelGenerator;

        public ODataServiceConfigurator(IEdmModelGenerator modelGenerator)
        {
            _modelGenerator = modelGenerator;
        }

        public void Configure(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.MessageHandlers.Add(new MiniProfilerMessageHandler());

            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new WNPGenericRoutingConvention());

            config.MapODataServiceRoute(
                routeName: "WNPODataRoute",
                routePrefix: null,
                routingConventions: conventions,
                pathHandler: new DefaultODataPathHandler(),
                model: _modelGenerator.GenerateODataModel());
        }
    }
}