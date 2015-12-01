// <copyright file="WNPUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using Repository.WNP;

    /// <summary>
    /// Implements <see cref="IWNPUnitOfWork"/> for real database.
    /// </summary>
    public class WNPUnitOfWork : IWNPUnitOfWork
    {
        private WNPDBContext dbContext;
        private IOwnerRepository ownerRepository;
        private ISiteRepository siteRepository;
        private IWorkstationRepository workstationRepository;
        private IEquipmentRepository equipmentRepository;
        private int operatingCompany;

        /// <summary>
        /// Initializes a new instance of the <see cref="WNPUnitOfWork" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public WNPUnitOfWork(WNPDBContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.BeginTransaction();
        }

        /// <summary>
        /// Sets the set operating company.
        /// </summary>
        /// <value>
        /// The operating company.
        /// </value>
        public int SetOperatingCompany
        {
            set { this.operatingCompany = value; }
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        public WNPDBContext DbContext
        {
            get
            {
                return this.dbContext;
            }
        }

        /// <inheritdoc/>
        public IOwnerRepository OwnerRepository
        {
            get
            {
                if (this.ownerRepository == null)
                {
                    this.ownerRepository = new OwnerRepository(this.dbContext, this.operatingCompany);
                }

                return this.ownerRepository;
            }
        }

        /// <inheritdoc/>
        public ISiteRepository SitesRepository
        {
            get
            {
                if (this.siteRepository == null)
                {
                    this.siteRepository = new SiteRepository(this.dbContext, this.operatingCompany);
                }

                return this.siteRepository;
            }
        }

        /// <inheritdoc/>
        public IWorkstationRepository WorkstationRepository
        {
            get
            {
                if (this.workstationRepository == null)
                {
                    this.workstationRepository = new WorkstationRepository(this.dbContext, this.operatingCompany);
                }

                return this.workstationRepository;
            }
        }

        /// <inheritdoc/>
        public IEquipmentRepository EquipmentRepository
        {
            get
            {
                if (this.equipmentRepository == null)
                {
                    this.equipmentRepository = new EquipmentRepository(this.dbContext, this.operatingCompany);
                }

                return this.equipmentRepository;
            }
        }

        /// <inheritdoc/>
        public void Commit()
        {
            this.dbContext.CompleteTransaction();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (this.dbContext != null)
            {
                this.dbContext.Dispose();
            }
        }

        /// <inheritdoc/>
        public void Rollback()
        {
            this.dbContext.AbortTransaction();
        }

        /// <inheritdoc/>
        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
