//-----------------------------------------------------------------------
// <copyright file="IdMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="Comment"/> class
    /// </summary>
    public class IdMap : ClassMapping<Id>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdMap"/> class.
        /// </summary>
        public IdMap()
        {
            this.MapId();
        }

        /// <summary>
        /// Maps the identifier.
        /// </summary>
        public virtual void MapId()
        {
            // ID mapping
            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("ID");
                    m.Generator(Generators.Native);
                });
        }
    }
}