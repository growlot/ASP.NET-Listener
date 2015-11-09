// <copyright file="WnpPersistenceAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNPAsync
{
    using AMSLLC.Listener.Core;
    using Poco;

    /// <summary>
    /// WNP persistence adapter
    /// </summary>
    [WithinWnpContext]
    public class WnpPersistenceAdapter : PocoCachedAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WnpPersistenceAdapter" /> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public WnpPersistenceAdapter(WnpAsyncDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}