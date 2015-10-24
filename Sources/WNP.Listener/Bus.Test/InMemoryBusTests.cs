//-----------------------------------------------------------------------
// <copyright file="EventRegisterTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Bus.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Domain;

    /// <summary>
    /// Tests entity base class
    /// </summary>
    [TestClass]
    public class EventRegisterTests
    {
        private IDomainEventBus domainEventBus = new InMemoryBus();

        /// <summary>
        /// Handler registration works and handler is called when event is raised
        /// </summary>
        [TestMethod]
        public void DomainEventHandlerSubscriptionWorksAndHandlerIsCalledWhenEventIsPublished()
        {
            this.domainEventBus.Subscribe<TestEvent>(testEvent => TestEventHandler.Handler(testEvent));
            TestEvent eventData = new TestEvent(1);

            this.domainEventBus.Publish(eventData);
            Assert.AreEqual(1, TestEventHandler.CallCounter);
        }

        /// <summary>
        /// Exception is thrown when event data is not provided.
        /// </summary>
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionThrownWhenEventDataNotProvided()
        {
            TestEvent eventData = null;

            this.domainEventBus.Publish(eventData);
        }

        /// <summary>
        /// Test class for event handling
        /// </summary>
        private static class TestEventHandler
        {
            /// <summary>
            /// The handler
            /// </summary>
            private static readonly Action<TestEvent> HandlerDefinition = (eventData) => callCounter++;

            /// <summary>
            /// The call counter
            /// </summary>
            private static int callCounter = 0;

            /// <summary>
            /// Gets the handler.
            /// </summary>
            /// <value>
            /// The handler.
            /// </value>
            public static Action<TestEvent> Handler
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
        }

        /// <summary>
        /// Test class implementing <see cref="IDomainEvent"/> interface
        /// </summary>
        private class TestEvent : IDomainEvent
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestEvent"/> class.
            /// </summary>
            /// <param name="id">The identifier.</param>
            public TestEvent(int id)
            {
                this.Id = id;
            }

            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Just test code.")]
            public int Id { get; private set; }
        }
    }
}
