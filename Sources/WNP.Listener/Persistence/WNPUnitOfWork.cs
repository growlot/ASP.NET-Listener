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
            this.dbContext.BeginTransaction();
        }

        public IOwnerRepository OwnerRepository
        {
            get
            {
                if(this.ownerRepository == null)
                {
                    this.ownerRepository = new OwnerRepository(this.dbContext);
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
            this.dbContext.CompleteTransaction();
        }

        public void Rollback()
        {
            this.dbContext.AbortTransaction();
        }
    }
}
