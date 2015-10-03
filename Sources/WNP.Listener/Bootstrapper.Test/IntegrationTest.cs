// //-----------------------------------------------------------------------
// // <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using ApplicationService;
    using ApplicationService.Impl;
    using Communication;
    using Core;
    using Core.Ninject;
    using Domain;
    using Domain.Listener.Transaction;
    using global::Owin;
    using Microsoft.Owin.Hosting;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Repository;

    [TestClass]
    public class IntegrationTest
    {
        public async void OpenTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                //HttpResponseMessage response = await server.HttpClient.PostAsync("/");
                // TODO: Validate response
            }
        }
    }
}