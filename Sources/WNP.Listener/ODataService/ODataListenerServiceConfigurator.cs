// <copyright file="ODataListenerServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Web.OData.Builder;
    using Controllers;
    using Persistence.Listener;

    public class ODataListenerServiceConfigurator : ODataControllerConfigurator
    {
        protected override void SetupTransactionRegistryController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryEntity>> actionBuilder = null, string tableName = null)
        {
            base.SetupTransactionRegistryController(
                builder,
                (b,
                    configuration) =>
                {
                    // bound actions
                    configuration.Action("Process");
                    configuration.Action("Succeed");

                    var failAction = configuration.Action("Fail");
                    failAction.Parameter<string>("Message");
                    failAction.Parameter<string>("Details").OptionalParameter = true;

                    // unbound actions
                    var openAction = b.Action("Open");
                    //this.ConfigureHeader(openAction, builder);
                    openAction.Parameter<string>("EntityCategory").OptionalParameter = false;
                    openAction.Parameter<string>("OperationKey").OptionalParameter = false;
                    openAction.Parameter<string>("Body").OptionalParameter = false;
                    openAction.Returns<string>();

                    var openBatchAction = b.Action("Batch");
                    openBatchAction.Parameter<string>("BatchNumber").OptionalParameter = false;
                    openBatchAction.Parameter<string>("Body").OptionalParameter = false;
                    //this.ConfigureHeader(openBatchAction, builder);
                    //openBatchAction.Parameter<BatchRequestMessage>("request").OptionalParameter = false;
                    //openBatchAction.CollectionParameter<BatchRequestMessage>("request").OptionalParameter = false;
                    openBatchAction.Returns<string>();

                    var buildBatchAction = b.Action("BuildBatch");
                    //this.ConfigureHeader(buildBatchAction, builder);
                    buildBatchAction.Parameter<string>("batchKey").OptionalParameter = false;
                    buildBatchAction.Returns<string>();

                    configuration.Ignore(p => p.TransactionId);
                }, tableName);
        }

        protected override void SetupEndpointController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<EndpointEntity>> actionBuilder = null, string tableName = null)
        {
            base.SetupEndpointController(
                builder,
                (modelBuilder,
                    configuration) =>
                {
                    configuration.ContainsRequired(entity => entity.ProtocolType);
                    configuration.ContainsRequired(entity => entity.EndpointTriggerType);
                }, tableName);
        }

        protected override void SetupEntityCategoryOperationController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<EntityCategoryOperationEntity>> actionBuilder = null,
            string tableName = null)
        {
            base.SetupEntityCategoryOperationController(
                builder,
                (modelBuilder,
                    configuration) =>
                {
                    configuration.ContainsRequired(entity => entity.EnabledOperation);
                    configuration.ContainsRequired(entity => entity.EntityCategory);
                    configuration.ContainsMany(entity => entity.OperationEndpoints);
                }, tableName);
        }

        protected override void SetupTransactionRegistryDetailsController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryViewEntity>> actionBuilder = null, string tableName = null)
        {
            base.SetupTransactionRegistryDetailsController(
                builder,
                (modelBuilder,
                    configuration) =>
                {
                    var cntFunc = configuration.Collection.Function("CountByStatus");
                    cntFunc.CollectionParameter<int>("statusTypes");
                    cntFunc.Returns<int>();
                }, "TransactionRegistryDetails");
        }

        private void ConfigureHeader(
            ActionConfiguration action,
            ODataModelBuilder model)
        {
            foreach (var parameterDefinition in ListenerRequestHeaderMap.Instance)
            {
                var parameter = action.AddParameter(
                    parameterDefinition.Key,
                    model.GetTypeConfigurationOrNull(parameterDefinition.Value));
                parameter.OptionalParameter = true;
            }
        }
    }
}