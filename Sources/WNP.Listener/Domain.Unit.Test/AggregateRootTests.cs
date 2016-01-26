namespace AMSLLC.Listener.Domain.Unit.Test
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for AggregateRootTests
    /// </summary>
    [TestClass]
    public class AggregateRootTests
    {
        [TestMethod]
        public void DomainEventsShouldBeEmptyForNewClass()
        {
            var testAggregateRoot = new AggregateRootTest();

            Assert.AreNotEqual(null, testAggregateRoot.DomainEvents);
            Assert.AreEqual(0, testAggregateRoot.DomainEvents.Count);
        }

        /// <summary>
        /// Class used for testing abstract <see cref="AggregateRoot{TId}"/> implementation
        /// </summary>
        private class AggregateRootTest : AggregateRoot<int>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="AggregateRootTest"/> class.
            /// </summary>
            public AggregateRootTest()
            {
            }

            /// <summary>
            /// Restores objects state from provided memento.
            /// </summary>
            /// <param name="memento">The memento.</param>
            /// <exception cref="System.NotImplementedException"></exception>
            protected override void SetMemento(IMemento memento)
            {
                throw new NotImplementedException();
            }
        }
    }
}
