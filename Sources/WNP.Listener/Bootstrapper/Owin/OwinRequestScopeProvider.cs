// <copyright file="OwinRequestScopeProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin
{
    using Middleware;

    using Ninject.Activation;
    using Ninject.Components;
    using Ninject.Web.WebApi;

    /// <summary>
    /// Ninject RequestScopeProvider that uses OwinRequestContext instead of non-existant in OwinSelfHost HttpContext.
    /// </summary>
    public class OwinRequestScopeProvider : NinjectComponent, IWebApiRequestScopeProvider
    {
        /// <summary>
        /// Gets the request scope for the current activation context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>The request scope.</returns>
        public object GetRequestScope(IContext context) => OwinRequestScopeContext.Current;
    }
}