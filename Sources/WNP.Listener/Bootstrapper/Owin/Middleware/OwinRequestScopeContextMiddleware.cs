// <copyright file="OwinRequestScopeContextMiddleware.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>
    /// Owin middleware for single request scope
    /// </summary>
    public class OwinRequestScopeContextMiddleware : OwinMiddleware
    {
        private readonly OwinMiddleware next;
        private readonly bool threadSafeItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinRequestScopeContextMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public OwinRequestScopeContextMiddleware(OwinMiddleware next)
            : this(next, threadSafeItem: false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinRequestScopeContextMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        /// <param name="threadSafeItem">if set to <c>true</c> [thread safe item].</param>
        public OwinRequestScopeContextMiddleware(OwinMiddleware next, bool threadSafeItem)
            : base(next)
        {
            this.next = next;
            this.threadSafeItem = threadSafeItem;
        }

        /// <inheritdoc/>
        public async override Task Invoke(IOwinContext context)
        {
            var scopeContext = new OwinRequestScopeContext(context, this.threadSafeItem);
            OwinRequestScopeContext.Current = scopeContext;

            try
            {
                await this.next.Invoke(context);
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
}