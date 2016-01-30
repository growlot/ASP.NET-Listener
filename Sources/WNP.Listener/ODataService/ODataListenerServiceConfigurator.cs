// <copyright file="ODataListenerServiceConfigurator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using System;
    using System.Web.OData.Builder;
    using Controllers;
    using Persistence.Listener;

    /// <summary>
    /// Configures Listener OData service.
    /// </summary>
    public class ODataListenerServiceConfigurator : ODataControllerConfigurator
    {
        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        /// <param name="tableName">The table name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is an override method and it's signature can not be changed.")]
        protected override void SetupTransactionRegistryController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryEntity>> actionBuilder = null,
            string tableName = null)
        {
            base.SetupTransactionRegistryController(
                builder,
                (b, configuration) =>
                {
                    // bound actions
                    configuration.Action("Process");
                    configuration.Action("Succeed");

                    var failAction = configuration.Action("Fail");
                    failAction.Parameter<string>("Message");
                    failAction.Parameter<string>("Details").OptionalParameter = true;

                    // unbound actions
                    var openAction = b.Action("Open");
                    openAction.Parameter<string>("EntityCategory").OptionalParameter = false;
                    openAction.Parameter<string>("OperationKey").OptionalParameter = false;
                    openAction.Parameter<string>("Body").OptionalParameter = false;
                    openAction.Parameter<string>("OperationTransactionIdentifier").OptionalParameter = true;
                    openAction.ReturnsCollection<Guid>();

                    var openBatchAction = b.Action("Batch");
                    openBatchAction.Parameter<string>("BatchNumber").OptionalParameter = false;
                    openBatchAction.Parameter<string>("Body").OptionalParameter = false;
                    //// openBatchAction.Parameter<BatchRequestMessage>("request").OptionalParameter = false;
                    //// openBatchAction.CollectionParameter<BatchRequestMessage>("request").OptionalParameter = false;
                    openBatchAction.ReturnsCollection<Guid>();

                    var buildBatchAction = b.Action("BuildBatch");
                    buildBatchAction.Parameter<string>("batchKey").OptionalParameter = false;
                    buildBatchAction.ReturnsCollection<Guid>();

                    configuration.Ignore(p => p.TransactionId);
                },
                tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        /// <param name="tableName">The table name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is an override method and it's signature can not be changed.")]
        protected override void SetupEndpointController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<EndpointEntity>> actionBuilder = null,
            string tableName = null)
        {
            base.SetupEndpointController(
                builder,
                (modelBuilder, configuration) =>
                {
                    configuration.ContainsRequired(entity => entity.ProtocolType);
                    configuration.ContainsRequired(entity => entity.EndpointTriggerType);
                },
                tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        /// <param name="tableName">The table name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is an override method and it's signature can not be changed.")]
        protected override void SetupEntityCategoryOperationController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<EntityCategoryOperationEntity>> actionBuilder = null,
            string tableName = null)
        {
            base.SetupEntityCategoryOperationController(
                builder,
                (modelBuilder, configuration) =>
                {
                    configuration.ContainsRequired(entity => entity.EnabledOperation);
                    configuration.ContainsRequired(entity => entity.EntityCategory);
                    configuration.ContainsMany(entity => entity.OperationEndpoints);
                },
                tableName);
        }

        /// <summary>
        /// Setup the controller.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="actionBuilder">The action builder.</param>
        /// <param name="tableName">The table name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "This is an override method and it's signature can not be changed.")]
        protected override void SetupTransactionRegistryDetailsController(
            ODataModelBuilder builder,
            Action<ODataModelBuilder, EntityTypeConfiguration<TransactionRegistryViewEntity>> actionBuilder = null,
            string tableName = null)
        {
            base.SetupTransactionRegistryDetailsController(
                builder,
                (modelBuilder, configuration) =>
                {
                    var cntFunc = configuration.Collection.Function("CountByStatus");
                    cntFunc.CollectionParameter<int>("statusTypes");
                    cntFunc.Returns<int>();
                },
                "TransactionRegistryDetails");
        }
    }
}