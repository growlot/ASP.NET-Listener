using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.MetadataService.Mapping;
using AMSLLC.Listener.MetadataService.Model;
using AMSLLC.Listener.Persistence;
using Ninject.Modules;
using Ninject.Web.Common;

namespace AMSLLC.Listener.MetadataService.Properties
{
    using System;
    using Serilog;

    public class MetadataServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind<MetadataDbContext>().ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");

            this.Kernel.Bind<IMetadataService>().To<MetadataServiceImpl>()
                .InTransientScope();

            this.Kernel.Bind<IActionConfigurator>().To<ActionConfiguratorImpl>()
                .InSingletonScope();

            try
            {
                Mappers.Register(typeof(WNPMetadataEntry), new WNPMetadataMapping());
            }
            catch (ArgumentException)
            {
                Log.Error("Mapper Error (possible duplicate registration). No Rethrow");
            }
        }
    }
}