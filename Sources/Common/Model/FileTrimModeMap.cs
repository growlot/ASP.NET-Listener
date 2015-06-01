//-----------------------------------------------------------------------
// <copyright file="FileTrimModeMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileTrimMode"/> class
    /// </summary>
    public class FileTrimModeMap : ClassMapping<FileTrimMode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileTrimModeMap"/> class.
        /// </summary>
        public FileTrimModeMap()
        {
            this.Table("FileTrimMode");
            this.Lazy(false);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileTrimModeId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50)); 
        }
    }
}