using Ninject.Modules;
using Ninject.Web.Common;
using WNP.Listener.MetadataService;
using WNP.Listener.MetadataService.Impl;
using WNP.Listener.ODataService.Services;
using WNP.Listener.ODataService.Services.FilterTransformer;
using WNP.Listener.ODataService.Services.Impl;
using WNP.Listener.ODataService.Services.Impl.FilterTransformer;

namespace WNP.Listener.ODataService.Properties
{
    public class ODataServiceModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<WNPDBContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");

            Kernel.Bind<ODataServiceConfigurator>().ToSelf().InSingletonScope();

            Kernel.Bind<IEdmModelGenerator>().To<EdmModelGeneratorImpl>();            
            Kernel.Bind<IODataRouteManager>().To<ODataRouteManagerImpl>();

            Kernel.Bind<IODataEntityConfiguration>().To<ODataEntityConfigurationImpl>();

            Kernel.Bind<IAutoConvertor>().To<AutoConvertorImpl>();
            Kernel.Bind<IFilterTransformer>().To<FilterTransformerImpl>();
            Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServerImpl>();
        }
    }
}