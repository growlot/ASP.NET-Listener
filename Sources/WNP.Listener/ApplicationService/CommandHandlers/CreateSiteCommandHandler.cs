//-----------------------------------------------------------------------
// <copyright file="CreateSiteCommandHanlder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.CommandHandlers
{
    using System.Threading.Tasks;
    using Domain;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Repository.WNP;

    /// <summary>
    /// Handles Site creation command.
    /// </summary>
    public class CreateSiteCommandHandler : ICommandHandler<CreateSiteCommand>
    {
        private readonly IWNPUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSiteCommandHandler"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public CreateSiteCommandHandler(IWNPUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handles the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>
        /// The empty task.
        /// </returns>
        public async Task Handle(CreateSiteCommand command)
        {
            IMemento ownerMemento = await (this.unitOfWork.OwnerRepository).GetOwner(command.Owner);
            Owner owner = new Owner();
            ((IOriginator)owner).SetMemento(ownerMemento);

            var siteBuilder = new SiteBuilder()
                .BilledTo(command.Account)
                .LocatedAt(command.Address)
                .WithDescription(command.Description)
                .WithPremiseNumber(command.PremiseNumber);
            var site = owner.AddSite(siteBuilder);
            //await (unitOfWork.SitesRespository.SaveSite(site));
        }
    }
}
