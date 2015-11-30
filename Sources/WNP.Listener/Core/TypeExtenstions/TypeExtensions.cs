// <copyright file="TypeExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System
{
    using Collections.Generic;
    using Diagnostics.CodeAnalysis;
    using Diagnostics.Contracts;
    using Linq;
    using Reflection;

    /// <summary>
    /// Defines custom extensions for <see cref="Type"/> type
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the generic method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <returns>The generic method.</returns>
        public static MethodInfo GetGenericMethod(this Type type, string name)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(name != null);

            var methods = type
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                .Where(mInfo => mInfo.Name == name && mInfo.IsGenericMethod)
                .ToList();
            if (methods.Count == 0)
            {
                throw new InvalidOperationException("No generic methods found with specified method name {0}.".FormatWith(name));
            }
            else if (methods.Count == 1)
            {
                return methods.First();
            }

            throw new InvalidOperationException("Multiple generic methods found with specified name {0}.".FormatWith(name));
        }

        /// <summary>
        /// Gets the generic method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="parameterTypes">The parameter types. Use null for generic types.</param>
        /// <returns>The generic method.</returns>
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] parameterTypes)
        {
            Contract.Requires<ArgumentNullException>(type != null);
            Contract.Requires<ArgumentNullException>(name != null);
            Contract.Requires<ArgumentNullException>(parameterTypes != null);

            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            foreach (var method in methods.Where(m => m.Name == name && m.IsGenericMethodDefinition == true))
            {
                Contract.Assume(method != null, "Method is enumerated from list of methods and should never be null");
                var methodParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

                if (methodParameterTypes.SequenceEqual(parameterTypes, new SimpleTypeComparer()))
                {
                    return method;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if Type is Nullable.
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>true if type is Nullable, false otherwise</returns>
        public static bool IsNullable(this Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            return !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Gets default type value.
        /// </summary>
        /// <param name="type">Type to get default value of.</param>
        /// <returns>Default value of the Type provided.</returns>
        public static object GetDefault(this Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);

            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }

        private class SimpleTypeComparer : IEqualityComparer<Type>
        {
            public bool Equals(Type left, Type right)
            {
                // If both are null, or both are same instance, return true.
                if (ReferenceEquals(left, right))
                {
                    return true;
                }

                // use null type when checking with generic.
                if (left != null && (left.IsGenericParameter && right == null))
                {
                    return true;
                }

                // If non generic case and one is null, but not both, return false.
                if ((left == null) || (right == null))
                {
                    return false;
                }

                return left.FullName == right.FullName;
            }

            public int GetHashCode(Type obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
