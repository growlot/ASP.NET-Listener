using AMSLLC.Listener.MetadataService;
using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.ODataService.Services;
using AMSLLC.Listener.ODataService.Services.FilterTransformer;
using AMSLLC.Listener.ODataService.Services.Impl;
using AMSLLC.Listener.ODataService.Services.Impl.FilterTransformer;
using Ninject.Modules;
using Ninject.Web.Common;

namespace AMSLLC.Listener.ODataService.Properties
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

            Kernel.Bind<IAutoConvertor>().To<AutoConvertorImpl>();
            Kernel.Bind<IFilterTransformer>().To<FilterTransformerImpl>();
            Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServerImpl>();
        }
    }
}