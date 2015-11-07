// <copyright file="NinjectDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Ninject
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using global::Ninject;
    using global::Ninject.Extensions.ChildKernel;
    using global::Ninject.Parameters;

    /// <summary>
    /// Implements <see cref="IDependencyInjectionAdapter"/> for Ninject
    /// </summary>
    public class NinjectDependencyInjectionAdapter : INinjectDependencyInjectionAdapter
    {
        private readonly Dictionary<string, IDependencyInjectionAdapter> namedScopes = new Dictionary<string, IDependencyInjectionAdapter>();
        private ReaderWriterLockSlim scopeLock = new ReaderWriterLockSlim();

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyInjectionAdapter"/> class.
        /// </summary>
        public NinjectDependencyInjectionAdapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyInjectionAdapter"/> class.
        /// </summary>
        /// <param name="childKernel">The child kernel.</param>
        protected NinjectDependencyInjectionAdapter(ChildKernel childKernel)
        {
            this.Kernel = childKernel;
        }

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        public StandardKernel Kernel { get; } = new StandardKernel();

        /// <inheritdoc/>
        public TType ResolveType<TType>(params KeyValuePair<string, object>[] arguments)
        {
            if (arguments != null && arguments.Any())
            {
                return
                    this.Kernel.Get<TType>(
                        arguments.Select(argument => new ConstructorArgument(argument.Key, argument.Value))
                            .Cast<IParameter>()
                            .ToArray());
            }

            return this.Kernel.Get<TType>();
        }

        /// <inheritdoc/>
        public object ResolveType(Type type)
        {
            return this.Kernel.Get(type);
        }

        /// <inheritdoc/>
        public TType ResolveNamed<TType>(string name)
        {
            return this.Kernel.Get<TType>(name);
        }

        /// <summary>
        /// Use scope for resolutions. Scope will be created if not exists
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="modules">The modules.</param>
        /// <returns>AMSLLC.Listener.Core.IDependencyInjectionAdapter.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Scope will be disposed separately")]
        public virtual IDependencyInjectionAdapter WithScope(
            string name,
            params string[] modules)
        {
            this.scopeLock.EnterUpgradeableReadLock();
            try
            {
                IDependencyInjectionAdapter result = null;
                if (this.namedScopes.TryGetValue(name, out result))
                {
                    return result;
                }
                else
                {
                    this.scopeLock.EnterWriteLock();
                    try
                    {
                        var kernel = new ChildKernel(this.Kernel);
                        result = this.CreateAdapter(kernel);
                        if (modules != null)
                        {
                            foreach (string module in modules)
                            {
                                var m = this.ResolveNamed<IDependencyInjectionModule>(module);
                                m.Initialize(kernel);
                            }
                        }

                        this.namedScopes.Add(name, result);
                    }
                    finally
                    {
                        this.scopeLock.ExitWriteLock();
                    }

                    return result;
                }
            }
            finally
            {
                this.scopeLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Disposes the scope.
        /// </summary>
        /// <param name="name">The name.</param>
        public void DisposeScope(
                    string name)
        {
            if (this.namedScopes.ContainsKey(name))
            {
                this.namedScopes[name]?.Dispose();
                this.namedScopes.Remove(name);
            }
        }

        /// <summary>
        /// Initializes specified action with Ninject dependency resolver.
        /// </summary>
        /// <param name="action">The action.</param>
        public void Initialize(Action<StandardKernel> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Can not initialize Ninject Kernel because action is not specified.");
            }

            action(this.Kernel);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the specified disposing.
        /// </summary>
        /// <param name="disposing">The disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var keys = new List<string>(this.namedScopes.Keys);
                foreach (string key in keys)
                {
                    this.DisposeScope(key);
                }

                this.namedScopes.Clear();
                this.Kernel.Dispose();
            }
        }

        /// <summary>
        /// Creates the child adapter.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <returns>AMSLLC.Listener.Core.IDependencyInjectionAdapter.</returns>
        protected virtual IDependencyInjectionAdapter CreateAdapter(
                    ChildKernel kernel)
        {
            return new NinjectDependencyInjectionAdapter(kernel);
        }
    }
}
