//-----------------------------------------------------------------------
// <copyright file="InterconnectSite.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Interconnect site is a site where current utility is connected to other utility.
    /// These sites usually exchanges high volume of resources.
    /// </summary>
    public sealed class InterconnectSite : ValueObject<InterconnectSite>
    {
        /// <summary>
        /// The interconnect utility name.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// The flag that marks if site is this site is connected to other utility.
        /// </summary>
        private readonly bool isInterconnect;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterconnectSite"/> class.
        /// </summary>
        public InterconnectSite()
            : this(false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterconnectSite"/> class.
        /// </summary>
        /// <param name="name">The interconnect utility name.</param>
        public InterconnectSite(string name)
            : this(true, name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterconnectSite"/> class.
        /// </summary>
        /// <param name="isInterconnect">if set to <c>true</c> [is interconnect].</param>
        /// <param name="name">The name.</param>
        public InterconnectSite(bool isInterconnect, string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && !isInterconnect)
            {
                throw new ArgumentException("Non interconnect site can not have interconnect utility specified.");
            }

            this.isInterconnect = isInterconnect;
            this.name = name;
        }

        /// <summary>
        /// Gets the interconnect utility name.
        /// </summary>
        /// <value>
        /// The interconnect utility name.
        /// </value>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the flag indicating if site is connected to other utility.
        /// </summary>
        /// <value>
        /// The flag indicating if site is connected to other utility.
        /// </value>
        public bool IsInterconnect
        {
            get
            {
                return this.isInterconnect;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.isInterconnect)
            {
                return string.Format(CultureInfo.InvariantCulture, "Site connected to utility {0}.", this.name);
            }

            return string.Format(CultureInfo.InvariantCulture, "Not interconnect site.");
        }
    }
}
