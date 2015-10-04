// //-----------------------------------------------------------------------
// // <copyright file="ApiServiceConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System.Net.Http.Formatting;
    using System.Web.Http;

    public class ApiServiceConfigurator
    {
        public void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());
        }
    }
}