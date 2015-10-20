namespace AMSLLC.Listener.Persistence
{
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Repository.WNP;
    using Domain.WNP.OwnerAggregate;

    public class OwnerRepository : IOwnerRepository
    {
        private WNPDBContext dbContext;

        public OwnerRepository(WNPDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IMemento> GetOwner(int owner)
        {
            return Task.FromResult((IMemento)this.dbContext.FirstOrDefault<OwnerMemento>("SELECT owner FROM wndba.towner WHERE owner = @0", owner));
        }
    }
}
