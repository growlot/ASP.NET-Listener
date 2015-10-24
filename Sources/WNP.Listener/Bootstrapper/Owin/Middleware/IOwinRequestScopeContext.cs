// <copyright file="IOwinRequestScopeContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin.Middleware
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using Microsoft.Owin;

    /// <summary>
    /// RequestScopeContext for Owin.
    /// </summary>
    public interface IOwinRequestScopeContext
    {
        /// <summary>
        /// Raw Owin Environment dictionary.
        /// </summary>
        IDictionary<string, object> Environment { get; }

        /// <summary>
        /// Gets a key/value collection that can be used to organize and share data during an HTTP request.
        /// </summary>
        IDictionary<string, object> Items { get; }

        /// <summary>
        /// Gets the Owin environment context.
        /// </summary>
        /// <value>
        /// The Owin environment context.
        /// </value>
        IOwinContext Context { get; }

        /// <summary>
        /// Gets the initial timestamp of the current HTTP request.
        /// </summary>
        DateTime Timestamp { get; }

        /// <summary>
        /// <para>Enables an object's Dispose method to be called when the request completed.</para>
        /// <para>Return value is subscription token. If calle token.Dispose() then canceled register.</para>
        /// </summary>
        /// <param name="target">IDisposable item.</param>
        /// <returns>The dispose subscription token.</returns>
        IDisposable DisposeOnPipelineCompleted(IDisposable target);
    }

    /// <summary>
    /// Implements <see cref="IOwinRequestScopeContext"/>
    /// </summary>
    [Serializable]
    public class OwinRequestScopeContext : MarshalByRefObject, IOwinRequestScopeContext
    {
        private const string CallContextKey = "owin.rscopectx";

        private readonly DateTime utcTimestamp = DateTime.UtcNow;

        private readonly List<UnsubscribeDisposable> disposables;
        private readonly ConcurrentQueue<UnsubscribeDisposable> disposablesThreadsafeQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinRequestScopeContext"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="threadSafeItem">if set to <c>true</c> [thread safe item].</param>
        public OwinRequestScopeContext(IOwinContext context, bool threadSafeItem)
        {
            this.utcTimestamp = DateTime.UtcNow;
            this.Environment = context.Environment;
            this.Context = context;

            if (threadSafeItem)
            {
                this.Items = new ConcurrentDictionary<string, object>();
                this.disposablesThreadsafeQueue = new ConcurrentQueue<UnsubscribeDisposable>();
            }
            else
            {
                this.Items = new Dictionary<string, object>();
                this.disposables = new List<UnsubscribeDisposable>();
            }
        }

        /// <summary>
        /// Gets or sets the IOwinRequestScopeContext object for the current HTTP request.
        /// </summary>
        public static IOwinRequestScopeContext Current
        {
            get { return (IOwinRequestScopeContext)CallContext.LogicalGetData(CallContextKey); }
            set { CallContext.LogicalSetData(CallContextKey, value); }
        }

        /// <inheritdoc/>
        public IDictionary<string, object> Environment { get; private set; }

        /// <summary>
        /// Gets a key/value collection that can be used to organize and share data during an HTTP request.
        /// </summary>
        public IDictionary<string, object> Items { get; private set; }

        /// <inheritdoc/>
        public IOwinContext Context { get; }

        /// <inheritdoc/>
        public DateTime Timestamp
        {
            get { return this.utcTimestamp.ToLocalTime(); }
        }

        /// <inheritdoc/>
        public IDisposable DisposeOnPipelineCompleted(IDisposable target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            var token = new UnsubscribeDisposable(target);
            if (this.disposables != null)
            {
                this.disposables.Add(token);
            }
            else
            {
                this.disposablesThreadsafeQueue.Enqueue(token);
            }

            return token;
        }

        internal static void FreeContextSlot()
        {
            CallContext.FreeNamedDataSlot(CallContextKey);
        }

        internal void Complete()
        {
            var exceptions = new List<Exception>();
            try
            {
                if (this.disposables != null)
                {
                    foreach (var item in this.disposables)
                    {
                        item.CallTargetDispose();
                    }
                }
                else
                {
                    UnsubscribeDisposable target;
                    while (this.disposablesThreadsafeQueue.TryDequeue(out target))
                    {
                        target.CallTargetDispose();
                    }
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
            finally
            {
                if (exceptions.Any())
                {
                    throw new AggregateException("failed on disposing", exceptions);
                }
            }
        }
    }
}