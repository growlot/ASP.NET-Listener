// <copyright file="ODataRouteManagerImpl.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;

    // this is stub service which we'll need in future for reloading/regenerating metadata
    public class ODataRouteManagerImpl : IODataRouteManager
    {
        private readonly HttpConfiguration config;

        public ODataRouteManagerImpl(HttpConfiguration config)
        {
            this.config = config;
        }

        /// <inheritdoc/>
        public void AddOrderRoute()
        {
            var builder = new ODataConventionModelBuilder();
            var route = this.config.Routes.Where(r => r is System.Web.OData.Routing.ODataRoute).First();
            var odataRoute = route as System.Web.OData.Routing.ODataRoute;

            this.config.Routes.Remove("ODataRoute");

            this.config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null, // "odata",
                model: builder.GetEdmModel(),
                pathHandler: odataRoute.PathRouteConstraint.PathHandler,
                routingConventions: odataRoute.PathRouteConstraint.RoutingConventions);
        }
    }
}