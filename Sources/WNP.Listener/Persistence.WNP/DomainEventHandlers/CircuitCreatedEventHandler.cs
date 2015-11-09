// <copyright file="CircuitCreatedEventHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP.DomainEventHandlers
{
    using System.Threading.Tasks;
    using Core;
    using Domain;
    using Domain.WNP.SiteAggregate.CircuitChild;
    using Repository.WNP;
    using WNP;

    /// <summary>
    /// Persists <see cref="CircuitCreatedEvent"/>
    /// </summary>
    public class CircuitCreatedEventHandler : EventPesistenceHandler, IDomainEventHandler<CircuitCreatedEvent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitCreatedEventHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="user">The user who initiated the event.</param>
        /// <param name="timeProvider">The time provider.</param>
        public CircuitCreatedEventHandler(IWNPUnitOfWork unitOfWork, string user, IDateTimeProvider timeProvider)
            : base(unitOfWork, user, timeProvider)
        {
        }

        /// <inheritdoc/>
        public Task HandleAsync(CircuitCreatedEvent domainEvent)
        {
            var circuit = new CircuitEntity()
            {
                Owner = domainEvent.OwnerId,
                Site = domainEvent.SiteId,
                Circuit = domainEvent.Id,
                CircuitDesc = domainEvent.Description,
                ConductorsPerPhase = domainEvent.NumberOfConductorsPerPhase,
                CreateBy = this.User,
                CreateDate = this.TimeProvider.Now(),
                EnclosureType = domainEvent.EnclosureType,
                InstallDate = domainEvent.InstallDate,
                Latitude = (double?)domainEvent.Latitude,
                Longitude = (double?)domainEvent.Longitude,
                ServiceAmps = (double?)domainEvent.ServiceAmperage,
                ServiceLocation = domainEvent.ServiceLocation,
                ServicePhase = domainEvent.ServicePhases.ToString(),
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
