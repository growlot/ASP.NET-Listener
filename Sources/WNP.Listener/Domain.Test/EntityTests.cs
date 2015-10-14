//-----------------------------------------------------------------------
// <copyright file="EntityTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests entity base class
    /// </summary>
    [TestClass]
    public class EntityTests
    {
        /// <summary>
        /// New and persisted entities should not be equal.
        /// </summary>
        [TestMethod]
        public void NewAndPersistedEntitiesShouldNotBeEqual()
        {
            TestEntity newEntity = new TestEntity("Some value");
            TestEntity persistedEntity = new TestEntity(1, "Some value");

            Assert.IsFalse(newEntity.Equals(persistedEntity));
        }

        /// <summary>
        /// Persisted entities should be equal if their identities match.
        /// </summary>
        [TestMethod]
        public void PersistedEntitiesShouldBeEqualIfTheirIdMatch()
        {
            TestEntity persistedEntity1 = new TestEntity(1, "Some value 1");
            TestEntity persistedEntity2 = new TestEntity(1, "Some value 2");

            Assert.IsTrue(persistedEntity1.Equals(persistedEntity2));
            Assert.IsTrue(persistedEntity1.Equals((object)persistedEntity2));
        }

        /// <summary>
        /// Persisted entities should have same hash if their identities match.
        /// </summary>
        [TestMethod]
        public void PersistedEntitiesShouldHaveSameHashIfTheirIdMatch()
        {
            TestEntity persistedEntity1 = new TestEntity(1, "Some value 1");
            TestEntity persistedEntity2 = new TestEntity(1, "Some value 2");

            Assert.AreEqual(persistedEntity1.GetHashCode(), persistedEntity2.GetHashCode());
        }

        /// <summary>
        /// New entities should not be equal.
        /// </summary>
        [TestMethod]
        public void NewEntitiesShouldNotBeEqual()
        {
            TestEntity newEntity1 = new TestEntity("Some value");
            TestEntity newEntity2 = new TestEntity("Some value");

            Assert.IsFalse(newEntity1.Equals(newEntity2));
        }

        /// <summary>
        /// Entity should not be equal to non entity.
        /// </summary>
        [TestMethod]
        public void EntityShouldNotBeEqualToNonentity()
        {
            TestEntity persistedEntity = new TestEntity(1, "Some value 1");
            TestNonentity nonentity = new TestNonentity(1, "Some value 1");

            Assert.IsFalse(persistedEntity.Equals(nonentity));
        }

        /// <summary>
        /// New entities should have different hashes.
        /// </summary>
        [TestMethod]
        public void NewEntitiesShouldHaveDifferentHashes()
        {
            TestEntity newEntity1 = new TestEntity("Some value");
            TestEntity newEntity2 = new TestEntity("Some value");

            Assert.AreNotEqual(newEntity1.GetHashCode(), newEntity2.GetHashCode());
        }

        /// <summary>
        /// Equal should work with nulls.
        /// </summary>
        [TestMethod]
        public void EqualShouldWorkWithNulls()
        {
            TestEntity newEntity1 = new TestEntity("Some value");
            TestEntity newEntity2 = null;

            Assert.IsFalse(newEntity1.Equals(newEntity2));
        }

        /// <summary>
        /// Same entity should be equal to itself.
        /// </summary>
        [TestMethod]
        public void SameNewEntityShouldBeEqualToItself()
        {
            TestEntity newEntity = new TestEntity("Some value");
            TestEntity persistedEntity = new TestEntity(1, "Some value");

            Assert.IsTrue(newEntity.Equals(newEntity));
            Assert.IsTrue(persistedEntity.Equals(persistedEntity));
        }

        /// <summary>
        /// Class that has same fields as <see cref="TestEntity"/>, but doesn't inherit from <see cref="Entity{T}"/>.
        /// </summary>
        private class TestNonentity
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestNonentity"/> class.
            /// For persisted entity simulation.
            /// </summary>
            /// <param name="id">The identifier.</param>
            /// <param name="field">The field.</param>
            public TestNonentity(int id, string field)
            {
                this.Id = id;
                this.Field = field;
            }

            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "It's just an imitation of how real entity would be set up.")]
            public int Id { get; private set; }

            /// <summary>
            /// Gets the field.
            /// </summary>
            /// <value>
            /// The field.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "It's just an imitation of how real entity would be set up.")]
            public string Field { get; private set; }
        }

        /// <summary>
        /// Class used for testing abstract Entity implementation
        /// </summary>
        private class TestEntity : Entity<int>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TestEntity"/> class.
            /// For persisted entity simulation.
            /// </summary>
            /// <param name="id">The identifier.</param>
            /// <param name="field">The field.</param>
            public TestEntity(int id, string field)
            {
                this.Id = id;
                this.Field = field;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TestEntity" /> class.
            /// </summary>
            /// <param name="field">The field.</param>
            public TestEntity(string field)
            {
                this.Field = field;
            }

            /// <summary>
            /// Gets the field.
            /// </summary>
            /// <value>
            /// The field.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "It's just an imitation of how real entity would be set up.")]
            public string Field { get; private set; }

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
