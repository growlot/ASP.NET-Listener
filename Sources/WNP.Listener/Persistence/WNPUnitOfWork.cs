namespace AMSLLC.Listener.Persistence
{
    using System;
    using Repository.WNP;

    public class WNPUnitOfWork : IWNPUnitOfWork
    {
        private WNPDBContext dbContext = new WNPDBContext("WNPDatabase");
        private IOwnerRepository ownerRepository;

        private WNPUnitOfWork()
        {
            dbContext.BeginTransaction();
        }

        public IOwnerRepository OwnerRepository
        {
            get
            {
                if(ownerRepository == null)
                {
                    this.ownerRepository = new OwnerRepository(dbContext);
                }
                return this.ownerRepository;
            }
        }

        public ISiteRepository SitesRepository
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Commit()
        {
            dbContext.CompleteTransaction();
        }

        public void Rollback()
        {
            dbContext.AbortTransaction();
        }
    }
}
