//-----------------------------------------------------------------------
// <copyright file="VehicleMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="Vehicle"/> class
    /// </summary>
    public class VehicleMap : ClassMapping<Vehicle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleMap"/> class.
        /// </summary>
        public VehicleMap()
        {
            this.Table("wndba.tvehicle");
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

            // property mappings
            this.Property(
                x => x.VehicleNumber,
                m =>
                {
                    m.Column("vehicle_id");
                    m.Type(TypeFactory.GetAnsiStringType(32));
                    m.Length(32);
                });

            this.Property(
                x => x.Description,
                m =>
                {
                    m.Column("vehicle_description");
                    m.Type(TypeFactory.GetAnsiStringType(60));
                    m.Length(60);
                });
        }
    }
}
