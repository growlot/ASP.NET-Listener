//-----------------------------------------------------------------------
// <copyright file="LocationMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="Location"/> class
    /// </summary>
    public class LocationMap : ClassMapping<Location>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMap"/> class.
        /// </summary>
        public LocationMap()
        {
            this.Table("wndba.TLOCATION");
            this.Lazy(false);

            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("ID");
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

            this.Property(x => x.LocationName, m => m.Column("LOCATION"));
            this.Property(x => x.Description, m => m.Column("LOCN_DESC"));
            this.Property(x => x.Area, m => m.Column("area_name"));
        }
    }
}
