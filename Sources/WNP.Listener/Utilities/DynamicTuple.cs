// <copyright file="DynamicTuple.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <summary>
    /// Tuple for holding variable number of items
    /// </summary>
    /// <typeparam name="T">DynamicTuple param</typeparam>
    public class DynamicTuple<T> : IEquatable<DynamicTuple<T>>
    {
        /// <summary>
        /// The values
        /// </summary>
        private readonly T[] values;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicTuple{T}"/> class.
        /// </summary>
        /// <param name="values">The values</param>
        public DynamicTuple(IEnumerable<T> values)
        {
            Contract.Requires(values != null);
            this.values = values.ToArray();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj != null && this.Equals(obj as DynamicTuple<T>);
        }

        /// <inheritdoc/>
        public bool Equals(DynamicTuple<T> other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other == null)
            {
                return false;
            }

            // values property will always be set on other, because it's the same type as this.
            Contract.Assume(other.values != null);

            var length = this.values.Length;
            if (length != other.values.Length)
            {
                return false;
            }

            for (var i = 0; i < length; ++i)
            {
                if (!Equals(this.values[i], other.values[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hc = 17;
            foreach (var value in this.values)
            {
                hc = (hc * 37) + (!ReferenceEquals(value, null) ? value.GetHashCode() : 0);
            }

            return hc;
        }
    }
}