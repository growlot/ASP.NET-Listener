// //-----------------------------------------------------------------------
// <copyright file="FieldConfigurationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System.Collections.Generic;

    /// <summary>
    /// Field configuration memento
    /// </summary>
    public class FieldConfigurationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldConfigurationMemento" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="mapToName">Name of the map to.</param>
        /// <param name="hashSequence">The hash sequence.</param>
        /// <param name="keySequence">The key sequence.</param>
        /// <param name="valueMap">The value map.</param>
        public FieldConfigurationMemento(string name, string mapToName, short? hashSequence, short? keySequence, Dictionary<string, object> valueMap)
        {
            this.Name = name;
            this.MapToName = mapToName;
            this.HashSequence = hashSequence;
            this.KeySequence = keySequence;
            if (valueMap != null)
            {
                foreach (var o in valueMap)
                {
                    this.ValueMap.Add(o.Key, o.Value);
                }
            }
        }

        /// <summary>
        /// Gets the key sequence.
        /// </summary>
        /// <value>The key sequence.</value>
        public short? KeySequence { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the name of the map to.
        /// </summary>
        /// <value>The name of the map to.</value>
        public string MapToName { get; private set; }

        /// <summary>
        /// Gets or sets the value map.
        /// </summary>
        /// <value>The value map.</value>
        public Dictionary<string, object> ValueMap { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the include in hash.
        /// </summary>
        /// <value>The include in hash.</value>
        public short? HashSequence { get; private set; }
    }
}