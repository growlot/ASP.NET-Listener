//-----------------------------------------------------------------------
// <copyright file="Site.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing Site entity
    /// </summary>
    public class Site
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class.
        /// </summary>
        public Site()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Site(int id)
        {
            this.Id = id;
            this.Circuits = new List<Circuit>();
            this.Comments = new List<Comment>();
            this.RelatedFiles = new List<RelatedFile>();
        }
        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; set; }

        /// <summary>
        /// Gets or sets Site Address Info
        /// </summary>        
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets Site Account Info
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets site circuit info
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<Circuit> Circuits { get; set; }

        /// <summary>
        /// Gets or sets site comment info
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets site multimedia info
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Public set is needed for WCF")]
        public IList<RelatedFile> RelatedFiles { get; set; }
    }
}