//-----------------------------------------------------------------------
// <copyright file="ListenerWebServiceClientTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Client.Implementation.Unit.Test
{
    using System;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate.Criterion;

    /// <summary>
    /// Tests Listener web service client
    /// </summary>
    [TestClass]
    public class ListenerWebServiceClientTests
    {
        /// <summary>
        /// Exposes protected methods for testing
        /// </summary>
        private class ListenerWebServiceClientTester : ListenerWebServiceClient
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ListenerWebServiceClientTester"/> class.
            /// </summary>
            /// <param name="persistenceManager">The persistence manager.</param>
            public ListenerWebServiceClientTester(IPersistenceManager persistenceManager)
                : base(persistenceManager)
            { 
            }
        }
    }
}
