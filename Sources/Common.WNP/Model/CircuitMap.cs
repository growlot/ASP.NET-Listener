//-----------------------------------------------------------------------
// <copyright file="CircuitMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="Circuit"/> class
    /// </summary>
    public class CircuitMap : ClassMapping<Circuit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CircuitMap"/> class.
        /// </summary>
        public CircuitMap()
        {
            this.Table("wndba.TCIRCUIT");
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

            // many to one mappings
            this.ManyToOne(
                x => x.Site,
                map =>
                {
                    map.Cascade(Cascade.None);
                    map.Class(typeof(Site));
                    map.Column("SITE");
                    map.Fetch(FetchKind.Join);
                });

            this.Property(x => x.CircuitIndex, m => m.Column("CIRCUIT"));
            this.Property(x => x.Description, m => m.Column("CIRCUIT_DESC"));
            this.Property(x => x.Latitude, m => m.Column("LATITUDE"));
            this.Property(x => x.Longitude, m => m.Column("LONGITUDE"));
        }
    }
}
