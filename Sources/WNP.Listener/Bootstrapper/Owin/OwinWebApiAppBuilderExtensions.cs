// <copyright file="OwinWebApiAppBuilderExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using global::Owin;

    using Ninject.Web.Common.OwinHost;

    /// <summary>
    /// The OWIN web API app builder extensions.
    /// </summary>
    public static class OwinWebApiAppBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/> and Adds Web API component to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> used to configure the endpoint.</param>
        /// <returns>The application builder.</returns>
        public static IAppBuilder UseNinjectWebApi(this IAppBuilder app, HttpConfiguration configuration)
        {
            AddOwinModuleToBootstrapper(app, configuration);

            return app.UseWebApi(configuration);
        }

        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/> and Adds Web API component to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="httpServer">The http server.</param>
        /// <returns>The application builder.</returns>
        public static IAppBuilder UseNinjectWebApi(this IAppBuilder app, HttpServer httpServer)
        {
            if (httpServer == null)
            {
                throw new ArgumentNullException("httpServer");
            }

            AddOwinModuleToBootstrapper(app, httpServer.Configuration);

            return app.UseWebApi(httpServer);
        }

        /// <summary>
        /// Adds the <see cref="OwinWebApiModule"/> to the <see cref="OwinBootstrapper"/>
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="configuration">The <see cref="HttpConfiguration"/> used to configure the endpoint.</param>
        private static void AddOwinModuleToBootstrapper(IAppBuilder app, HttpConfiguration configuration)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var bootstrapper =
                app.Properties.Where(element => element.Key.Equals(OwinAppBuilderExtensions.NinjectOwinBootstrapperKey))
                    .Select(x => x.Value)
                    .OfType<OwinBootstrapper>()
                    .Single();

            bootstrapper.AddModule(new OwinWebApiModule(configuration));
        }
    }
}