// <copyright file="ListenerPersistenceAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Listener
{
    using Core;
    using Poco;

    /// <summary>
    /// Listener persistence adapter
    /// </summary>
    [WithinListenerContext]
    public class ListenerPersistenceAdapter : PocoCachedAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerPersistenceAdapter"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public ListenerPersistenceAdapter(ListenerDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}