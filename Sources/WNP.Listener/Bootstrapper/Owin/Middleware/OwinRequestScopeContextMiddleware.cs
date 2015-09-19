using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    public class OwinRequestScopeContextMiddleware : OwinMiddleware
    {
        readonly OwinMiddleware next;
        readonly bool threadSafeItem;

        public OwinRequestScopeContextMiddleware(OwinMiddleware next)
            : this(next, threadSafeItem: false)
        {

        }

        public OwinRequestScopeContextMiddleware(OwinMiddleware next, bool threadSafeItem) : base(next)
        {
            this.next = next;
            this.threadSafeItem = threadSafeItem;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var scopeContext = new OwinRequestScopeContext(context, threadSafeItem);
            OwinRequestScopeContext.Current = scopeContext;

            try
            {
                await next.Invoke(context);
            }
            finally
            {
                try
                {
                    scopeContext.Complete();
                }
                finally
                {
                    OwinRequestScopeContext.FreeContextSlot();
                }
            }
        }
    }

    public static class AppBuilderOwinRequestScopeContextMiddlewareExtensions
    {
        /// <summary>
        /// Use OwinRequestScopeContextMiddleware.
        /// </summary>
        /// <param name="app">Owin app.</param>
        /// <param name="isThreadsafeItem">OwinRequestScopeContext.Item is threadsafe or not. Default is threadsafe.</param>
        /// <returns></returns>
        public static IAppBuilder UseRequestScopeContext(this IAppBuilder app, bool isThreadsafeItem = true)
        {
            return app.Use(typeof(OwinRequestScopeContextMiddleware), isThreadsafeItem);
        }
    }
}