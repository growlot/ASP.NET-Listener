using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Test
{
    using AMSLLC.Listener.ApplicationService;
    using AMSLLC.Listener.Core;
    using AMSLLC.Listener.Repository;
    using Ninject;

    public class TestScopeContainerInitializer : IDependencyInjectionModule
    {
        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Initialize(object container)
        {
            var kernel = (StandardKernel)container;
            kernel.Bind<IRepositoryManager>().To<RepositoryManager>().InSingletonScope();
        }
    }
}
