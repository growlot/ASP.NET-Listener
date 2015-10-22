// <copyright file="ConstantNodeExtensions.cs" company="Advanced Metering Services LLC">
// Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace Microsoft.OData.Core.UriParser.Semantic
{
    using System;
    using Edm.Library;

    /// <summary>
    /// Defines custom extensions for <see cref="ConstantNode"/> type
    /// </summary>
    public static class ConstantNodeExtensions
    {
        /// <summary>
        /// Converts <see cref="ConstantNode"/> value from Edm type to Clr type.
        /// </summary>
        /// <param name="constNode">The constant node.</param>
        /// <returns>The Clr object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static object ToKnownClrType(this ConstantNode constNode)
        {
            var edmDate = constNode.Value as Date?;
            if (edmDate.HasValue)
            {
                return new DateTime(edmDate.Value.Year, edmDate.Value.Month, edmDate.Value.Day);
            }

            return constNode.Value;
        }
    }
}