// //-----------------------------------------------------------------------
// // <copyright file="FieldConfiguration.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
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
        /// Gets or sets a value indicating whether this field should be included in the hash calculation.
        /// </summary>
        /// <value><c>true</c> if the field needs to be included in hash calculation; otherwise, <c>false</c>.</value>
        public bool IncludeInHash { get; set; }

        /// <summary>
        /// Gets or sets the value map.
        /// </summary>
        /// <value>The value map.</value>
        public ValueMapDictionary ValueMap { get; } = new ValueMapDictionary();

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