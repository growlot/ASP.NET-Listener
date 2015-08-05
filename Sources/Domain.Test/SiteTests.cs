//-----------------------------------------------------------------------
// <copyright file="SiteTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Test
{
    using System;
    using AMSLLC.Listener.Domain.Events;
    using AMSLLC.Listener.Domain.Mementos;
    using AMSLLC.Listener.Domain.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the Site domain model
    /// </summary>
    [TestClass]
    public class SiteTests
    {
        /// <summary>
        /// Initializes this test instance.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            EventsRegister.Register<SiteAddressUpdated>(testEvent => SiteAddressUpdatedTestHandler.Handler(testEvent));
        }

        /// <summary>
        /// When the site address is updated correct event should be raised.
        /// </summary>
        [TestMethod]
        public void WhenSiteAddressIsUpdatedCorrectEventShouldBeRaised()
        {
            SiteAddressUpdatedTestHandler.ResetCallCounter();
            Site site = new Site();
            ((IOriginator)site).SetMemento(new SiteMemento(id: 1));
            PhysicalAddress address = new PhysicalAddress("US", "CA", "San Jose", "455 Larkspur Dr.", string.Empty, "92926");

            site.UpdateAddress(address);
            Assert.AreEqual(1, SiteAddressUpdatedTestHandler.CallCounter);
        }

        /// <summary>
        /// Sites address can't be null.
        /// </summary>
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SiteAddressCannotBeNull()
        {
            Site site = new Site();
            ((IOriginator)site).SetMemento(new SiteMemento(1));

            site.UpdateAddress(null);
        }

        /// <summary>
        /// Sites address can't have empty address line.
        /// </summary>
        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void SiteAddressCannotHaveEmptyAddressLine()
        {
            Site site = new Site();
            ((IOriginator)site).SetMemento(new SiteMemento(1));
            PhysicalAddress address = new PhysicalAddress("US", "CA", "San Jose", string.Empty, string.Empty, "92926");

            site.UpdateAddress(address);
        }

        /// <summary>
        /// Sites address can't have empty address line.
        /// </summary>
        [TestMethod]
        public void OnlyFirstAddressLineIsRequiredForSiteAddress()
        {
            SiteAddressUpdatedTestHandler.ResetCallCounter();
            Site site = new Site();
            ((IOriginator)site).SetMemento(new SiteMemento(1));
            PhysicalAddress address = new PhysicalAddress(string.Empty, string.Empty, string.Empty, "455 Larkspur Dr.", string.Empty, string.Empty);

            site.UpdateAddress(address);
            Assert.AreEqual(1, SiteAddressUpdatedTestHandler.CallCounter);
        }

        /// <summary>
        /// Test class for SiteAddressUpdated event handling
        /// </summary>
        private static class SiteAddressUpdatedTestHandler
        {
            /// <summary>
            /// The handler
            /// </summary>
            private static readonly Action<SiteAddressUpdated> HandlerDefinition = (eventData) => callCounter++;

            /// <summary>
            /// The call counter
            /// </summary>
            private static int callCounter = 1;

            /// <summary>
            /// Gets the handler.
            /// </summary>
            /// <value>
            /// The handler.
            /// </value>
            public static Action<SiteAddressUpdated> Handler
            {
                get
                {
                    return HandlerDefinition;
                }
            }

            /// <summary>
            /// Gets the call counter.
            /// </summary>
            /// <value>
            /// The call counter.
            /// </value>
            public static int CallCounter
            {
                get
                {
                    return callCounter;
                }
            }

            /// <summary>
            /// Resets the call counter.
            /// </summary>
            public static void ResetCallCounter()
            {
                callCounter = 0;
            }
        }
    }
}
