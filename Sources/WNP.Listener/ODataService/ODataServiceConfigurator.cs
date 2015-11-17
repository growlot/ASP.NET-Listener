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

            // config.MessageHandlers.Add(new MiniProfilerMessageHandler());
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

            var builder = new ODataConventionModelBuilder { Namespace = "AMSLLC.Listener", ContainerName = "AMSLLC.Listener" };
            this.PrepareODataController<TransactionRegistryEntity, Guid>(builder, a => a.RecordKey, (b, configuration) =>
            {
                // bound actions
                configuration.Action("Process");
                configuration.Action("Succeed");

                var failAction = configuration.Action("Fail");
                failAction.Parameter<string>("Message");
                failAction.Parameter<string>("Details").OptionalParameter = true;

                // unbound actions
                var openAction = b.Action("Open");
                this.ConfigureHeader(openAction, builder);
                openAction.Returns<string>();

                var openBatchAction = b.Action("Batch");
                this.ConfigureHeader(openBatchAction, builder);
                openBatchAction.Returns<string>();

                var buildBatchAction = b.Action("BuildBatch");
                this.ConfigureHeader(buildBatchAction, builder);
                buildBatchAction.Parameter<string>("batchKey").OptionalParameter = false;
                buildBatchAction.Returns<string>();

                configuration.Ignore(p => p.TransactionId);
            });

            this.PrepareODataController<TransactionMessageDatumEntity, Guid>(builder, a => a.RecordKey);
            this.PrepareODataController<TransactionRegistryViewEntity, Guid>(builder, a => a.RecordKey, null, "TransactionRegistryDetails");

            // Create a message handler chain with an end-point.
            DelegatingHandler[] handlers = new DelegatingHandler[] { new ListenerMessageHandler() };
            var routeHandlers = HttpClientFactory.CreatePipeline(new HttpControllerDispatcher(config), handlers);
            config.MapODataServiceRoute(
                routeName: "listener",
                routePrefix: "listener",
                model: builder.GetEdmModel(),
                defaultHandler: routeHandlers);
        }

        private void PrepareODataController<TEntity, TKey>(
            ODataModelBuilder builder,
            Expression<Func<TEntity, TKey>> primaryKeySelector,
            Action<ODataModelBuilder, EntityTypeConfiguration<TEntity>> actionBuilder = null,
            string tableName = null) where TEntity : class
        {
            // separate OData endpoint for Listener API
            this.MapPetaPocoEntity(builder, primaryKeySelector, tableName);

            var entityType = builder.EntityType<TEntity>();

            actionBuilder?.Invoke(builder, entityType);
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
            Expression<Func<T, TKey>> primaryKeySelector, string tableName = null)
            where T : class
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                var tableNameAttribute = typeof(T).GetCustomAttribute<AsyncPoco.TableNameAttribute>();
                modelBuilder.EntitySet<T>(tableNameAttribute.Value);
            }
            else
            {
                modelBuilder.EntitySet<T>(tableName);
            }
            modelBuilder.EntityType<T>().HasKey(primaryKeySelector);
        }
    }
}