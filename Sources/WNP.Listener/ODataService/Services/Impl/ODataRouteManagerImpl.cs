using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace AMSLLC.Listener.ODataService.Services.Impl
{
    // this is stub service which we'll need in future for reloading/regenerating metadata
    public class ODataRouteManagerImpl : IODataRouteManager
    {
        private readonly HttpConfiguration _config;

        public ODataRouteManagerImpl(HttpConfiguration config)
        {
          _config = config;
        }

        public void AddOrderRoute()
        {
            var builder = new ODataConventionModelBuilder();
//            builder.EntitySet<Meter>("Meters");
//            builder.EntitySet<Order>("Orders");

            var route = _config.Routes.Where(r => r is System.Web.OData.Routing.ODataRoute).First();
            var odataRoute = route as System.Web.OData.Routing.ODataRoute;

            _config.Routes.Remove("ODataRoute");

            _config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null, //"odata",
                model: builder.GetEdmModel(),
                pathHandler: odataRoute.PathRouteConstraint.PathHandler,
                routingConventions: odataRoute.PathRouteConstraint.RoutingConventions);
        }
    }
}