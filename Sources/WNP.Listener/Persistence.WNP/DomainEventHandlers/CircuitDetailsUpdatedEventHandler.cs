// <copyright file="CircuitDetailsUpdatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Metadata;
    using WNP;

    /// <summary>
    /// Persists <see cref="CircuitDetailsUpdatedEvent"/>
    /// </summary>
    public class CircuitDetailsUpdatedEventHandler : EventPesistenceHandler, IDomainEventHandler<CircuitDetailsUpdatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitDetailsUpdatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public CircuitDetailsUpdatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(CircuitDetailsUpdatedEvent domainEvent)
        {
            var circuit = new CircuitEntity()
            {
                Owner = this.Owner,
                Site = domainEvent.SiteId,
                Circuit = domainEvent.CircuitId,
                CircuitDesc = domainEvent.Description,
                EnclosureType = domainEvent.EnclosureType,
                HasBracket = domainEvent.HasBracket ? "Y" : "N",
                InstallDate = domainEvent.InstallDate,
                MeterPoint = domainEvent.MeterPoint,
                ServicePoint = domainEvent.ServicePoint,
            };

            var columnList = new List<string>();
            columnList.Add(DBMetadata.Circuit.CircuitDesc);
            columnList.Add(DBMetadata.Circuit.EnclosureType);
            columnList.Add(DBMetadata.Circuit.HasBracket);
            columnList.Add(DBMetadata.Circuit.InstallDate);
            columnList.Add(DBMetadata.Circuit.MeterPoint);
            columnList.Add(DBMetadata.Circuit.ServicePoint);

            return this.UpdateAsync(circuit, columnList);
        }
    }
}
