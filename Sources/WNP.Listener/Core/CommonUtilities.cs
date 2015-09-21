// //-----------------------------------------------------------------------
// // <copyright file="CommonUtilities.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Core
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Common utilities
    /// </summary>
    public static class CommonUtilities
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        /// Gets the property information.
        /// based on: http://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression
        /// </summary>
        /// <typeparam name="TSource">The type of the t source.</typeparam>
        /// <typeparam name="TProperty">The type of the t property.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="propertyLambda">The property lambda.</param>
        /// <returns>PropertyInfo.</returns>
        /// <exception cref="System.ArgumentException">
        /// </exception>
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda,
                    type));

            return propInfo;
        }
    }
}