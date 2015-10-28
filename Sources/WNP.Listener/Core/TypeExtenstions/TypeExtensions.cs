// <copyright file="TypeExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System
{
    using Collections.Generic;
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
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static MethodInfo GetGenericMethod(this Type type, string name)
        {
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
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The generic method.</returns>
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static MethodInfo GetGenericMethod(this Type type, string name, Type[] parameterTypes)
        {
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            foreach (var method in methods.Where(m => m.Name == name && m.IsGenericMethodDefinition == true))
            {
                var methodParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

                if (methodParameterTypes.SequenceEqual(parameterTypes, new SimpleTypeComparer()))
                {
                    return method;
                }
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

                // If one is null, but not both, return false.
                if ((left == null) || (right == null))
                {
                    return false;
                }

                return left.Assembly == right.Assembly &&
                    left.Namespace == right.Namespace &&
                    left.Name == right.Name;
            }

            public int GetHashCode(Type obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
