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
        /// <param name="outgoingSequence">The sequence number in the outgoing data hash code.</param>
        /// <param name="incomingSequence">The sequence number in the incoming data hash code</param>
        /// <param name="valueMap">The value map.</param>
        /// <param name="entityCategoryOperationId">The entity category operation identifier.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <param name="includeInSummary">if set to <c>true</c> [include in summary].</param>
        public FieldConfigurationMemento(string name, string mapToName, short? outgoingSequence, short? incomingSequence, Dictionary<string, object> valueMap, int entityCategoryOperationId, string operationKey, bool includeInSummary)
        {
            this.Name = name;
            this.MapToName = mapToName;
            this.OutgoingSequence = outgoingSequence;
            this.IncomingSequence = incomingSequence;
            this.OperationKey = operationKey;
            this.EntityCategoryOperationId = entityCategoryOperationId;
            this.IncludeInSummary = includeInSummary;
            if (valueMap != null)
            {
                foreach (var o in valueMap)
                {
                    this.ValueMap.Add(o.Key, o.Value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include in summary].
        /// </summary>
        /// <value><c>true</c> if [include in summary]; otherwise, <c>false</c>.</value>
        public bool IncludeInSummary { get; set; }

        /// <summary>
        /// Gets the enabled entity operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EntityCategoryOperationId { get; private set; }

        /// <summary>
        /// Gets the key sequence.
        /// </summary>
        /// <value>The key sequence.</value>
        public short? IncomingSequence { get; private set; }

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
        public short? OutgoingSequence { get; private set; }

        /// <summary>
        /// Gets the operation key.
        /// </summary>
        /// <value>The operation key.</value>
        public string OperationKey { get; private set; }
    }
}