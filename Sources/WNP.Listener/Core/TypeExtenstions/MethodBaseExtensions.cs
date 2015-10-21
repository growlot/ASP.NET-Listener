// <copyright file="MethodBaseExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace System.Reflection
{
    using Collections.Generic;
    using Linq;

    /// <summary>
    /// Defines custom extensions for <see cref="MethodBase"/> type
    /// </summary>
    public static class MethodBaseExtensions
    {
        /// <summary>
        /// Invokes the method with named parameters.
        /// </summary>
        /// <param name="methodBase">The method base.</param>
        /// <param name="obj">The object on which methods is invoked.</param>
        /// <param name="parameters">The parameters provided as name-value dictionary.</param>
        /// <returns>The object returned by invoked method.</returns>
        public static object InvokeWithNamedParameters(this MethodBase methodBase, object obj, IDictionary<string, object> parameters)
        {
            var paramNames = methodBase.GetParameters().Select(p => p.Name).ToArray();
            var paramValues = new object[paramNames.Length];

            paramValues = paramValues.Select(o => Type.Missing).ToArray();
            parameters.Map(pair => paramValues[Array.IndexOf(paramNames, pair.Key)] = pair.Value);

            return methodBase.Invoke(obj, paramValues);
        }
    }
}