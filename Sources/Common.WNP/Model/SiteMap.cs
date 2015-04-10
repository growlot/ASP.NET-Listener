//-----------------------------------------------------------------------
// <copyright file="SiteMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="Site"/> class
    /// </summary>
    public class SiteMap : ClassMapping<Site>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMap"/> class.
        /// </summary>
        public SiteMap()
        {
            this.Table("wndba.TSITE");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("site");
                    m.Generator(Generators.Native);
                });

            // many to one mappings
            this.ManyToOne(
                x => x.Owner,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Owner));
                    map.Column("OWNER");
                    map.Fetch(FetchKind.Join);
                });

            this.Property(x => x.Description, m => m.Column("site_description"));
            this.Property(x => x.Address, m => m.Column("site_address"));
            this.Property(x => x.Address2, m => m.Column("site_address2"));
            this.Property(x => x.City, m => m.Column("site_city"));
            this.Property(x => x.State, m => m.Column("site_state"));
            this.Property(x => x.ZipCode, m => m.Column("site_zipcode"));
            this.Property(x => x.Country, m => m.Column("site_country"));
            this.Property(x => x.AccountName, m => m.Column("account_name"));
            this.Property(x => x.AccountNumber, m => m.Column("account_no"));
            this.Property(x => x.PremiseNumber, m => m.Column("premise_no"));
        }
    }
}