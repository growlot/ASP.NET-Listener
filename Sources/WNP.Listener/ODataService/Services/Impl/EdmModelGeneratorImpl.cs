using System;
using System.Linq;
using System.Reflection;
using System.Web.OData.Builder;
using AMSLLC.Listener.MetadataService;
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
            _metadataService = metadataService;
        }

        public IEdmModel GenerateODataModel() {
            if (_model != null)
                return _model;

            var builder = new ODataConventionModelBuilder
            {
                Namespace = "AMSLLC.Listener",
                ContainerName = "AMSLLC.Listener"
            };

            builder.EntitySet<TestEntity>("TestEntities");
            ActionConfiguration actionConfig = builder.EntityType<TestEntity>().Action("Action");
            ParameterConfiguration parameter = actionConfig.Parameter<string>("Value");
            parameter.OptionalParameter = false;
            parameter = actionConfig.Parameter<string>("Value2");
            parameter.OptionalParameter = true;

            var method = typeof(ODataConventionModelBuilder).GetMethod("EntitySet");
            var generatedModels =
                _metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IODataEntity).IsAssignableFrom(type));

            generatedModels.Map(type =>
            {
                entitySetMethod.MakeGenericMethod(type).Invoke(builder, new object[] {$"{type.Name}s"});

                var actionsContainer = _metadataService.GetModelMapping(type).ActionsContainer;
                if (actionsContainer != null)
                {
                    var entityTypeConfiguration = entityTypeMethod.MakeGenericMethod(type).Invoke(builder, new object[0]);

                    var actionMethodsList = actionsContainer.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    actionMethodsList.Map(info =>
                    {
                        var parameters = info.GetParameters();

                        // we got an action here
                        if (info.ReturnType == typeof (void))
                        {
                            var etcType = entityTypeConfiguration.GetType();
                            var actionMethodInfo = etcType.GetMethod("Action");

                            var actionConfiguration = actionMethodInfo.Invoke(entityTypeConfiguration, new[] { info.Name });
                        }
                    });
                }
            });

            return _model = builder.GetEdmModel();
        }
    }
}