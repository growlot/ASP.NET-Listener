// <copyright file="EntityCategory.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using OperationEvents;

    /// <summary>
    /// Entity Category
    /// </summary>
    public class EntityCategory : AggregateRoot<Guid>
    {
        private ICollection<EntityCategoryOperation> Operations { get; } = new Collection<EntityCategoryOperation>();

        private ICollection<EnabledOperation> EnabledOperations { get; } = new Collection<EnabledOperation>();

        private string EntityCategoryName { get; set; }

        /// <summary>
        /// Adds the operation for the entity category.
        /// </summary>
        /// <param name="entityOperationId">The entity operation identifier.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="fieldConfiguration">The field configuration.</param>
        /// <param name="endpoints">The endpoints.</param>
        public void AddOperation(
            Guid entityOperationId,
            string operationName,
            FieldConfigurationMemento fieldConfiguration,
            ICollection<EndpointMemento> endpoints)
        {
            if (
                this.EnabledOperations.All(
                    s => string.Compare(s.OperationName, operationName, StringComparison.Ordinal) != 0))
            {
                throw new InvalidOperationException(
                    "Operation {0} is not allowed".FormatWith(operationName, CultureInfo.InvariantCulture));
            }

            if (endpoints.Any(e => e == null))
            {
                throw new InvalidOperationException("One or more endpoints are not available");
            }

            if (this.Operations.Any(s => string.Compare(s.Operation, operationName, StringComparison.Ordinal) == 0))
            {
                throw new InvalidOperationException("This operation is already configured for the {0} entity".FormatWith(this.EntityCategoryName, CultureInfo.InvariantCulture));
            }

            var mem = new EntityCategoryOperationMemento()
            {
                OperationName = operationName,
                FieldConfiguration = fieldConfiguration,
                EntityCategoryOperationId = entityOperationId
            };

            mem.Endpoints.AddRange(endpoints);

            var eco = new EntityCategoryOperation();
            ((IOriginator)eco).SetMemento(mem);
            this.Operations.Add(eco);

            this.Events.Add(new EntityOperationAdded { EntityCategoryId = this.Id, EntityCategoryOperation = eco });
        }

        /// <summary>
        /// Deletes the entity category operation.
        /// </summary>
        /// <param name="entityOperationId">The entity operation identifier.</param>
        public void DeleteOperation(
                    Guid entityOperationId)
        {
            var operationToRemove = this.Operations.SingleOrDefault(s => s.Id == entityOperationId);

            if (operationToRemove == null)
            {
                throw new InvalidOperationException("Operation not found on the {0}".FormatWith(this.EntityCategoryName, CultureInfo.InvariantCulture));
            }

            this.Operations.Remove(operationToRemove);

            this.Events.Add(new EntityOperationRemoved { EntityCategoryId = this.Id, EntityOperationId = entityOperationId });
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(
            IMemento memento)
        {
            var myMemento = (EntityCategoryMemento)memento;
            this.EntityCategoryName = myMemento.EntityCategoryName;
            this.Id = myMemento.Id;
            foreach (EntityCategoryOperationMemento entityCategoryOperationMemento in myMemento.Operations)
            {
                var rec = new EntityCategoryOperation();
                ((IOriginator)rec).SetMemento(entityCategoryOperationMemento);
                this.Operations.Add(rec);
            }

            this.EnabledOperations.AddRange(myMemento.EnabledOperations.Select(s => new EnabledOperation(s)));
        }
    }
}