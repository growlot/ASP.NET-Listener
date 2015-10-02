//-----------------------------------------------------------------------
// <copyright file="SiteMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.Site
{
    /// <summary>
    /// Memento class for site aggregate root
    /// </summary>
    public class SiteMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMemento" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public SiteMemento(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        internal int Id { get; private set; }
    }
}
