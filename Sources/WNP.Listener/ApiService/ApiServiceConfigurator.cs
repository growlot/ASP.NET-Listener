// //-----------------------------------------------------------------------
// // <copyright file="ApiServiceConfigurator.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System;
    using System.Data.Entity;
    using System.Linq.Expressions;
    using System.Net.Http.Formatting;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Extensions;
    using System.Web.Http.OData.Query;
    using System.Web.OData.Extensions;
    using System.Web.OData.Builder;
    using System.Web.OData.Routing;
    using AsyncPoco;
    using Persistence.Listener;
    using AllowedQueryOptions = System.Web.OData.Query.AllowedQueryOptions;


    public class ApiServiceConfigurator
    {
        public void Configure(HttpConfiguration config)
        {


            
            //config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
            

            //config.AddODataQueryFilter(new EnableQueryAttribute { AllowedQueryOptions = AllowedQueryOptions.All });
            //config.MapHttpAttributeRoutes();
        }


        
    }
}