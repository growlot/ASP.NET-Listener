// <copyright file="TypeExtensionsTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core.Unit.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="TypeExtensions"/> class
    /// </summary>
    [TestClass]
    public class TypeExtensionsTests
    {
        /// <summary>
        /// Should the find existing generic method.
        /// </summary>
        [TestMethod]
        public void ShouldFindExistingGenericMethod()
        {
            var method = typeof(TypeForTest).GetGenericMethod("TestMethodStaticPublic");
            Assert.AreNotEqual(null, method, "Could not find static public method");

            method = typeof(TypeForTest).GetGenericMethod("TestMethodNonStaticPublic");
            Assert.AreNotEqual(null, method, "Could not find non static public method");

            method = typeof(TypeForTest).GetGenericMethod("TestMethodStaticProtected");
            Assert.AreNotEqual(null, method, "Could not find static protected method");

            method = typeof(TypeForTest).GetGenericMethod("TestMethodNonStaticProtected");
            Assert.AreNotEqual(null, method, "Could not find non static protected method");
        }

        /// <summary>
        /// Shoulds the fail if multiple generic methods are found.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldFailIfMultipleGenericMethodsAreFound()
        {
            typeof(TypeForTest).GetGenericMethod("TestMethodMultipleInstances");
        }

        /// <summary>
        /// Shoulds  fail if generic method is not found.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldFailIfGenericMethodNotFound()
        {
            typeof(TypeForTest).GetGenericMethod("NonExistingMethod");
        }

        /// <summary>
        /// Shoulds the fail if generic method is not found.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldFailIfGenericMethodNotFound2()
        {
            typeof(TypeForTest).GetGenericMethod("NonExistingMethod", new Type[] { null, typeof(int), typeof(bool) });
        }

        /// <summary>
        /// Shoulds the find generic method based on parameter list if multiple methods with same name exist.
        /// </summary>
        [TestMethod]
        public void ShouldFindGenericMethodFromParameterList()
        {
            var method = typeof(TypeForTest).GetGenericMethod("TestMethodMultipleInstances", new Type[] { null });
            Assert.AreNotEqual(null, method, "Could not find static public method");

            method = typeof(TypeForTest).GetGenericMethod("TestMethodMultipleInstances", new Type[] { null, null, typeof(bool) });
            Assert.AreNotEqual(null, method, "Could not find static public method");
        }

        /// <summary>
        /// Should return the default value for value types.
        /// </summary>
        [TestMethod]
        public void ShouldReturnDefaultValueForValueTypes()
        {
            Assert.AreEqual(0, typeof(int).GetDefault());
            Assert.AreEqual(false, typeof(bool).GetDefault());
            Assert.AreEqual('\0', typeof(char).GetDefault());
        }

        /// <summary>
        /// Should return null for reference or nullable types.
        /// </summary>
        [TestMethod]
        public void ShouldReturnNullForNullableTypes()
        {
            Assert.AreEqual(null, typeof(int?).GetDefault());
            Assert.AreEqual(null, typeof(string).GetDefault());
        }

        /// <summary>
        /// Should correctly detect nullable types.
        /// </summary>
        [TestMethod]
        public void ShouldCorrectlyDetectNullableTypes()
        {
            Assert.AreEqual(false, typeof(int).IsNullable());
            Assert.AreEqual(true, typeof(int?).IsNullable());
            Assert.AreEqual(true, typeof(string).IsNullable());
        }

        private class TypeForTest
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            public static T TestMethodMultipleInstances<T>(T parameter)
            {
                return parameter;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            public static T TestMethodMultipleInstances<T>(T parameter1, T parameter2, bool check)
            {
                return check ? parameter1 : parameter2;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            public static T TestMethodStaticPublic<T>(T parameter)
            {
                return parameter;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Test method.")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            public T TestMethodNonStaticPublic<T>(T parameter)
            {
                return parameter;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            protected static T TestMethodStaticProtected<T>(T parameter)
            {
                return parameter;
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Test method.")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used with reflection by test.")]
            protected T TestMethodNonStaticProtected<T>(T parameter)
            {
                return parameter;
            }
        }
    }
}
