// <copyright file="DataPropertyInfo.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Utilities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines data property.
    /// </summary>
    public class DataPropertyInfo
    {
        private readonly string property;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPropertyInfo"/> class.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="isList">if set to <c>true</c> [is list].</param>
        public DataPropertyInfo(string property, bool isList)
        {
            this.property = property;
            this.IsList = isList;
        }

        /// <summary>
        /// Gets a value indicating whether data property is list.
        /// </summary>
        /// <value>
        ///   <c>true</c> if data property is list; otherwise, <c>false</c>.
        /// </value>
        public bool IsList { get; }

        /// <summary>
        /// Gets the value of property.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>The value of property.</returns>
        public object GetValue(object owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), "Can not get property value if owner is not specified.");
            }

            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                return expandoDictionary[this.property];
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(this.property);
                return propInfo.GetValue(owner);
            }
        }

        /// <summary>
        /// Sets the value of property.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object owner, object value)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner), "Can not get property value if owner is not specified.");
            }

            var expandoDictionary = owner as IDictionary<string, object>;
            if (expandoDictionary != null)
            {
                expandoDictionary[this.property] = value;
            }
            else
            {
                var propInfo = owner.GetType().GetProperty(this.property);
                propInfo.SetValue(owner, value);
            }
        }
    }
}
