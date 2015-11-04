// <copyright file="TestDependencyInjectionAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Core;
    using Core.Ninject;
    using Ninject;
    using Ninject.Activation.Caching;
    using Ninject.MockingKernel;
    using Ninject.MockingKernel.Moq;
    using Ninject.Syntax;

    public class TestDependencyInjectionAdapter : NinjectDependencyInjectionAdapter
    {
        private Dictionary<Type, object> _reboundInstances = new Dictionary<Type, object>();

        public IBindingWhenInNamedWithOrOnSyntax<TType> Rebind<TType>(TType to)
        {
            this._reboundInstances.Add(typeof(TType), to);
            return this.Kernel.Rebind<TType>().ToConstant(to);
        }

        public void Reset()
        {
            //foreach (var source in this.Kernel.GetModules().Where(m => m is DependencyWiring))
            //    // only for modules I created)
            //{
            //    this.Kernel.Unload(source.Name);
            //}

            //.ForEach(m => this.Kernel.Unload(m.Name));
            //this.Kernel.Components.Get<ICache>().Clear();
            foreach (var reboundInstance in _reboundInstances)
            {
                this.Kernel.Release(reboundInstance);
            }
            _reboundInstances.Clear();
        }

        public void Dispose()
        {
            this.Kernel.Dispose();
        }
    }
}