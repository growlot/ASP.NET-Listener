//-----------------------------------------------------------------------
// <copyright file="DeviceBatchMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;

    /// <summary>
    /// Mapping for <see cref="DeviceBatch"/> class
    /// </summary>
    public class DeviceBatchMap : ClassMapping<DeviceBatch>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceBatchMap"/> class.
        /// </summary>
        public DeviceBatchMap()
        {
            this.Table("DeviceBatch");
            this.Lazy(false);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("DeviceBatchId");
                    m.Generator(Generators.Native);
                });
            this.Property(x => x.BatchNumber);
            this.Property(x => x.ExternalId);
        }
    }
}