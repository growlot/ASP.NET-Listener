// <copyright file="CircuitCreatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Threading.Tasks;
    using ApplicationService;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using WNP;

    /// <summary>
    /// Persists <see cref="CircuitCreatedEvent"/>
    /// </summary>
    public class CircuitCreatedEventHandler : EventPesistenceHandler, IDomainEventHandler<CircuitCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="requestScope">The request scope.</param>
        /// <param name="timeProvider">The time provider.</param>
        public CircuitCreatedEventHandler(ICurrentRequestScope requestScope, IDateTimeProvider timeProvider)
            : base(requestScope, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(CircuitCreatedEvent domainEvent)
        {
            var circuit = new CircuitEntity()
            {
                Owner = this.Owner,
                Site = domainEvent.SiteId,
                Circuit = domainEvent.CircuitId,
                CircuitDesc = domainEvent.Description,
                ConductorsPerPhase = domainEvent.NumberOfConductorsPerPhase,
                EnclosureType = domainEvent.EnclosureType,
                HasBracket = domainEvent.HasBracket ? "Y" : "N",
                InstallDate = domainEvent.InstallDate,
                Latitude = (double?)domainEvent.Latitude,
                Longitude = (double?)domainEvent.Longitude,
                MeterPoint = domainEvent.MeterPoint,
                ServiceAmps = (double?)domainEvent.ServiceAmperage,
                ServiceLocation = domainEvent.ServiceLocation,
                ServicePhase = domainEvent.ServicePhases.ToString(),
                ServicePoint = domainEvent.ServicePoint,
                ServiceVoltage = (double?)domainEvent.ServiceVoltage,
                ServiceWire = domainEvent.ServiceWires.ToString(),
                WireLocation = domainEvent.WireLocation,
                WireSize = domainEvent.WireSize,
                WireType = domainEvent.WireType,
            };

            return this.InsertAsync(circuit);
        }
    }
}
