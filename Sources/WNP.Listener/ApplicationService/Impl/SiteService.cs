namespace AMSLLC.Listener.ApplicationService.Impl
{
    using System;
    using System.Threading.Tasks;
    using Domain.WNP.OwnerAggregate;
    using Domain.WNP.SiteAggregate;
    using Domain;
    using Messages;
    using Repository.WNP;

    public class SiteService
    {
        public async Task AddSite(AddSiteRequestMessage requestMessage)
        {
            using (var scope = ApplicationServiceScope.Create())
            {
                var siteRepository = scope.RepositoryBuilder.Create<ISiteRepository>();
                var ownerRepository = scope.RepositoryBuilder.Create<IOwnerSitesRepository>();
                
                // if premise number specified ensure uniquenes
                if (!string.IsNullOrWhiteSpace(requestMessage.PremiseNumber))
                {
                    var existingSite = await siteRepository.GetSiteByPremiseNumber(requestMessage.PremiseNumber);
                    if (existingSite != null)
                    {
                        throw new InvalidOperationException($"Can't create a new site with premise number {requestMessage.PremiseNumber}, because there is already a site with this premise number.");
                    }
                }

                var ownerMemento =
                    await
                        ownerRepository.GetOwner(requestMessage.Owner);

                var owner =
                    scope.DomainBuilder.Create<Owner>();

                ((IOriginator)owner).SetMemento(ownerMemento);

                PhysicalAddress address = new PhysicalAddressBuilder()
                    .CreatePhysicalAddress()
                    .WithCountry(requestMessage.Country)
                    .WithState(requestMessage.State)
                    .WithCity(requestMessage.City)
                    .WithAddressLine1(requestMessage.Address1)
                    .WithAddressLine2(requestMessage.Address2)
                    .WithZipCode(requestMessage.Zip);

                SiteBuilder siteBuilder = new SiteBuilder()
                    .CreateSite()
                    .WithDescription(requestMessage.Description)
                    .WithPremiseNumber(requestMessage.PremiseNumber)
                    .BilledTo(new BillingAccount(requestMessage.AccountName, requestMessage.AccountNumber))
                    .LocatedAt(address);
                
                await
                    Task.WhenAll(
                        owner.AddSite(siteBuilder));
            }
        }
    }
}
