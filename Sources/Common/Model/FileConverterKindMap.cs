//-----------------------------------------------------------------------
// <copyright file="FileConverterKindMap.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using NHibernate.Mapping.ByCode;
    using NHibernate.Mapping.ByCode.Conformist;
    using NHibernate.Type;

    /// <summary>
    /// Mapping for <see cref="FileConverterKind"/> class
    /// </summary>
    public class FileConverterKindMap : ClassMapping<FileConverterKind>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileConverterKindMap"/> class.
        /// </summary>
        public FileConverterKindMap()
        {
            this.Table("FileConverterKind");
            this.Lazy(true);

            this.Id(
                x => x.Id,
                m =>
                {
                    m.Column("FileConverterKindId");
                });
            this.Property(
                x => x.Description,
                x => x.Length(50));
        }
    }
}