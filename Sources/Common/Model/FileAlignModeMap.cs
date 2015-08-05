//-----------------------------------------------------------------------
// <copyright file="FileAlignModeMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileAlignMode"/> class
    /// </summary>
    public class FileAlignModeMap : ClassMapping<FileAlignMode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileAlignModeMap"/> class.
        /// </summary>
        public FileAlignModeMap()
        {
            this.Table("FileAlignMode");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileAlignModeId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50));
        }
    }
}