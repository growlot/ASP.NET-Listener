﻿// //-----------------------------------------------------------------------
// <copyright file="FailFast.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// A helpper class to perform quick checks on the arguments.
    /// </summary>
    public static class FailFast
    {
        /// <summary>
        /// Ensures the value is not null.
        /// </summary>
        /// <typeparam name="TType">The type of the object to validate.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The variable name.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static void EnsureNotNull<TType>(TType obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// Allowing to continue if passed object is not null
        /// </summary>
        /// <typeparam name="TType">The type of the object to validate.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The variable name.</param>
        /// <returns>TType.</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static TType PassThroughNotNull<TType>(TType obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }

            return obj;
        }

        /// <summary>
        /// Throw an error if rule is invalid
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="message">The message.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static void AssertTrue(Func<bool> rule, string message, string argumentName = null)
        {
            if (!rule())
            {
                throw string.IsNullOrEmpty(argumentName)
                    ? new ArgumentException(message)
                    : new ArgumentException(message, argumentName);
            }
        }
    }
}