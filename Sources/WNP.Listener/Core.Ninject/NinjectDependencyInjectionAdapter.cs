namespace AMSLLC.Core.Ninject
{
    using System;
    using global::Ninject;

    public class NinjectDependencyInjectionAdapter : IDependencyInjectionAdapter
    {
        public StandardKernel Kernel { get; } = new StandardKernel();

        public TType ResolveType<TType>()
        {
            return this.Kernel.Get<TType>();
        }

        public object ResolveType(Type type)
        {
            return this.Kernel.Get(type);
        }

        public TType ResolveNamed<TType>(string name)
        {
            return this.Kernel.Get<TType>(name);
        }

        public void Initialize(Action<StandardKernel> action)
        {
            action(this.Kernel);
        }
    }
}
