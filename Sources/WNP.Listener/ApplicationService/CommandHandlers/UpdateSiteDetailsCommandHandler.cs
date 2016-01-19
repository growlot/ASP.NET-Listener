//-----------------------------------------------------------------------
// <copyright file="UpdateSiteDetailsCommandHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Commands;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles Site creation command.
    /// </summary>
    public class UpdateSiteDetailsCommandHandler : CommandHandlerBase, ICommandHandler<UpdateSiteDetailsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSiteDetailsCommandHandler" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="domainEventBus">The domain event bus.</param>
        public UpdateSiteDetailsCommandHandler(IWNPUnitOfWork unitOfWork, IDomainEventBus domainEventBus)
            : base(unitOfWork, domainEventBus)
        {
        }

        /// <inheritdoc/>
        public async Task HandleAsync(UpdateSiteDetailsCommand command)
        {
            IMemento ownerMemento = await this.UnitOfWork.SitesRepository.GetOwnerWithCollidingSitesAsync(command.SiteId, command.PremiseNumber, command.Description);
            Owner owner = new Owner();
            ((IOriginator)owner).SetMemento(ownerMemento);

            owner.UpdateSiteDetails(command.SiteId, command.PremiseNumber, command.Description);

            await this.PublishEvents(owner);
        }
    }
}
