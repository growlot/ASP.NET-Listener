// //-----------------------------------------------------------------------
// // <copyright file="ODataServiceConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;
    using System.Web.OData.Batch;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using MessageHandlers;
    using Persistence.Listener;
    using Services;
    using MetadataService;
    using AsyncPoco;

    public class ODataServiceConfigurator
    {
        private readonly IEdmModelGenerator modelGenerator;
        private readonly IMetadataService metadataService;

        public ODataServiceConfigurator(IEdmModelGenerator modelGenerator, IMetadataService metadataService)
        {
            this.modelGenerator = modelGenerator;
            this.metadataService = metadataService;
        }

        public void Configure(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            // config.MessageHandlers.Add(new MiniProfilerMessageHandler());

            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new WNPGenericRoutingConvention(metadataService));
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;

            config.MapODataServiceRoute(
                routeName: "WNPODataRoute",
                routePrefix: null,
                model: this.modelGenerator.GenerateODataModel(),
                pathHandler: new DefaultODataPathHandler(),
                routingConventions: conventions //,
                /*defaultHandler: new DefaultODataBatchHandler(new HttpServer(config))*/);

            // separate OData endpoint for Listener API
            var builder = new ODataConventionModelBuilder
            {
                Namespace = "AMSLLC.Listener",
                ContainerName = "AMSLLC.Listener"
            };

            MapPetaPocoEntity<TransactionRegistryEntity, string>(builder, a => a.RecordKey);

            var transactionRegistry = builder.EntityType<TransactionRegistryEntity>();

            // bound actions
            transactionRegistry.Action("Process");
            transactionRegistry.Action("Succeed");

            var failAction = transactionRegistry.Action("Fail");
            failAction.Parameter<string>("Message");
            failAction.Parameter<string>("Details").OptionalParameter = true;

            // unbound actions
            var openAction = builder.Action("Open");
            ConfigureHeader(openAction, builder);
            openAction.Returns<string>();

            DelegatingHandler[] handlers = new DelegatingHandler[]
            {
                new ListenerMessageHandler()
            };

            // Create a message handler chain with an end-point.
            var routeHandlers = HttpClientFactory.CreatePipeline(
                new HttpControllerDispatcher(config), handlers);

            config.MapODataServiceRoute(
                routeName: "listener",
                routePrefix: "listener",
                model: builder.GetEdmModel(),
                defaultHandler: routeHandlers);
        }

        private void ConfigureHeader(ActionConfiguration action, ODataModelBuilder model)
        {
            foreach (var parameterDefinition in ListenerRequestHeaderMap.Instance)
            {
                var parameter = action.AddParameter(parameterDefinition.Key, model.GetTypeConfigurationOrNull(parameterDefinition.Value));
                parameter.OptionalParameter = true;
            }
        }

        private void MapPetaPocoEntity<T, TKey>(
            ODataModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector) where T : class
        {
            var tableNameAttribute = typeof(T).GetCustomAttribute<TableNameAttribute>();
            modelBuilder.EntitySet<T>(tableNameAttribute.Value);
            modelBuilder.EntityType<T>().HasKey(primaryKeySelector);
        }
    }





}