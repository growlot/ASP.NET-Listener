// //-----------------------------------------------------------------------
// <copyright file="ODataServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using HttpMessageHandlers;
    using MetadataService;
    using Services;

    /// <summary>
    /// Configures OData service
    /// </summary>
    public class ODataServiceConfigurator
    {
        private readonly IEdmModelGenerator modelGenerator;
        private readonly IMetadataProvider metadataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataServiceConfigurator"/> class.
        /// </summary>
        /// <param name="modelGenerator">The model generator.</param>
        /// <param name="metadataService">The metadata service.</param>
        public ODataServiceConfigurator(IEdmModelGenerator modelGenerator, IMetadataProvider metadataService)
        {
            this.modelGenerator = modelGenerator;
            this.metadataService = metadataService;
        }

        /// <summary>
        /// Configures OData service.
        /// </summary>
        /// <param name="config">The HTTP configuration.</param>
        public void Configure(HttpConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), "Can not configure OData service if HTTP configuration is not provided.");
            }

            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            config.Filters.Add(new CommonExceptionFilterAttribute());

            // config.MessageHandlers.Add(new MiniProfilerMessageHandler());
            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new WNPGenericRoutingConvention(this.metadataService));
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            // Adding batch handler. Code taken from OData implementation.
            // Can not use MapODataServiceRoute, because it doesn't allow to specify defaultHandler and batchHandler at the same time.
            using (var httpServer = new HttpServer(config))
            {
                using (var batchHandler = new TransactionalODataBatchHandler(httpServer))
                {
                    batchHandler.ODataRouteName = "WNPODataRoute";
                    string batchTemplate = ODataRouteConstants.Batch;
                    config.Routes.MapHttpBatchRoute(batchHandler.ODataRouteName + "Batch", batchTemplate, batchHandler);
                }
            }

            using (var httpController = new HttpControllerDispatcher(config))
            {
                DelegatingHandler[] handlersWnp = new DelegatingHandler[] { new RequestScopeHandler() };

                var routeHandlersWnp = HttpClientFactory.CreatePipeline(httpController, handlersWnp);
                ////using (var routeHandlersWnp = HttpClientFactory.CreatePipeline(httpController, handlersWnp))
                ////{
                    config.MapODataServiceRoute(
                        routeName: "WNPODataRoute",
                        routePrefix: null,
                        model: this.modelGenerator.GenerateODataModel(),
                        pathHandler: new DefaultODataPathHandler(),
                        routingConventions: conventions,
                        defaultHandler: routeHandlersWnp);
                ////}
            }

            var builder = new ODataConventionModelBuilder { Namespace = "AMSLLC.Listener", ContainerName = "ListenerContainer" };

            new ODataListenerServiceConfigurator().Run(builder);

            // Create a message handler chain with an end-point.
            config.MapODataServiceRoute(
                routeName: "listener",
                routePrefix: "listener",
                model: builder.GetEdmModel());
        }
    }
}