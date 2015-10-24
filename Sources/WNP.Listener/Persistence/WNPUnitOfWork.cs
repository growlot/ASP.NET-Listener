// <copyright file="WNPUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence
{
    using Repository.WNP;

    /// <summary>
    /// Implements <see cref="IWNPUnitOfWork"/> for real database.
    /// </summary>
    public class WNPUnitOfWork : IWNPUnitOfWork
    {
        private WNPDBContext dbContext = new WNPDBContext("WNPDatabase");
        private IOwnerRepository ownerRepository;
        private ISiteRepository siteRepository;

        private WNPUnitOfWork()
        {
            this.dbContext.BeginTransaction();
        }

        /// <summary>
        /// Gets the owner repository.
        /// </summary>
        /// <value>
        /// The owner repository.
        /// </value>
        public IOwnerRepository OwnerRepository
        {
            get
            {
                if (this.ownerRepository == null)
                {
                    this.ownerRepository = new OwnerRepository(this.dbContext);
                }

                return this.ownerRepository;
            }
        }

        /// <summary>
        /// Gets the sites repository.
        /// </summary>
        /// <value>
        /// The sites repository.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public ISiteRepository SitesRepository
        {
            get
            {
                if (this.siteRepository == null)
                {
                    this.siteRepository = new SiteRepository(this.dbContext);
                }

                return this.siteRepository;
            }
        }

        /// <summary>
        /// Commits this unit of work.
        /// </summary>
        public void Commit()
        {
            this.dbContext.CompleteTransaction();
        }

        /// <summary>
        /// Rollbacks this unit of work.
        /// </summary>
        public void Rollback()
        {
            this.dbContext.AbortTransaction();
        }
    }
}
