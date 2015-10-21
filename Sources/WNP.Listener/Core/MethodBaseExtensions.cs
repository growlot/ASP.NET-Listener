using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AMSLLC.Core
{
    public static class MethodBaseExtensions
    {
        public static object InvokeWithNamedParameters(this MethodBase methodBase, object obj, IDictionary<string, object> parameters) =>
            methodBase.Invoke(obj, ConstructParameters(methodBase, parameters));

        private static object[] ConstructParameters(MethodBase methodBase, IDictionary<string, object> parameters)
        {
            var paramNames = methodBase.GetParameters().Select(p => p.Name).ToArray();
            var paramValues = new object[paramNames.Length];

            paramValues = paramValues.Select(o => Type.Missing).ToArray();
            parameters.Map(pair => paramValues[Array.IndexOf(paramNames, pair.Key)] = pair.Value);

            return paramValues;
        }
    }
}