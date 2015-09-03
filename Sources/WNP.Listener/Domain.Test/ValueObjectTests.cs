//-----------------------------------------------------------------------
// <copyright file="ValueObjectTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for ValueObject implementation
    /// </summary>
    [TestClass]
    public class ValueObjectTests
    {
        /// <summary>
        /// Equals works with identical objects.
        /// </summary>
        [TestMethod]
        public void EqualsWorksWithIdenticalObjects()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");

            Assert.IsTrue(address.Equals(address2));
        }

        /// <summary>
        /// Equals works with different objects.
        /// </summary>
        [TestMethod]
        public void EqualsWorksWithNonDifferentObjects()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.IsFalse(address.Equals(address2));
        }

        /// <summary>
        /// Equals works with null properties.
        /// </summary>
        [TestMethod]
        public void EqualsWorksWithNullProperties()
        {
            Address address = new Address(null, "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");
            Address address3 = new Address(null, "Austin", "TX");

            Assert.IsFalse(address.Equals(address2));
            Assert.IsFalse(address2.Equals(address));
            Assert.IsTrue(address.Equals(address3));
        }

        /// <summary>
        /// Equals is reflexive (object is equal to itself).
        /// </summary>
        [TestMethod]
        public void EqualsIsReflexive()
        {
            Address address = new Address("Address1", "Austin", "TX");

            Assert.IsTrue(address.Equals(address));
        }

        /// <summary>
        /// Equals is symmetric (same result if objects are switched places).
        /// </summary>
        [TestMethod]
        public void EqualsIsSymmetric()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.IsFalse(address.Equals(address2));
            Assert.IsFalse(address2.Equals(address));
        }

        /// <summary>
        /// Equality operators (==, !=) work.
        /// </summary>
        [TestMethod]
        public void EqualityOperatorsWork()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");
            Address address3 = new Address("Address2", "Austin", "TX");

            Assert.IsTrue(address == address2);
            Assert.IsTrue(address2 != address3);
        }

        /// <summary>
        /// Equality operators (==, !=) work with null objects.
        /// </summary>
        [TestMethod]
        public void EqualityOperatorsWorkWithNullObjects()
        {
            Address address = null;
            Address address2 = new Address("Address", "Austin", "TX");

            Assert.IsFalse(address == address2);
            Assert.IsTrue(address != address2);

            Assert.IsFalse(address2 == address);
            Assert.IsTrue(address2 != address);
        }

        /// <summary>
        /// Equals work with null objects.
        /// </summary>
        [TestMethod]
        public void EqualsWorkWithNullObjects()
        {
            Address address = null;
            Address address2 = new Address("Address", "Austin", "TX");

            Assert.IsFalse(address2.Equals(address));
            Assert.IsFalse(address2.Equals((object)address));
        }

        /// <summary>
        /// Derived types behave correctly.
        /// </summary>
        [TestMethod]
        public void DerivedTypesBehaveCorrectly()
        {
            Address address = new Address("Address1", "Austin", "TX");
            ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

            Assert.IsFalse(address.Equals(address2));
            Assert.IsFalse(address == address2);
        }

        /// <summary>
        /// Equal objects have same hash code.
        /// </summary>
        [TestMethod]
        public void EqualObjectsHaveSameHashCode()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address1", "Austin", "TX");

            Assert.AreEqual(address.GetHashCode(), address2.GetHashCode());
        }

        /// <summary>
        /// Transposed values give different hash codes.
        /// </summary>
        [TestMethod]
        public void TransposedValuesGiveDifferentHashCodes()
        {
            Address address = new Address(null, "Austin", "TX");
            Address address2 = new Address("TX", "Austin", null);

            Assert.AreNotEqual(address.GetHashCode(), address2.GetHashCode());
        }

        /// <summary>
        /// Unequal objects have different hash codes.
        /// </summary>
        [TestMethod]
        public void UnequalObjectsHaveDifferentHashCodes()
        {
            Address address = new Address("Address1", "Austin", "TX");
            Address address2 = new Address("Address2", "Austin", "TX");

            Assert.AreNotEqual(address.GetHashCode(), address2.GetHashCode());
        }

        /// <summary>
        /// Derived types hash codes behave correctly.
        /// </summary>
        [TestMethod]
        public void DerivedTypesHashCodesBehaveCorrectly()
        {
            ExpandedAddress address = new ExpandedAddress("Address1", "Apt 999999", "Austin", "TX");
            ExpandedAddress address2 = new ExpandedAddress("Address1", "Apt 123", "Austin", "TX");

            Assert.AreNotEqual(address.GetHashCode(), address2.GetHashCode());
        }

        /// <summary>
        /// Class used for testing ValueObject implementation
        /// </summary>
        private class Address : ValueObject<Address>
        {
            /// <summary>
            /// The _address1
            /// </summary>
            private readonly string address1;

            /// <summary>
            /// The _city
            /// </summary>
            private readonly string city;

            /// <summary>
            /// The _state
            /// </summary>
            private readonly string state;

            /// <summary>
            /// Initializes a new instance of the <see cref="Address"/> class.
            /// </summary>
            /// <param name="address1">The address1.</param>
            /// <param name="city">The city.</param>
            /// <param name="state">The state.</param>
            public Address(string address1, string city, string state)
            {
                this.address1 = address1;
                this.city = city;
                this.state = state;
            }

            /// <summary>
            /// Gets the address1.
            /// </summary>
            /// <value>
            /// The address1.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Properties are used in equality and has functions.")]
            public string Address1
            {
                get
                {
                    return this.address1;
                }
            }

            /// <summary>
            /// Gets the city.
            /// </summary>
            /// <value>
            /// The city.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Properties are used in equality and has functions.")]
            public string City
            {
                get
                {
                    return this.city;
                }
            }

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <value>
            /// The state.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Properties are used in equality and has functions.")]
            public string State
            {
                get
                {
                    return this.state;
                }
            }
        }

        /// <summary>
        /// Class used for testing ValueObject implementation in inheritance scenarios
        /// </summary>
        private class ExpandedAddress : Address
        {
            /// <summary>
            /// The _address2
            /// </summary>
            private readonly string address2;

            /// <summary>
            /// Initializes a new instance of the <see cref="ExpandedAddress"/> class.
            /// </summary>
            /// <param name="address1">The address1.</param>
            /// <param name="address2">The address2.</param>
            /// <param name="city">The city.</param>
            /// <param name="state">The state.</param>
            public ExpandedAddress(string address1, string address2, string city, string state)
                : base(address1, city, state)
            {
                this.address2 = address2;
            }

            /// <summary>
            /// Gets the address2.
            /// </summary>
            /// <value>
            /// The address2.
            /// </value>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Properties are used in equality and has functions.")]
            public string Address2
            {
                get
                {
                    return this.address2;
                }
            }
        }
    }
}
