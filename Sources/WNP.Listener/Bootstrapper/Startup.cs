using System.Web.Http;
using AMSLLC.Listener.Bootstrapper.Owin.Middleware;
using AMSLLC.Listener.MetadataService.Properties;
using AMSLLC.Listener.ODataService;
using AMSLLC.Listener.ODataService.Properties;
using Microsoft.Owin.Diagnostics;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using StackExchange.Profiling;

namespace AMSLLC.Listener.Bootstrapper
{
    public class Startup
    {
        private readonly StandardKernel _kernel = new StandardKernel();

        public void Configuration(IAppBuilder app)
        {
            InitKernel();
            InitOwinHost(app);
            InitProfiler();
        }

        private void InitKernel()
        {
            _kernel.Load<ODataServiceModule>();
            _kernel.Load<MetadataServiceModule>();
        }

        private void InitProfiler()
        {
            MiniProfiler.Settings.ProfilerProvider = new OwinRequestProfilerProvider();
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(true);
        }

        private void InitOwinHost(IAppBuilder app)
        {
            var config = _kernel.Get<HttpConfiguration>();

            var odataServiceConfigurator = _kernel.Get<ODataServiceConfigurator>();
            odataServiceConfigurator.Configure(config);

            app.UseRequestScopeContext();
            app.UseErrorPage(ErrorPageOptions.ShowAll);
            app.UseNinjectMiddleware(() => _kernel).UseNinjectWebApi(config);

            config.EnsureInitialized();
        }
    }
}