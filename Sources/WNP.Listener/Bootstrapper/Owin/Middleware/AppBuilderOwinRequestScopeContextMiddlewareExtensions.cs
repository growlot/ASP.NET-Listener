// <copyright file="AppBuilderOwinRequestScopeContextMiddlewareExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    using global::Owin;

    /// <summary>
    ///  Custom extensions for <see cref="IAppBuilder"/>
    /// </summary>
    public static class AppBuilderOwinRequestScopeContextMiddlewareExtensions
    {
        /// <summary>
        /// Use OwinRequestScopeContextMiddleware.
        /// </summary>
        /// <param name="app">Owin app.</param>
        /// <param name="isThreadsafeItem">OwinRequestScopeContext.Item is threadsafe or not. Default is threadsafe.</param>
        /// <returns>The updated Owin application.</returns>
        public static IAppBuilder UseRequestScopeContext(this IAppBuilder app, bool isThreadsafeItem = true)
        {
            return app.Use(typeof(OwinRequestScopeContextMiddleware), isThreadsafeItem);
        }
    }
}