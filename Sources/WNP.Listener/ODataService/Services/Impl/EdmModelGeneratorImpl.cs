using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.OData.Builder;
using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.ODataService.Actions;
using AMSLLC.Listener.ODataService.Actions.Attributes;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;
using Ninject.Infrastructure.Language;

namespace AMSLLC.Listener.ODataService.Services.Impl
{
    public class EdmModelGeneratorImpl : IEdmModelGenerator
    {
        private readonly IMetadataService _metadataService;

        private static IEdmModel _model;

        public EdmModelGeneratorImpl(IMetadataService metadataService)
        {
            this._metadataService = metadataService;
        }

        public IEdmModel GenerateODataModel() {
            if (_model != null)
                return _model;

            var builder = new ODataConventionModelBuilder
            {
                Namespace = "AMSLLC.Listener",
                ContainerName = "AMSLLC.Listener"
            };

            var generatedModels =
                this._metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IODataEntity).IsAssignableFrom(type));

            generatedModels.Map(type => this.GenerateBoundActions(type, builder));

            this.GenerateUnboundActions(builder);

            return _model = builder.GetEdmModel();
        }

        private void GenerateUnboundActions(ODataConventionModelBuilder builder)
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
                        .Where(info => info.CustomAttributes.Any(data => data.AttributeType == typeof (UnboundActionAttribute)));

                var actionPrefix = type.Name;
                var actionPrefixAttribute =
                    type.GetCustomAttributes(typeof (ActionPrefixAttribute)).FirstOrDefault() as ActionPrefixAttribute;

                if (actionPrefixAttribute != null)
                    actionPrefix = actionPrefixAttribute.Prefix;

                actionMethodsList.Map(info =>
                {
                    var actionConfiguration = builder.Action($"{actionPrefix}_{info.Name}");
                    CreateActionParameters(info, actionConfiguration);
                });
            });
        }

        private void GenerateBoundActions(Type type, ODataConventionModelBuilder builder)
        {
            var entitySetMethod = typeof (ODataConventionModelBuilder).GetMethod("EntitySet");
            var entityTypeMethod = typeof (ODataConventionModelBuilder).GetMethod("EntityType");

            entitySetMethod.MakeGenericMethod(type).Invoke(builder, new object[] {$"{type.Name}s"});

            var actionsContainer = this._metadataService.GetModelMapping(type).ActionsContainer;
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
                    var actionConfiguration = CreateActionConfiguration(info, entityTypeConfiguration, actionsContainer);
                    CreateActionParameters(info, actionConfiguration);
                });
            }
        }

        private static void CreateActionParameters(MethodInfo actionMethodInfo, ActionConfiguration actionConfiguration)
        {
            var methodParameters = actionMethodInfo.GetParameters();
            var actionConfigurationType = typeof(ActionConfiguration);

            methodParameters.Map(parameterInfo =>
            {
                var paramConfig =
                    actionConfigurationType.GetMethod("Parameter")
                        .MakeGenericMethod(parameterInfo.ParameterType)
                        .Invoke(actionConfiguration, new object[] {parameterInfo.Name}) as ParameterConfiguration;

                Debug.Assert(paramConfig != null, "paramConfig != null");
                if (parameterInfo.IsOptional)
                    paramConfig.OptionalParameter = true;
            });

            if (actionMethodInfo.ReturnType != typeof (void))
            {
                actionConfigurationType.GetMethod("Returns")
                    .MakeGenericMethod(actionMethodInfo.ReturnType)
                    .Invoke(actionConfiguration, new object[0]);
            }
        }

        private static ActionConfiguration CreateActionConfiguration(MethodInfo actionMethod, object entityTypeConfiguration, Type actionsContainer)
        {
            var isCollectionWide =
                actionMethod.CustomAttributes.Any(a => a.AttributeType == typeof (CollectionWideActionAttribute));

            var etcType = entityTypeConfiguration.GetType();

            object actionConfiguration;
            var actionPrefix = actionsContainer.Name;
            var actionPrefixAttribute =
                actionsContainer.GetCustomAttributes(typeof (ActionPrefixAttribute)).FirstOrDefault() as
                    ActionPrefixAttribute;

            if (actionPrefixAttribute != null)
                actionPrefix = actionPrefixAttribute.Prefix;

            if (isCollectionWide)
            {
                var collectionPropertyType = etcType.GetProperty("Collection").PropertyType;
                var collectionProperty = etcType.GetProperty("Collection").GetValue(entityTypeConfiguration);

                var actionMethodInfo = collectionPropertyType.GetMethod("Action");

                actionConfiguration = actionMethodInfo.Invoke(collectionProperty,
                    new[] {$"{actionPrefix}_{actionMethod.Name}"});
            }
            else
            {
                var actionMethodInfo = etcType.GetMethod("Action");
                actionConfiguration = actionMethodInfo.Invoke(entityTypeConfiguration,
                    new[] {$"{actionPrefix}_{actionMethod.Name}"});
            }

            return actionConfiguration as ActionConfiguration;
        }
    }
}