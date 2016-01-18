// <copyright file="EdmModelGenerator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Web.OData.Builder;
    using MetadataService;
    using MetadataService.Attributes;
    using Microsoft.OData.Edm;
    using Ninject.Infrastructure.Language;
    using Utilities;

    /// <summary>
    /// Implements <see cref="IEdmModelGenerator"/>
    /// </summary>
    public class EdmModelGenerator : IEdmModelGenerator
    {
        private static IEdmModel model;

        private readonly IMetadataProvider metadataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdmModelGenerator"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        public EdmModelGenerator(IMetadataProvider metadataService)
        {
            this.metadataService = metadataService;
        }

        /// <inheritdoc/>
        public IEdmModel GenerateODataModel()
        {
            if (model != null)
            {
                return model;
            }

            var builder = new ODataConventionModelBuilder
            {
                Namespace = "AMSLLC.Listener",
                ContainerName = "AMSLLC.Listener"
            };

            var entitySetTypes =
                this.metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IODataEntity).IsAssignableFrom(type) && !typeof(IContainedEntity).IsAssignableFrom(type));

            entitySetTypes.Map(type => GenerateEntitySet(type, builder));

            var virtualEntityTypes =
                this.metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IVirtualODataEntity).IsAssignableFrom(type));

            virtualEntityTypes.Map(type => GenerateEntityType(type, builder));

            var entityTypes =
                this.metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IODataEntity).IsAssignableFrom(type));

            entityTypes.Map(type => this.GenerateBoundActions(type, builder));

            GenerateUnboundActions(builder);

            return model = builder.GetEdmModel();
        }

        private static void CreateActionParameters(MethodInfo actionMethodInfo, ActionConfiguration actionConfiguration)
        {
            var methodParameters = actionMethodInfo.GetParameters();
            var actionConfigurationType = typeof(ActionConfiguration);

            methodParameters.Where(
                info => info.CustomAttributes.All(data => data.AttributeType != typeof(BoundEntityKeyAttribute)))
                .Map(
                    parameterInfo =>
                        {
                            var paramConfig =
                                actionConfigurationType.GetMethod("Parameter")
                                    .MakeGenericMethod(parameterInfo.ParameterType)
                                    .Invoke(actionConfiguration, new object[] { parameterInfo.Name }) as
                                ParameterConfiguration;

                            Debug.Assert(paramConfig != null, "paramConfig != null");
                            if (parameterInfo.IsOptional)
                            {
                                paramConfig.OptionalParameter = true;
                            }
                        });

            if (actionMethodInfo.ReturnType != typeof(void))
            {
                actionConfigurationType.GetMethod("Returns")
                    .MakeGenericMethod(actionMethodInfo.ReturnType)
                    .Invoke(actionConfiguration, new object[0]);
            }
        }

        /// <summary>
        /// Creates the action configuration.
        /// </summary>
        /// <param name="actionMethod">The action method.</param>
        /// <param name="entityTypeConfiguration">The entity type configuration.</param>
        /// <returns>The OData action configuration.</returns>
        private static ActionConfiguration CreateActionConfiguration(MethodInfo actionMethod, object entityTypeConfiguration)
        {
            var isCollectionWide =
                actionMethod.CustomAttributes.Any(a => a.AttributeType == typeof(CollectionWideActionAttribute));

            var etcType = entityTypeConfiguration.GetType();

            object actionConfiguration;

            if (isCollectionWide)
            {
                var collectionPropertyType = etcType.GetProperty("Collection").PropertyType;
                var collectionProperty = etcType.GetProperty("Collection").GetValue(entityTypeConfiguration);

                var actionMethodInfo = collectionPropertyType.GetMethod("Action");

                actionConfiguration = actionMethodInfo.Invoke(
                    collectionProperty,
                    new[] { actionMethod.Name });
            }
            else
            {
                var actionMethodInfo = etcType.GetMethod("Action");
                actionConfiguration = actionMethodInfo.Invoke(
                    entityTypeConfiguration,
                    new[] { actionMethod.Name });
            }

            return actionConfiguration as ActionConfiguration;
        }

        private static void GenerateEntitySet(Type type, ODataConventionModelBuilder builder)
        {
            var entitySetMethod = typeof(ODataConventionModelBuilder).GetMethod("EntitySet");
            entitySetMethod.MakeGenericMethod(type).Invoke(builder, new object[] { StringUtilities.Invariant($"{type.Name}s") });
        }

        private static void GenerateUnboundActions(ODataConventionModelBuilder builder)
        {
            var unboundActionContainers =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface)
                    .Where(type => typeof(IUnboundActionsContainer).IsAssignableFrom(type));

            unboundActionContainers.Map(type =>
            {
                var actionMethodsList =
                    type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(info => info.CustomAttributes.Any(data => data.AttributeType == typeof(UnboundActionAttribute)));

                actionMethodsList.Map(info =>
                {
                    var actionConfiguration = builder.Action(info.Name);
                    CreateActionParameters(info, actionConfiguration);
                });
            });
        }

        private static void GenerateEntityType(Type type, ODataConventionModelBuilder builder)
        {
            var entitySetMethod = typeof(ODataConventionModelBuilder).GetMethod("EntityType");
            entitySetMethod.MakeGenericMethod(type).Invoke(builder, new object[] { });
        }

        private void GenerateBoundActions(Type type, ODataConventionModelBuilder builder)
        {
            var entityTypeMethod = typeof(ODataConventionModelBuilder).GetMethod("EntityType");

            var actionsContainer = this.metadataService.GetModelMapping(type).ActionsContainer;
            if (actionsContainer != null)
            {
                var entityTypeConfiguration = entityTypeMethod.MakeGenericMethod(type).Invoke(builder, new object[0]);

                var actionMethodsList =
                    actionsContainer.GetMethods(BindingFlags.Public | BindingFlags.Instance |
                                                BindingFlags.DeclaredOnly)
                        .Where(info => info.Name != "GetEntityTableName")
                        .Where(info => info.CustomAttributes.Any(data => data.AttributeType == typeof(BoundActionAttribute)));

                actionMethodsList.Map(info =>
                {
                    var actionConfiguration = CreateActionConfiguration(info, entityTypeConfiguration);
                    CreateActionParameters(info, actionConfiguration);
                });
            }
        }
    }
}