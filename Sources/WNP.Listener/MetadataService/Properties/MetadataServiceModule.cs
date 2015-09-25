using AMSLLC.Listener.MetadataService.Impl;
using AMSLLC.Listener.MetadataService.Mapping;
using AMSLLC.Listener.MetadataService.Model;
using AMSLLC.Listener.Persistence;
using Ninject.Modules;
using Ninject.Web.Common;

namespace AMSLLC.Listener.MetadataService.Properties
{
    public class MetadataServiceModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<MetadataDbContext>().ToSelf().
                InRequestScope().WithConstructorArgument("connectionStringName", "WNPDatabase");

            Kernel.Bind<IMetadataService>().To<MetadataServiceImpl>().
                InTransientScope();

            Mappers.Register(typeof(WNPMetadataEntry), new WNPMetadataMapping());
        }
    }
}