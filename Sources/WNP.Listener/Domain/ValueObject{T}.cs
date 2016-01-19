//-----------------------------------------------------------------------
// <copyright file="ValueObject{T}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    /// <summary>
    /// The abstract ValueObject class that provides common implementation for equality and hashing functions
    /// </summary>
    /// <typeparam name="T">The type of value object.</typeparam>
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.Equals(obj as T);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            int startValue = 17;
            int multiplier = 59;

            int hashCode = startValue;

            IEnumerable<FieldInfo> fields = this.GetFields();

            // This is ValueObject class and value object always contain at least one field.
            Contract.Assert(fields != null);

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                {
                    hashCode = (hashCode * multiplier) + value.GetHashCode();
                }
            }

            return hashCode;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public virtual bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            IEnumerable<FieldInfo> fields = this.GetFields();

            foreach (FieldInfo field in fields)
            {
                // list of fields was retrieved from object definition, so it can't contain null's
                Contract.Assume(field != null);

                object thisValue = field.GetValue(this);
                object otherValue = field.GetValue(other);

                if (thisValue == null)
                {
                    if (otherValue != null)
                    {
                        return false;
                    }
                }
                else if (!thisValue.Equals(otherValue))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the fields from object and all it's base objects.
        /// </summary>
        /// <returns>List of fields in the object.</returns>
        private IEnumerable<FieldInfo> GetFields()
        {
            Contract.Ensures(Contract.Result<IEnumerable<FieldInfo>>() != null);
            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<FieldInfo>>(), x => x != null));

            Type t = this.GetType();

            List<FieldInfo> fields = new List<FieldInfo>();

            // fields are retrieved from object using reflection, so they can't be null
            ////Contract.Assume(Contract.ForAll(fields, x => x != null));
            ////Contract.Assert(Contract.ForAll(fields, x => x != null));
            while (t != typeof(object))
            {
                var currentTypeFields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                fields.AddRange(currentTypeFields);
                t = t.BaseType;

                // all types in C# inherit from object, and this part is not executed when type is object.
                Contract.Assume(t != null);
            }

            return fields;
        }
    }
}
