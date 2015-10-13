// //-----------------------------------------------------------------------
// // <copyright file="ODataServiceConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;
    using System.Web.OData.Batch;
    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using System.Web.OData.Formatter;
    using System.Web.OData.Routing;
    using System.Web.OData.Routing.Conventions;
    using MessageHandlers;
    using Persistence;
    using Persistence.Listener;
    using Services;

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


            ODataModelBuilder builder = new ODataConventionModelBuilder();
            var set = MapPetaPocoEntity<TransactionRegistryEntity, string>(builder, a => a.Key);

            var pAction = set.Action("Process");
            pAction.Namespace = "Transaction";

            var sAction = set.Action("Succeed");
            sAction.Namespace = "Transaction";

            var fAction = set.Action("Fail");
            fAction.Namespace = "Transaction";
            fAction.Parameter<string>("Message");
            fAction.Parameter<string>("Details");

            var action = builder.Action("Open");
            ConfigureHeader(action, builder);
            action.Returns<string>();




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
            foreach (var o in ListenerRequestHeaderMap.Instance)
            {
                var parameter = action.AddParameter(o.Key, model.GetTypeConfigurationOrNull(o.Value));
                parameter.OptionalParameter = true;
            }
        }

        private EntityTypeConfiguration<T> MapPetaPocoEntity<T, TKey>(ODataModelBuilder modelBuilder,
            Expression<Func<T, TKey>> primaryKeySelector) where T : class
        {
            var tableNameAttribute = typeof(T).GetCustomAttribute<AsyncPoco.TableNameAttribute>();
            //var primaryKeyAttribute = typeof(T).GetCustomAttribute<AsyncPoco.PrimaryKeyAttribute>();
            //var keyPropertyName = GetPropertyName(primaryKeySelector);
            //if (string.Compare(primaryKeyAttribute.Value, keyPropertyName, StringComparison.InvariantCulture) != 0)
            //{
            //    throw new InvalidOperationException(
            //        $"Specified {keyPropertyName} as primary key, {primaryKeyAttribute.Value} expected");
            //}
            var tps = modelBuilder.EntitySet<T>(tableNameAttribute.Value);
            var tp = tps.EntityType;
            return tp.HasKey(primaryKeySelector);
        }

        private string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> c)
        {
            Type paramType = c.Parameters[0].Type; // first parameter of expression
            var d = paramType.GetMember((c.Body as MemberExpression).Member.Name)[0];
            return d.Name;
        }
    }





}