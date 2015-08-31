using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using Ninject.Web.Common;
using PetaPoco;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using StackExchange.Profiling.Helpers;
using WNP.Listener.MetadataService.Impl;
using WNP.Listener.MetadataService.Mapping;
using WNP.Listener.MetadataService.Model;

namespace WNP.Listener.MetadataService.Properties
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