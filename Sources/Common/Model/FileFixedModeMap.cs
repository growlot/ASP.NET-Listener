//-----------------------------------------------------------------------
// <copyright file="FileFixedModeMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileFixedMode"/> class
    /// </summary>
    public class FileFixedModeMap : ClassMapping<FileFixedMode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileFixedModeMap"/> class.
        /// </summary>
        public FileFixedModeMap()
        {
            this.Table("FileFixedMode");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileFixedModeId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50)); 
        }
    }
}