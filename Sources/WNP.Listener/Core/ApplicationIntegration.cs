using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Core
{
    public static class ApplicationIntegration
    {
        public static IDependencyInjectionAdapter DependencyResolver { get; private set; }

        /// <summary>
        /// Sets the dependency injection resolver.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        public static void SetDependencyInjectionResolver(IDependencyInjectionAdapter adapter)
        {
            DependencyResolver = adapter;
        }
    }
}
