// //-----------------------------------------------------------------------
// <copyright file="FieldConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Field configuration
    /// </summary>
    public class FieldConfiguration : IOriginator
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the target field name
        /// </summary>
        /// <value>The target field name.</value>
        public string MapToName { get; set; }

        /// <summary>
        /// Gets or sets the value map.
        /// </summary>
        /// <value>The value map.</value>
        public ValueMapDictionary ValueMap { get; } = new ValueMapDictionary();

        /// <summary>
        /// Gets or sets the key sequence.
        /// </summary>
        /// <value>The key sequence.</value>
        public short? KeySequence { get; set; }

        /// <summary>
        /// Gets or sets the hash sequence.
        /// </summary>
        /// <value>The hash sequence.</value>
        public short? HashSequence { get; set; }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected void SetMemento(IMemento memento)
        {
            var myMemento = (FieldConfigurationMemento)memento;
            this.Name = myMemento.Name;
            this.MapToName = myMemento.MapToName;
            this.HashSequence = myMemento.HashSequence;
            this.KeySequence = myMemento.KeySequence;
            if (myMemento.ValueMap != null)
            {
                foreach (var valueMap in myMemento.ValueMap)
                {
                    this.ValueMap.Add(valueMap.Key, valueMap.Value);
                }
            }
        }
    }
}