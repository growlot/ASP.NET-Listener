using System.Linq;
using System.Web.OData.Builder;
using AMSLLC.Listener.MetadataService;
using Microsoft.OData.Edm;
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
                Namespace = _metadataService.ODataModelNamespace,
                ContainerName = "WNP"
            };

            var method = typeof(ODataConventionModelBuilder).GetMethod("EntitySet");
            var generatedModels =
                _metadataService.ODataModelAssembly.GetTypes()
                    .Where(type => typeof(IODataEntity).IsAssignableFrom(type));

            generatedModels.Map(type => method.MakeGenericMethod(type).Invoke(builder, new object[] { $"{type.Name}s" }));

            return _model = builder.GetEdmModel();
        }
    }
}