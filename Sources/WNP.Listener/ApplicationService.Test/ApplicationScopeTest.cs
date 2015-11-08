// <copyright file="ApplicationScopeTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace ApplicationService.Test
{
    using AMSLLC.Listener.ApplicationService;
    using AMSLLC.Listener.ApplicationService.Implementations;
    using AMSLLC.Listener.Core;
    using AMSLLC.Listener.Core.Ninject.Test;
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class ApplicationScopeTest
    {
        [TestMethod]
        public void TestApplicationScope()
        {
            var di = new TestDependencyInjectionAdapter();
            di.Kernel.Bind<IDependencyInjectionModule>().To<TestScopeContainerInitializer>().InSingletonScope().Named("ApplicationScopeModule");
            di.Kernel.Bind<IDomainBuilder>().To<DomainBuilder>();
            di.Kernel.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>();
            ApplicationIntegration.SetDependencyInjectionResolver(di);

            var appScope1 = new Mock<ApplicationServiceScope>()
            {
                CallBase = true
            };

            var appScopeMock1 = appScope1.As<IApplicationServiceScope>();

            var tr1 = appScopeMock1.Object.RepositoryBuilder.Create<ITransactionRepository>();
            var tr2 = appScopeMock1.Object.RepositoryBuilder.Create<ITransactionRepository>();

            Assert.AreEqual(tr1, tr2);

            var appScope2 = new Mock<ApplicationServiceScope>()
            {
                CallBase = true
            };

            var appScopeMock2 = appScope2.As<IApplicationServiceScope>();

            var tr3 = appScopeMock2.Object.RepositoryBuilder.Create<ITransactionRepository>();
            var tr4 = appScopeMock2.Object.RepositoryBuilder.Create<ITransactionRepository>();

            Assert.AreEqual(tr3, tr4);

            Assert.AreNotEqual(tr1, tr3);
            Assert.AreNotEqual(tr2, tr4);
        }
    }
}