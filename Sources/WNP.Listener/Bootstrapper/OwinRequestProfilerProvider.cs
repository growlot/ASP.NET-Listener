using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using AMSLLC.Listener.Bootstrapper.Owin.Middleware;
using Microsoft.Owin;
using StackExchange.Profiling;

namespace AMSLLC.Listener.Bootstrapper
{
    public class OwinRequestProfilerProvider : BaseProfilerProvider
    {
        private const string CacheKey = ":mini-profiler:";

        /// <summary>
        /// Gets the currently running MiniProfiler for the current HttpContext; null if no MiniProfiler was <see cref="M:StackExchange.Profiling.WebRequestProfilerProvider.Start(System.String)"/>ed.
        /// 
        /// </summary>
        private MiniProfiler Current
        {
            get { return OwinRequestScopeContext.Current.Context?.Get<MiniProfiler>(":mini-profiler:"); }
            set { OwinRequestScopeContext.Current.Context?.Set(":mini-profiler:", value); }
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="T:StackExchange.Profiling.WebRequestProfilerProvider"/> class.
        ///             Public constructor.  This also registers any UI routes needed to display results
        /// 
        /// </summary>
        public OwinRequestProfilerProvider()
        {
            
        }

        /// <summary>
        /// Starts a new MiniProfiler and associates it with the current <see cref="P:System.Web.HttpContext.Current"/>.
        /// 
        /// </summary>
        public override MiniProfiler Start(string sessionName = null)
        {
            var current = OwinRequestScopeContext.Current.Context;
/*
            if (current == null || current.Request.AppRelativeCurrentExecutionFilePath == null)
                return (MiniProfiler)null;
*/

            Uri url = current.Request.Uri;
/*
            string str1 = current.Request.AppRelativeCurrentExecutionFilePath.Substring(1).ToUpperInvariant();
            foreach (string str2 in MiniProfiler.Settings.IgnoredPaths ?? new string[0])
            {
                if (str1.Contains((str2 ?? string.Empty).ToUpperInvariant()))
                    return (MiniProfiler)null;
            }
            if (current.Request.Path.StartsWith(VirtualPathUtility.ToAbsolute(MiniProfiler.Settings.RouteBasePath), StringComparison.InvariantCultureIgnoreCase))
                return (MiniProfiler)null;
*/

            MiniProfiler profiler = new MiniProfiler(sessionName ?? url.OriginalString);
            this.Current = profiler;
            SetProfilerActive(profiler);
//            profiler.User = OwinRequestProfilerProvider.Settings.UserProvider.GetUser(current.Request);
            return profiler;
        }

        /// <summary>
        /// Starts a new MiniProfiler and associates it with the current <see cref="P:System.Web.HttpContext.Current"/>.
        /// 
        /// </summary>
        [Obsolete("Please use the Start(string sessionName) overload instead of this one. ProfileLevel is going away.")]
        public override MiniProfiler Start(ProfileLevel level, string sessionName = null)
        {
            var current = OwinRequestScopeContext.Current.Context;

/*
            if (current == null || current.Request.AppRelativeCurrentExecutionFilePath == null)
                return (MiniProfiler)null;
*/
            Uri url = current.Request.Uri;
/*
            string str1 = current.Request.AppRelativeCurrentExecutionFilePath.Substring(1).ToUpperInvariant();
            foreach (string str2 in MiniProfiler.Settings.IgnoredPaths ?? new string[0])
            {
                if (str1.Contains((str2 ?? string.Empty).ToUpperInvariant()))
                    return (MiniProfiler)null;
            }
            if (current.Request.Path.StartsWith(VirtualPathUtility.ToAbsolute(MiniProfiler.Settings.RouteBasePath), StringComparison.InvariantCultureIgnoreCase))
                return (MiniProfiler)null;
*/

            MiniProfiler profiler = new MiniProfiler(sessionName ?? url.OriginalString, level);
            this.Current = profiler;
            SetProfilerActive(profiler);
//            profiler.User = WebRequestProfilerProvider.Settings.UserProvider.GetUser(current.Request);
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
                return;
            MiniProfiler current2 = this.Current;
            if (current2 == null || !StopProfiler(current2))
                return;
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
                            MiniProfiler.Settings.Storage.SetViewed(current2.User, id);
                    }
                    if (unviewedIds == null || unviewedIds.Count <= 0)
                        return;

                    response.Headers.Append("X-MiniProfiler-Ids", ((object)unviewedIds).ToJson());
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Makes sure 'profiler' has a Name, pulling it from route data or url.
        /// 
        /// </summary>
        private static void EnsureName(MiniProfiler profiler, IOwinRequest request)
        {
            if (!string.IsNullOrWhiteSpace(profiler.Name))
                return;

            profiler.Name = request.Uri.AbsolutePath ?? string.Empty;
            if (profiler.Name.Length <= 50)
                return;

            profiler.Name = profiler.Name.Remove(50);
        }

        /// <summary>
        /// Returns the current profiler
        /// 
        /// </summary>
        public override MiniProfiler GetCurrentProfiler()
        {
            return this.Current;
        }

        /// <summary>
        /// WebRequestProfilerProvider specific configurations
        /// 
        /// </summary>
        public static class Settings
        {
            private static IUserProvider provider = (IUserProvider)new IpAddressIdentity();

            /// <summary>
            /// Provides user identification for a given profiling request.
            /// 
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

    internal static class ExtensionMethods
    {
        internal static string ToJson(this object o)
        {
            if (o != null)
                return new JavaScriptSerializer().Serialize(o);
            return null;
        }
    }
}