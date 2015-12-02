// <copyright file="OwinRequestProfilerProvider.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Owin;
    using Owin.Middleware;
    using StackExchange.Profiling;

    /// <summary>
    /// Implements <see cref="BaseProfilerProvider"/> for Owin requests.
    /// </summary>
    [CLSCompliant(false)]
    public class OwinRequestProfilerProvider : BaseProfilerProvider
    {
        private const string CacheKey = ":mini-profiler:";

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinRequestProfilerProvider"/> class.
        /// </summary>
        public OwinRequestProfilerProvider()
        {
        }

        /// <summary>
        /// Gets or sets the currently running MiniProfiler for the current HttpContext; null if no MiniProfiler was <see cref="M:StackExchange.Profiling.WebRequestProfilerProvider.Start(System.String)"/>ed.
        /// </summary>
        /// <value>
        /// The current profiler.
        /// </value>
        private MiniProfiler Current
        {
            get { return OwinRequestScopeContext.Current.Context?.Get<MiniProfiler>(":mini-profiler:"); }
            set { OwinRequestScopeContext.Current.Context?.Set(":mini-profiler:", value); }
        }

        /// <summary>
        /// Starts a new MiniProfiler and associates it with the current <see cref="P:System.Web.HttpContext.Current" />.
        /// </summary>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>The profiler.</returns>
        public override MiniProfiler Start(string sessionName = null)
        {
            var current = OwinRequestScopeContext.Current.Context;

            Uri url = current.Request.Uri;

            MiniProfiler profiler = new MiniProfiler(sessionName ?? url.OriginalString);
            this.Current = profiler;
            SetProfilerActive(profiler);

            // profiler.User = OwinRequestProfilerProvider.Settings.UserProvider.GetUser(current.Request);
            return profiler;
        }

        /// <summary>
        /// Starts a new MiniProfiler and associates it with the current <see cref="P:System.Web.HttpContext.Current" />.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="sessionName">Name of the session.</param>
        /// <returns>The profiler instance</returns>
        [Obsolete("Please use the Start(string sessionName) overload instead of this one. ProfileLevel is going away.")]
        public override MiniProfiler Start(ProfileLevel level, string sessionName = null)
        {
            var current = OwinRequestScopeContext.Current.Context;

            Uri url = current.Request.Uri;

            MiniProfiler profiler = new MiniProfiler(sessionName ?? url.OriginalString, level);
            this.Current = profiler;
            SetProfilerActive(profiler);
            return profiler;
        }

        /// <summary>
        /// Ends the current profiling session, if one exists.
        ///
        /// </summary>
        /// <param name="discardResults">When true, clears the <see cref="P:StackExchange.Profiling.MiniProfiler.Current"/> for this HttpContext, allowing profiling to
        ///             be prematurely stopped and discarded. Useful for when a specific route does not need to be profiled.
        ///             </param>
        public override void Stop(bool discardResults)
        {
            var current1 = OwinRequestScopeContext.Current.Context;
            if (current1 == null)
            {
                return;
            }

            MiniProfiler current2 = this.Current;
            if (current2 == null || !StopProfiler(current2))
            {
                return;
            }

            if (discardResults)
            {
                this.Current = (MiniProfiler)null;
            }
            else
            {
                var request = current1.Request;
                var response = current1.Response;

                EnsureName(current2, request);
                SaveProfiler(current2);
                try
                {
                    List<Guid> unviewedIds = MiniProfiler.Settings.Storage.GetUnviewedIds(current2.User);
                    if (unviewedIds != null && unviewedIds.Count > MiniProfiler.Settings.MaxUnviewedProfiles)
                    {
                        foreach (Guid id in Enumerable.Take<Guid>((IEnumerable<Guid>)unviewedIds, unviewedIds.Count - MiniProfiler.Settings.MaxUnviewedProfiles))
                        {
                            MiniProfiler.Settings.Storage.SetViewed(current2.User, id);
                        }
                    }

                    if (unviewedIds == null || unviewedIds.Count <= 0)
                    {
                        return;
                    }

                    response.Headers.Append("X-MiniProfiler-Ids", ((object)unviewedIds).ToJson());
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Returns the current profiler
        /// </summary>
        /// <returns>Current profiler.</returns>
        public override MiniProfiler GetCurrentProfiler()
        {
            return this.Current;
        }

        /// <summary>
        /// Makes sure 'profiler' has a Name, pulling it from route data or url.
        /// </summary>
        /// <param name="profiler">The profiler.</param>
        /// <param name="request">The request.</param>
        private static void EnsureName(MiniProfiler profiler, IOwinRequest request)
        {
            if (!string.IsNullOrWhiteSpace(profiler.Name))
            {
                return;
            }

            profiler.Name = request.Uri.AbsolutePath ?? string.Empty;
            if (profiler.Name.Length <= 50)
            {
                return;
            }

            profiler.Name = profiler.Name.Remove(50);
        }

        /// <summary>
        /// WebRequestProfilerProvider specific configurations
        ///
        /// </summary>
        public static class Settings
        {
            private static IUserProvider provider = (IUserProvider)new IpAddressIdentity();

            /// <summary>
            /// Gets or sets user identification for a given profiling request.
            /// </summary>
            public static IUserProvider UserProvider
            {
                get
                {
                    return provider;
                }

                set
                {
                    provider = value;
                }
            }
        }
    }
}