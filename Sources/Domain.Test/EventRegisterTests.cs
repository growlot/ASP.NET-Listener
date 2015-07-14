//-----------------------------------------------------------------------
// <copyright file="EventRegisterTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Test
{
    using System;
    using AMSLLC.Listener.Domain.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests entity base class
    /// </summary>
    [TestClass]
    public class EventRegisterTests
    {
        /// <summary>
        /// Handler registration works and handler is called when event is raised
        /// </summary>
        [TestMethod]
        public void HandlerRegistrationWorksAndHandlerIsCalledWhenEventIsRaised()
        {
            EventsRegister.Register<TestEvent>(testEvent => TestEventHandler.Handler(testEvent));
            TestEvent eventData = new TestEvent(1);

            EventsRegister.Raise(eventData);
            Assert.AreEqual(1, TestEventHandler.CallCounter);
        }

        /// <summary>
        /// Exception is thrown when event data is not provided.
        /// </summary>
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionThrownWhenEventDataNotProvided()
        {
            TestEvent eventData = null;

            EventsRegister.Raise(eventData);
        }

        /// <summary>
        /// Test class for event handling
        /// </summary>
        private static class TestEventHandler
        {
            /// <summary>
            /// The handler
            /// </summary>
            public static readonly Action<TestEvent> Handler = (eventData) => callCounter++;

            /// <summary>
            /// The call counter
            /// </summary>
            private static int callCounter = 0;

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
        /// Test class implementing <see cref="IEvent"/> interface
        /// </summary>
        private class TestEvent : IEvent
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
