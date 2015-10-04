// //-----------------------------------------------------------------------
// // <copyright file="ApiServiceConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using ApplicationService;
    using Communication.Jms;
    using Serilog;

    public class ApiServiceConfigurator
    {
        public void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            Log.Logger = log;

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());
        }
    }
}