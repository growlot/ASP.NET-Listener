// //-----------------------------------------------------------------------
// <copyright file="ODataServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using HttpMessageHandlers;
    using MetadataService;
    using Persistence.Listener;
    using Services;
    using Shared;

    public class ODataServiceConfigurator
    {
        private readonly IEdmModelGenerator modelGenerator;
        private readonly IMetadataProvider metadataService;

        public ODataServiceConfigurator(IEdmModelGenerator modelGenerator, IMetadataProvider metadataService)
        {
            this.modelGenerator = modelGenerator;
            this.metadataService = metadataService;
        }

        public void Configure(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.Filters.Add(new CommonExceptionFilterAttribute());

            config.MessageHandlers.Add(new MiniProfilerMessageHandler());
            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new WNPGenericRoutingConvention(this.metadataService));
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            DelegatingHandler[] handlersWnp = new DelegatingHandler[] { new RequestScopeHandler() };
            var routeHandlersWnp = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlersWnp);

            // Adding batch handler. Code taken from OData implementation.
            // Can not use MapODataServiceRoute, because it doesn't allow to specify defaultHandler and batchHandler at the same time.
            var batchHandler = new TransactionalODataBatchHandler(new HttpServer(config));
            batchHandler.ODataRouteName = "WNPODataRoute";
            string batchTemplate = ODataRouteConstants.Batch;
            config.Routes.MapHttpBatchRoute(batchHandler.ODataRouteName + "Batch", batchTemplate, batchHandler);

            config.MapODataServiceRoute(
                routeName: "WNPODataRoute",
                routePrefix: null,
                model: this.modelGenerator.GenerateODataModel(),
                pathHandler: new DefaultODataPathHandler(),
                routingConventions: conventions,
                defaultHandler: routeHandlersWnp);

            var builder = new ODataConventionModelBuilder { Namespace = "AMSLLC.Listener" };

            new ODataListenerServiceConfigurator().Run(builder);

            // Create a message handler chain with an end-point.
            DelegatingHandler[] handlers = new DelegatingHandler[] { new ListenerMessageHandler() };
            var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);
            config.MapODataServiceRoute(
                routeName: "listener",
                routePrefix: "listener",
                model: builder.GetEdmModel(),
                defaultHandler: routeHandlers);
        }
    }
}