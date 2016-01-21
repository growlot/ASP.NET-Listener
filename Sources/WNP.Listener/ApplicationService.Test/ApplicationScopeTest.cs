// <copyright file="ApplicationScopeTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Test
{
    using Core;
    using Core.Ninject.Test;
    using Domain;
    using Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Repository.Listener;

    [TestClass]
    public class ApplicationScopeTest
    {
        [TestMethod]
        public void TestApplicationScope()
        {
            var di = new TestDependencyInjectionAdapter();
            di.Kernel.Bind<IDependencyInjectionModule>()
                .To<TestScopeContainerInitializer>()
                .Named("ApplicationScopeModule");
            di.Kernel.Bind<IDomainBuilder>().To<DomainBuilder>().InSingletonScope();
            di.Kernel.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
            ApplicationIntegration.SetDependencyInjectionResolver(di);

            di.Rebind<IDependencyInjectionModule>(new TestScopeContainerInitializer())
                .Named("ApplicationScopeModule");

            var db1 = di.ResolveType<IDomainBuilder>();

            var appScope1 = new Mock<ApplicationServiceScope>()
            {
                CallBase = true
            };

            var appScopeMock1 = appScope1.As<IApplicationServiceScope>();

            var tr1 = appScopeMock1.Object.RepositoryBuilder.Create<ITransactionRepository>();
            var tr2 = appScopeMock1.Object.RepositoryBuilder.Create<ITransactionRepository>();

            Assert.AreEqual(tr1, tr2);
            Assert.AreEqual(db1, appScopeMock1.Object.DomainBuilder);

            var appScope2 = new Mock<ApplicationServiceScope>()
            {
                CallBase = true
            };

            var appScopeMock2 = appScope2.As<IApplicationServiceScope>();

            var tr3 = appScopeMock2.Object.RepositoryBuilder.Create<ITransactionRepository>();
            var tr4 = appScopeMock2.Object.RepositoryBuilder.Create<ITransactionRepository>();

            Assert.AreEqual(tr3, tr4);
            Assert.AreEqual(db1, appScopeMock2.Object.DomainBuilder);

            Assert.AreNotEqual(tr1, tr3);
            Assert.AreNotEqual(tr2, tr4);

            appScopeMock1.Object.Dispose();

            var db3 = di.ResolveType<IDomainBuilder>();
            Assert.IsNotNull(db3, "Cannot resolve instance from root DI");
            Assert.AreEqual(db1, db3);
            Assert.IsNull(appScopeMock1.Object.DomainBuilder);
            Assert.IsNotNull(appScopeMock2.Object.DomainBuilder);
        }
    }
}