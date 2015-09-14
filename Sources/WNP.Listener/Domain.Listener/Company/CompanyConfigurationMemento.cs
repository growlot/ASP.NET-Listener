//-----------------------------------------------------------------------
// <copyright file="CompanyConfigurationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Listener.Company
{
    /// <summary>
    /// Memento class for company aggregate root
    /// </summary>
    public class CompanyConfigurationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyConfigurationMemento" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public CompanyConfigurationMemento(int id)
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
