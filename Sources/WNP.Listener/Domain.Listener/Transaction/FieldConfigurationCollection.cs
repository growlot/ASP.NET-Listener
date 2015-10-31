// <copyright file="FieldConfigurationCollection.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Field configuration collection
    /// </summary>
    public class FieldConfigurationCollection : Collection<FieldConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldConfigurationCollection"/> class.
        /// </summary>
        public FieldConfigurationCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldConfigurationCollection"/> class.
        /// </summary>
        /// <param name="fieldConfigurations">The field configurations.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public FieldConfigurationCollection(IEnumerable<FieldConfiguration> fieldConfigurations)
        {
            if (fieldConfigurations != null)
            {
                foreach (var fieldConfiguration in fieldConfigurations)
                {
                    this.Add(fieldConfiguration);
                }
            }
        }
    }
}