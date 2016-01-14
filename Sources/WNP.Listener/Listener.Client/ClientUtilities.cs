// <copyright file="ClientUtilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Utilities class for Listener client.
    /// </summary>
    public static class ClientUtilities
    {
        /// <summary>
        /// Composes two expressions to one.
        /// </summary>
        /// <typeparam name="T">The type of expression</typeparam>
        /// <param name="first">The first expression.</param>
        /// <param name="second">The second expression.</param>
        /// <param name="merge">The merge function.</param>
        /// <returns>The merged expression</returns>
        /// <exception cref="ArgumentNullException">At least one expression is not provided.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Method is specifically designe to work with Expressions.")]
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (merge == null)
            {
                throw new ArgumentNullException(nameof(merge));
            }

            // map from parameters of second to parameters of firs)
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }
}
