// <copyright file="ExpressionHelpers.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    ///     Expression helpers
    /// </summary>
    public static class ExpressionHelpers
    {
        /// <summary>
        ///     Composes the expression.
        /// </summary>
        /// <typeparam name="T">Expression Type</typeparam>
        /// <param name="first">The first.</param>
        /// <param name="second">The second.</param>
        /// <param name="merge">The merge.</param>
        /// <returns>Expression&lt;T&gt;.</returns>
        /// <exception cref="ArgumentNullException">Validation exceptions</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "As designed")]
        public static Expression<T> Compose<T>(
            this Expression<T> first,
            Expression<T> second,
            Func<Expression, Expression, Expression> merge)
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
            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters.Select(
                (
                    f,
                    i) => new
                    {
                        f,
                        s = second.Parameters[i]
                    }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first
            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }
}